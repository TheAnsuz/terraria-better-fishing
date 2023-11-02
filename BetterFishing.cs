using BetterFishing.AnglerShop;
using BetterFishing.Compat;
using BetterFishing.Config;
using BetterFishing.Config.Model;
using BetterFishing.EasyQuests;
using BetterFishing.Multilure;
using BetterFishing.Multilure.Condition;
using BetterFishing.Util;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing
{
    public class BetterFishing : Mod
    {
        public const string LANGUAGE_PATH = "Mods.BetterFishing";
        public const string ASSETS_PATH = "BetterFishing/Assets";

        public readonly static ModConfiguration Configuration = ModContent.GetInstance<ModConfiguration>();

        public static BetterFishing Instance;
        public static CalamityCompat Calamity;
        public static VanillaCompat Vanilla;

        public override void Load()
        {
            Instance = this;

            Vanilla = new VanillaCompat();
            Calamity = new CalamityCompat();
        }

        public override void PostSetupContent()
        {
            Vanilla.TryEnable();
            Calamity.TryEnable();
        }

        public override void Unload()
        {
            Vanilla.TryDisable();
            Calamity.TryDisable();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) => PacketHandler.Invoke(reader, whoAmI);

    }
}