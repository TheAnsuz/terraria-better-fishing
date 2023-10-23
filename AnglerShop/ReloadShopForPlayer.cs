using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
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
    }
}
