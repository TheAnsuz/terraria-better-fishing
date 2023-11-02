using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    internal class QuestCoinItem : ModItem
    {
        public override LocalizedText DisplayName => Language.GetOrRegister($"{BetterFishing.LANGUAGE_BASE}.AnglerShop.Coin.DisplayName");
        public override LocalizedText Tooltip => Language.GetOrRegister($"{BetterFishing.LANGUAGE_BASE}.AnglerShop.Coin.Tooltip");
        public override string Texture => $"{BetterFishing.ASSETS_BASE}/Item/QuestCoinItem";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 40;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = Terraria.ID.ItemRarityID.Orange;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.value = Terraria.Item.buyPrice(silver: 60);
        }

    }
}
