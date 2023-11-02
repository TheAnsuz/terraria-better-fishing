using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class AndSellCondition : SellCondition
    {
        private readonly SellCondition first;
        private readonly SellCondition second;

        internal AndSellCondition(SellCondition first, SellCondition second)
        {
            this.first = first;
            this.second = second;
        }

        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return first.Acomplishes(npc, player, items) && second.Acomplishes(npc, player, items);
        }
    }
}
