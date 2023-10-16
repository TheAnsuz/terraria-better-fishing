using BetterFishing.Config.Model;
using BetterFishing.Multilure.PostThrow;
using System;
using System.Collections.Generic;

namespace BetterFishing.Multilure
{
    public class Multilure
    {
        public const int DEFAULT_SPREAD = 45;
        public const string LANGUAGE_PREFIX = "Mods.BetterFishing.Multilure";
        public const float SPREAD_MULTIPLIER = 0.05f;
        public const string DESCRIPTION_DEFAULT = "It is unknown what this item does";
        public const string DESCRIPTION_PREFIX = "[i:2373] ";
        public const string TOOLTIP_DEFINITION = "BetterFishing";

        private static Dictionary<MultilureMode, Dictionary<int, MultilurePostThrow>> _postThrows;
        private static Dictionary<MultilureMode, Dictionary<int, MultilureLine[]>> _lines;
        private static Dictionary<MultilureMode, Dictionary<int, MultilureDescription[]>> _descriptions;

        static Multilure()
        {
            Array modes = Enum.GetValues(typeof(MultilureMode));
            int size = modes.Length;

            _lines = new(size);
            _descriptions = new(size);
            _postThrows = new(size);

            foreach (MultilureMode mode in modes)
            {
                _lines.Add(mode, new Dictionary<int, MultilureLine[]>());
                _descriptions.Add(mode, new Dictionary<int, MultilureDescription[]>());
                _postThrows.Add(mode, new Dictionary<int, MultilurePostThrow>());
            }
        }

        internal static void SetPostThrow(MultilureMode mode, int itemId, MultilurePostThrow postThrow)
        {
            _postThrows[mode].Add(itemId, postThrow);
        }

        internal static MultilurePostThrow GetPostThrow(int itemId)
        {
            return _postThrows[BetterFishing.Configuration.MultilureMode].GetValueOrDefault(itemId, null);
        }

        internal static void SetLines(MultilureMode mode, int itemId, List<MultilureLine> line)
        {
            _lines[mode].Add(itemId, line.ToArray());
        }

        internal static MultilureLine[] GetLines(MultilureMode mode, int itemId)
        {
            return _lines[mode].GetValueOrDefault(itemId, null);
        }

        internal static MultilureLine[] GetLines(int itemId)
        {
            return _lines[BetterFishing.Configuration.MultilureMode].GetValueOrDefault(itemId, null);
        }

        internal static MultilureDescription[] GetDescription(int itemId)
        {
            return _descriptions[BetterFishing.Configuration.MultilureMode].GetValueOrDefault(itemId, null);
        }

        internal static void SetDescription(MultilureMode mode, int itemId, List<MultilureDescription> description)
        {
            _descriptions[mode].Add(itemId, description.ToArray());
        }

    }
}
