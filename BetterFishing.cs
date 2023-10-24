using BetterFishing.Compat;
using BetterFishing.Config;
using BetterFishing.Config.Model;
using BetterFishing.Multilure;
using BetterFishing.Multilure.Condition;
using BetterFishing.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing
{
    public class BetterFishing : Mod
    {
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
            base.PostSetupContent();

            Vanilla.TryEnable();
            Calamity.TryEnable();
        }

        public override void Unload()
        {
            base.Unload();

            Vanilla.TryDisable();
            Calamity.TryDisable();
        }
    }
}