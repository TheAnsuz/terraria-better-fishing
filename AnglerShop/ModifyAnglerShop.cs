using BetterFishing.Config.Model;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    internal class ModifyAnglerShop : GlobalNPC
    {
        // Opens it alongside the interfaze
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Angler && BetterFishing.Configuration.AnglerShopMode != AnglerShopMode.DISABLED)
                AnglerShop.OpenAnglerShop();
        }

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type != NPCID.Angler)
                return;

            Player player = Main.LocalPlayer;

            int offset = AnglerShop.Line * 10; // Item per line

            AnglerShopMode _anglerShopMode = BetterFishing.Configuration.AnglerShopMode;
            int itemsIndex = 0;
            for (int i = 0; i < AnglerShop.GetEntrySize(); i++)
            {
                if (itemsIndex >= items.Length)
                    break;

                KeyValuePair<int, AnglerShopEntry> entry = AnglerShop.GetEntryByIndex(i);

                // Forced condition, always check (ie: is Hardmode)
                if (!entry.Value.ForcedCondition.Acomplishes(npc, player, items))
                    continue;

                // Is complex or conditional mode, checks for custom conditions
                if ((_anglerShopMode == AnglerShopMode.COMPLEX ||
                    _anglerShopMode == AnglerShopMode.CONDITIONAL)
                    && !entry.Value.Condition.Acomplishes(npc, player, items))
                    continue;

                // If complex or progressive, check for amount of completed quests
                if ((_anglerShopMode == AnglerShopMode.COMPLEX ||
                    _anglerShopMode == AnglerShopMode.PROGRESSIVE)
                    && entry.Value.QuestsToUnlock > player.anglerQuestsFinished)
                    continue;

                if (offset-- > 0)
                    continue;

                Item item = new(entry.Key)
                {
                    isAShopItem = true,
                    shopCustomPrice = entry.Value.Cost
                };

                if (!entry.Value.UseVanillaCurrency)
                    item.shopSpecialCurrency = AnglerShopSystem.QuestCoinCurrencyID;

                items[itemsIndex++] = item;
            }
            int index = items.Length - 1;

            if (items[index] != null && !items[index].IsAir && AnglerShop.Line >= AnglerShop.CalculatedMaxLine)
            {
                AnglerShop.CalculatedMaxLine++;
            }

            if (items[index - 10] == null || items[index - 10].IsAir)
            {
                AnglerShop.CalculatedMaxLine--;
            }
        }
    }
}
