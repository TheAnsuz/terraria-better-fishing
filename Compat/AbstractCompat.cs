using BetterFishing.Multilure;
using Terraria.ModLoader;

namespace BetterFishing.Compat
{
    public abstract class AbstractCompat
    {
        public abstract string ModName { get; }

        public bool IsEnabled() => Mod != null;

        public Mod Mod;

        public void TryEnable()
        {
            if (ModLoader.TryGetMod(ModName, out Mod))
            {
                LoadMultilure(MultilureModRegistry.Modded(Mod));
            }
        }

        public void TryDisable()
        {
            if (!IsEnabled())
                return;
        }

        protected abstract void LoadMultilure(MultilureModRegistry Reg);
    }
}
