using BetterFishing.Config.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    public sealed class AnglerShop
    {
        private static readonly Dictionary<int, AnglerShopEntry> _registry;
        private static readonly VanillaShop _vanilla;

        static AnglerShop()
        {
            _registry = new();

            _vanilla = new VanillaShop();
        }

        public static VanillaShop Vanilla() => _vanilla;

        public static ModdedShop Modded(Mod mod)
        {
            return new ModdedShop(mod);
        }

        public static bool IsOpen { get; private set; }
        public static readonly int LineMin = 0;
        public static int CalculatedMaxLine = 0;
        private static int _line;
        public static int Line { get { return _line; } set { _line = Math.Max(Math.Min(CalculatedMaxLine, value), LineMin); } }

        public static bool CanScroll(int value)
        {
            if (_line + value < LineMin) return false;

            if (_line + value > CalculatedMaxLine) return false;

            return true;
        }

        public static int GetEntrySize() => _registry.Count;

        public static KeyValuePair<int, AnglerShopEntry> GetEntryByIndex(int index)
        {
            return _registry.ElementAt(index);
        }

        public static AnglerShopEntry GetEntry(int id)
        {
            return _registry.GetValueOrDefault(id, null);
        }

        public static void SetEntry(int id, AnglerShopEntry entry)
        {
            if (!_registry.TryAdd(id, entry))
                BetterFishing.Instance.Logger.Warn($"Tried to register itemID {id} but already exists at the shop");
        }

        internal static void OpenAnglerShop(int line = 0)
        {
            if (Main.LocalPlayer.talkNPC == -1)
                return;

            Line = line;
            Main.playerInventory = true;
            Main.npcChatText = string.Empty;
            Main.SetNPCShopIndex(1); // dunno what the 1 does but its needed
            Main.instance.shop[Main.npcShop].SetupShop("BetterFishing/Angler/Shop", Main.LocalPlayer.TalkNPC);
            SoundEngine.PlaySound(SoundID.MenuTick);
            IsOpen = true;
        }

        public static void CloseAnglerShop()
        {
            if (!IsOpen)
                return;

            IsOpen = false;
            Main.SetNPCShopIndex(0);
        }
    }

    public sealed class VanillaShop
    {
        public AnglerShopEntry Sell(int id)
        {
            AnglerShopEntry entry = new AnglerShopEntry(id);
            AnglerShop.SetEntry(id, entry);
            return entry;
        }
    }

    public sealed class ModdedShop
    {
        private readonly Mod Mod;

        internal ModdedShop(Mod mod)
        {
            this.Mod = mod;
        }

        public AnglerShopEntry Sell(string itemName)
        {
            if (Mod.TryFind(itemName, out ModItem item))
            {
                AnglerShopEntry entry = new AnglerShopEntry(item.Type);
                AnglerShop.SetEntry(item.Type, entry);
                return entry;
            }
            return null;
        }
    }
}
