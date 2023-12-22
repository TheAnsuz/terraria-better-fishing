using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    internal class ReloadShopForPlayer : ModPlayer
    {
        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (AnglerShop.IsOpen)
                AnglerShop.OpenAnglerShop();
        }

        public override bool CanBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            // See branch_gimmic
            if (vendor.type != NPCID.Angler)
                return base.CanBuyItem(vendor, shopInventory, item);

            if (!AnglerShop.IsOpen)
                return base.CanBuyItem(vendor, shopInventory, item);

            AnglerShopEntry entry = AnglerShop.GetEntry(item.type);

            if (entry == null)
                return base.CanBuyItem(vendor, shopInventory, item);

            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(entry.ToString()), Color.White);

            return true;
        }
    }
}
