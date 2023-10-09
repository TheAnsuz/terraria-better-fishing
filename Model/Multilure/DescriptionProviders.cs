
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using System.Linq;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace BetterFishing.Model.Multilure
{
    public class DescriptionProviders
    {
        private const string TOOLTIP_DEFINITION = "Tooltip";

        /*
         * Descripcion normal implementada por el mod
         */

        public static DescriptionProvider NormalDescription(int itemId, int bobblers)
        {
            _normalBobblers[itemId] = bobblers;
            return NormalDescription_Delegated;
        }

        private readonly static Dictionary<int, int> _normalBobblers = new();

        private static void NormalDescription_Delegated(Mod mod, Item item, List<TooltipLine> tooltips)
        {
            int existingTooltips = tooltips.Count(o => o.Name.Contains(TOOLTIP_DEFINITION));
            TooltipLine line;

            if (_normalBobblers[item.type] > 1)
                line = new(mod, $"{TOOLTIP_DEFINITION}{existingTooltips + 1}", Language.GetTextValue("Mods.BetterFishing.Multilure.Normal.Tooltip", _normalBobblers[item.type]));
            else
                line = new(mod, $"{TOOLTIP_DEFINITION}{existingTooltips + 1}", Language.GetTextValue("Mods.BetterFishing.Multilure.Normal.TooltipSingle"));

            tooltips.Add(line);
        }

        /*
         * Descripcion normal con rango de valores
         */
        private static readonly Dictionary<int, int> _rangedMin = new();
        private static readonly Dictionary<int, int> _rangedMax = new();

        public static DescriptionProvider RangedDescription(int itemId, int min, int max)
        {
            _rangedMin[itemId] = min;
            _rangedMax[itemId] = max;
            return RangedDescription_Delegated;
        }

        private static void RangedDescription_Delegated(Mod mod, Item item, List<TooltipLine> tooltips)
        {
            int existingTooltips = tooltips.Count(o => o.Name.Contains(TOOLTIP_DEFINITION));
            TooltipLine line;

            line = new(mod, $"{TOOLTIP_DEFINITION}{existingTooltips + 1}", Language.GetTextValue("Mods.BetterFishing.Multilure.Ranged.Tooltip", _rangedMin[item.type], _rangedMax[item.type]));

            tooltips.Add(line);
        }

        /*
         * Sin descripcion modificada
         */

        public static DescriptionProvider DefaultDescription()
        {

            return DefaultDescription_Delegated;
        }

        private static void DefaultDescription_Delegated(Mod mod, Item item, List<TooltipLine> tooltips)
        {
            int existingTooltips = tooltips.Count(o => o.Name.Contains(TOOLTIP_DEFINITION));
            TooltipLine line;

            line = new(mod, $"{TOOLTIP_DEFINITION}{existingTooltips + 1}", Language.GetTextValue("Mods.BetterFishing.Multilure.Normal.TooltipSingle"));

            tooltips.Add(line);
        }

        /*
         * Descripcion personalizada simple
         */

        public static DescriptionProvider SimpleCustomDescription(string modName, string key, params object[] args)
        {
            return (Mod mod, Item item, List<TooltipLine> tooltips) =>
            {
                int existingTooltips = tooltips.Count(o => o.Name.Contains(TOOLTIP_DEFINITION));

                tooltips.Add(new(mod, $"{TOOLTIP_DEFINITION}{existingTooltips + 1}", Language.GetTextValue($"Mods.BetterFishing.Multilure.{modName}.{key}", args)));
            };
        }

        public static DescriptionProvider CustomDescription(DescriptionProvider provider)
        {
            return provider;
        }

        public static DescriptionProvider RemplaceDescription(string modName, int line, string key, params object[] args)
        {
            return (Mod mod, Item item, List<TooltipLine> tooltips) =>
            {
                int existingTooltips = tooltips.Count(o => o.Name.Contains(TOOLTIP_DEFINITION));

                tooltips.Add(new(mod, $"{TOOLTIP_DEFINITION}{line}", Language.GetTextValue($"Mods.BetterFishing.Multilure.{modName}.{key}", args)));
            };
        }
    }
}
