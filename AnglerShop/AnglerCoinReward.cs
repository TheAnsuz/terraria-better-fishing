using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    internal sealed class AnglerCoinReward
    {
        private readonly static AnglerCoinRewardEntry Default = new AnglerCoinRewardEntry(-1).Amount(1);
        protected internal static readonly Dictionary<int, AnglerCoinRewardEntry> _rewards = new Dictionary<int, AnglerCoinRewardEntry>();
        private static AnglerCoinRewardVanilla _vanilla = new AnglerCoinRewardVanilla();
        private AnglerCoinReward() { }

        public static AnglerCoinRewardVanilla Vanilla() => _vanilla;
        public static AnglerCoinRewardModded Modded(Mod Mod) => new AnglerCoinRewardModded(Mod);

        public static AnglerCoinRewardEntry GetReward(int id)
        {
            return _rewards.GetValueOrDefault(id, Default);
        }
    }

    public sealed class AnglerCoinRewardVanilla
    {

        public AnglerCoinRewardEntry Add(int itemId)
        {
            AnglerCoinRewardEntry entry = new AnglerCoinRewardEntry(itemId);
            AnglerCoinReward._rewards.Add(itemId, entry);
            return entry;
        }

    }

    public sealed class AnglerCoinRewardModded
    {
        private readonly Mod Mod;
        internal AnglerCoinRewardModded(Mod mod)
        {
            Mod = mod;
        }

        public AnglerCoinRewardEntry Add(string itemName)
        {
            if (Mod.TryFind(itemName, out ModItem item))
            {
                AnglerCoinRewardEntry entry = new AnglerCoinRewardEntry(item.Type);
                AnglerCoinReward._rewards.Add(item.Type, entry);
                return entry;
            }
            return null;
        }
    }
}
