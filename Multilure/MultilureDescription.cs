using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.Multilure
{
    public class MultilureDescription
    {
        public const int MODE_ADD = 0;
        public const int MODE_REMOVE = 1;
        public const int MODE_REPLACE = 2;

        private int _mode;
        public readonly string Mod;
        public readonly string Name;
        private readonly string _languageKey;
        private GameCulture _culture;
        private TooltipLine _cachedTooltip;
        private readonly object[] _args;

        public bool IsAddition
        {
            get => _mode == MODE_ADD;
            set => _mode = MODE_ADD;
        }

        public bool IsReplacement
        {
            get => _mode == MODE_REPLACE;
            set => _mode = MODE_REPLACE;
        }

        public bool IsRemoval
        {
            get => _mode == MODE_REMOVE;
            set => _mode = MODE_REMOVE;
        }

        public MultilureDescription(string mod, string name, string languageKey, int mode, params object[] args)
        {
            _mode = mode;
            Mod = mod;
            Name = name;
            _languageKey = languageKey;
            _args = args;
        }

        public TooltipLine Tooltip()
        {
            if (_culture != Language.ActiveCulture)
                _cachedTooltip = null;

            if (_cachedTooltip == null)
            {
                string tooltip = Language.GetOrRegister(_languageKey).Format(_args);
                _cachedTooltip = new TooltipLine(BetterFishing.Instance, Name, (_mode == MODE_ADD ? Multilure.DESCRIPTION_PREFIX : "") + tooltip);
                _culture = Language.ActiveCulture;
            }

            return _cachedTooltip;
        }

    }
}
