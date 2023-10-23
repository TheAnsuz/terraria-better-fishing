using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class HappinessSellCondition : SellCondition
    {
        private readonly NPCHappiness _happiness;

        internal HappinessSellCondition(NPCHappiness happiness)
        {
            this._happiness = happiness;
        }

        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return npc.Happiness.Equals(_happiness);
        }
    }
}
