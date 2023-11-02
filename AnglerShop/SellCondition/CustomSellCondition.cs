using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class CustomSellCondition : SellCondition
    {
        private readonly CustomCheck _check;

        internal CustomSellCondition(CustomCheck check)
        {
            _check = check;
        }

        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return _check(npc, player, items);
        }

        public delegate bool CustomCheck(NPC npc, Player player, Item[] items);
    }
}
