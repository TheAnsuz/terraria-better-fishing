using BetterFishing.Config.Model;
using BetterFishing.Multilure.Condition;
using BetterFishing.Util;
using System.Collections.Generic;
using Terraria.Localization;

namespace BetterFishing.Multilure
{
    public class MultilureEntry
    {
        private readonly MultilureMode _mode;
        private readonly string _modName;
        private readonly int _id;
        private readonly List<MultilureLine> _lines = new();
        private readonly List<MultilureDescription> _descriptions = new();
        //private MultilurePostThrow _postThrow;

        internal MultilureEntry(string mod, MultilureMode mode, int id)
        {
            _modName = mod;
            _id = id;
            _mode = mode;
        }

        private MultilureEntry(MultilureMode mode, string mod, int id, List<MultilureLine> lines, List<MultilureDescription> descriptions)
        {
            _modName = mod;
            _mode = mode;
            _id = id;
            _lines = lines;
            _descriptions = descriptions;
        }

        public MultilureEntry AddTooltip(string key, params object[] args)
        {
            if (_id == -1)
                return this;

            string path = MultilureUtilities.GetDescriptionPath(_modName, _mode.ToString(), key);

            if (!Language.Exists(path))
            {
                BetterFishing.Instance.Logger.Error($"Failed to get multilure language entry for {key} from {_modName}");
                Language.GetOrRegister(path);
            }

            MultilureDescription description = new MultilureDescription(_modName, Multilure.TOOLTIP_DEFINITION, path, MultilureDescription.MODE_ADD, args);
            _descriptions.Add(description);
            return this;
        }

        public MultilureEntry ReplaceTooltip(string tooltipName, string key, params object[] args)
        {
            if (_id == -1)
                return this;

            string path = MultilureUtilities.GetDescriptionPath(_modName, "REPLACE", key);

            if (!Language.Exists(path))
            {
                BetterFishing.Instance.Logger.Error($"Failed to get multilure language entry for {key} from {_modName}");
                Language.GetOrRegister(path);
            }

            MultilureDescription description = new MultilureDescription("Terraria", tooltipName, path, MultilureDescription.MODE_REPLACE, args);
            _descriptions.Add(description);
            return this;
        }

        public MultilureEntry RemoveTooltip(string tooltipName) => RemoveTooltip("Terraria", tooltipName);

        public MultilureEntry RemoveTooltip(string mod, string tooltipName)
        {
            if (_id == -1)
                return this;

            MultilureDescription description = new MultilureDescription(mod, tooltipName, "", MultilureDescription.MODE_REMOVE);
            _descriptions.Add(description);
            return this;
        }

        public MultilureEntry RepeatLast(int times)
        {
            if (_lines.Count == 0)
                return this;

            MultilureLine line = _lines[_lines.Count - 1];

            for (int i = 0; i < times; i++)
                _lines.Add(line);

            return this;
        }

        public MultilureEntry AddLines(int lines, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(lines, lines, MultilureCondition.True(), spread, MultilureLine.MODE_NORMAL);

        public MultilureEntry AddLines(int min, int max, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(min, max, MultilureCondition.True(), spread, MultilureLine.MODE_NORMAL);

        public MultilureEntry AddLines(int lines, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(lines, lines, condition, spread, MultilureLine.MODE_NORMAL);

        public MultilureEntry AddLines(int min, int max, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(min, max, condition, spread, MultilureLine.MODE_NORMAL);

        public MultilureEntry AddConsecutiveLines(int lines, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(lines, lines, MultilureCondition.True(), spread, MultilureLine.MODE_CONSECUTIVE);

        public MultilureEntry AddConsecutiveLines(int min, int max, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(min, max, MultilureCondition.True(), spread, MultilureLine.MODE_CONSECUTIVE);

        public MultilureEntry AddConsecutiveLines(int lines, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(lines, lines, condition, spread, MultilureLine.MODE_CONSECUTIVE);

        public MultilureEntry AddConsecutiveLines(int min, int max, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
             Add(min, max, condition, spread, MultilureLine.MODE_CONSECUTIVE);

        public MultilureEntry AddAlternativeLines(int lines, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(lines, lines, MultilureCondition.True(), spread, MultilureLine.MODE_ALTERNATIVE);

        public MultilureEntry AddAlternativeLines(int min, int max, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(min, max, MultilureCondition.True(), spread, MultilureLine.MODE_ALTERNATIVE);

        public MultilureEntry AddAlternativeLines(int lines, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(lines, lines, condition, spread, MultilureLine.MODE_ALTERNATIVE);

        public MultilureEntry AddAlternativeLines(int min, int max, MultilureCondition condition, int spread = Multilure.DEFAULT_SPREAD) =>
            Add(min, max, condition, spread, MultilureLine.MODE_ALTERNATIVE);

        /*
        public MultilureEntry PostThrow(MultilurePostThrow postThrow)
        {
            _postThrow = postThrow;
            return this;
        }
        */

        private MultilureEntry Add(int min, int max, MultilureCondition condition, int spread, short mode)
        {
            MultilureLine line = new MultilureLine(min, max, spread, condition);

            switch (mode)
            {
                case MultilureLine.MODE_CONSECUTIVE:
                    line.IsConsecutive = true;
                    break;
                case MultilureLine.MODE_ALTERNATIVE
                    :
                    line.IsAlternative = true;
                    break;
            }

            _lines.Add(line);

            return this;
        }

        public void Finish()
        {
            if (_id == -1)
            {
                return;
            }
            /*
            LocalizedText localizedText = Language.GetOrRegister(MultilureUtilities.GetDescriptionPath(_mod, _mode.ToString(), _descriptionKey));

            TooltipLine tooltip = new(
                BetterFishing.Instance,
                Multilure.TOOLTIP_DEFINITION,
                Multilure.DESCRIPTION_PREFIX + localizedText.Format(_args)
                );
            */
            //Multilure.SetPostThrow(_mode, _id, _postThrow);
            Multilure.SetDescription(_mode, _id, _descriptions);
            Multilure.SetLines(_mode, _id, _lines);
        }

        public void FinishAlsoFor(MultilureMode mode)
        {
            Finish();
            new MultilureEntry(mode, _modName, _id, _lines, _descriptions).Finish();
        }

        public MultilureEntry FinishAnd(MultilureMode mode)
        {
            Finish();
            return new MultilureEntry(_modName, mode, _id);
        }


    }
}
