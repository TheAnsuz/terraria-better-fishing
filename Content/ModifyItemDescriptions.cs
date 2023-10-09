using BetterFishing.Model.Multilure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.Content
{
    internal class ModifyItemDescriptions : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.fishingPole <= 0)
                return;


            MultilureModRecord record = MultilureMod.GetRecord(item.type);

            if (record == null) return;


            DescriptionProvider description = record.GetDescription();

            if (description == null) return;

            description.Invoke(Mod, item, tooltips);
        }
    }
}
