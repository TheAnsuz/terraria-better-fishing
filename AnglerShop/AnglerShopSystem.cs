using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Input;

namespace BetterFishing.AnglerShop
{
    internal class AnglerShopSystem : ModSystem
    {
        public static int QuestCoinID;
        public static int QuestCoinCurrencyID;

        private static UserInterface _anglerShop;

        public override void Load()
        {
            On_Player.GetAnglerReward_MainReward += ModifyAnglerReward.GiveRewards;
            BetterFishing.Configuration.OnConfigurationChanged += OnConfigurationChanged;
            On_Main.SetNPCShopIndex += UpdateShop;
            if (Main.dedServ)
                return;

            _anglerShop = new UserInterface();
            _anglerShop.SetState(new AnglerShopUIState());
        }

        private void OnConfigurationChanged()
        {
            AnglerShop.CalculatedMaxLine = 0;

            if (AnglerShop.IsOpen)
                AnglerShop.OpenAnglerShop();
        }

        private void UpdateShop(On_Main.orig_SetNPCShopIndex orig, int index)
        {
            orig(index);

            if (index != 0)
                return;

            AnglerShop.CloseAnglerShop();
        }

        public override void PostSetupContent()
        {
            QuestCoinID = ModContent.ItemType<QuestCoinItem>();
            QuestCoinCurrency currency = new(QuestCoinID, Item.CommonMaxStack);
            QuestCoinCurrencyID = CustomCurrencyManager.RegisterCurrency(currency);
        }

        public override void PostUpdateInput()
        {
            if (Main.keyState.IsKeyDown(Keys.Escape))
            {
                AnglerShop.CloseAnglerShop();
            }

            if (AnglerShop.IsOpen && PlayerInput.ScrollWheelDeltaForUI != 0)
            {
                int fixedScroll = -PlayerInput.ScrollWheelDeltaForUI / 120;

                if (AnglerShop.CanScroll(fixedScroll))
                {
                    AnglerShop.Line += fixedScroll;
                    Main.instance.shop[Main.npcShop].SetupShop("BetterFishing/Angler/Shop", Main.LocalPlayer.TalkNPC);
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _anglerShop.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "BetterFishing: Angler Shop",
                    delegate
                    {
                        if (AnglerShop.IsOpen)
                        {
                            _anglerShop.Draw(Main.spriteBatch, new());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
