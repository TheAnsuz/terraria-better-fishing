using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class TrueSellCondition : SellCondition
    {
        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return true;
        }
    }
}
