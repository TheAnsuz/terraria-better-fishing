using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class NotSellCondition : SellCondition
    {
        private readonly SellCondition condition;

        internal NotSellCondition(SellCondition condition)
        {
            this.condition = condition;
        }

        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return !condition.Acomplishes(npc, player, items);
        }
    }
}
