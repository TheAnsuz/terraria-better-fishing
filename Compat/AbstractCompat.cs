using BetterFishing.AnglerShop;
using BetterFishing.Multilure;
using Terraria.ModLoader;

namespace BetterFishing.Compat
{
    public abstract class AbstractCompat
    {
        public abstract string ModName { get; }

        public bool IsEnabled() => Mod != null;

        public Mod Mod;

        public virtual void TryEnable()
        {
            if (ModLoader.TryGetMod(ModName, out Mod))
            {
                LoadMultilure(MultilureModRegistry.Modded(Mod));
                LoadAnglerShop(AnglerShop.AnglerShop.Modded(Mod));
                AddAnglerQuestRewards(AnglerCoinReward.Modded(Mod));
            }
        }

        public virtual void TryDisable()
        {
            if (!IsEnabled())
                return;
        }
        protected abstract void AddAnglerQuestRewards(AnglerCoinRewardModded Rewards);

        protected abstract void LoadAnglerShop(ModdedShop Shop);

        protected abstract void LoadMultilure(MultilureModRegistry Reg);
    }
}
