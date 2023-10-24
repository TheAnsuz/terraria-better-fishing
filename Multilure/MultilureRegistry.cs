using BetterFishing.Config.Model;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace BetterFishing.Multilure
{
    public class MultilureRegistry
    {
        public static MultilureRegistry Vanilla()
        {
            if (_vanilla == null)
                _vanilla = new MultilureRegistry();

            return _vanilla;
        }

        private static MultilureRegistry _vanilla;

        private MultilureRegistry() { }

        public MultilureEntry Create(MultilureMode mode, short id)
        {
            return new MultilureEntry("Vanilla", mode, id);
        }

    }
    public class MultilureModRegistry
    {
        private readonly static Dictionary<string, MultilureModRegistry> _modded = new();

        public static MultilureModRegistry Modded(Mod mod)
        {
            if (!_modded.ContainsKey(mod.Name))
            {
                _modded.Add(mod.Name, new MultilureModRegistry(mod));
            }

            return _modded[mod.Name];
        }

        private readonly Mod _mod;

        private MultilureModRegistry(Mod mod)
        {
            _mod = mod;
        }

        public MultilureEntry Create(MultilureMode mode, string itemName)
        {
            if (_mod.TryFind(itemName, out ModItem item))
                return new MultilureEntry(_mod.Name, mode, item.Type);

            BetterFishing.Instance.Logger.Error($"Failed to create multilure entry for {_mod.Name}'s {itemName}");
            return new MultilureEntry("ERROR", mode, -1);
        }
    }
}
