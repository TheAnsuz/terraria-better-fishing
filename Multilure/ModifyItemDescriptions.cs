using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.Multilure
{
    internal class ModifyItemDescriptions : GlobalItem
    {

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.fishingPole <= 0)
                return;

            if (BetterFishing.Configuration.MultilureMode == Config.Model.MultilureMode.DISABLED)
                return;

            MultilureDescription[] lines = Multilure.GetDescription(item.type);

            if (lines == null) return;

            foreach (MultilureDescription line in lines)
            {

                if (line.IsAddition)
                {
                    tooltips.Add(line.Tooltip());

                }
                else if (line.IsRemoval)
                {
                    int index = tooltips.FindIndex(tooltip => tooltip.Mod.Equals(line.Mod) && tooltip.Name.Equals(line.Name));
                    tooltips.RemoveAt(index);

                }
                else if (line.IsReplacement)
                {
                    int index = tooltips.FindIndex(tooltip => tooltip.Mod.Equals(line.Mod) && tooltip.Name.Equals(line.Name));
                    tooltips.RemoveAt(index);
                    tooltips.Insert(index, line.Tooltip());
                }
            }
        }
    }
}
