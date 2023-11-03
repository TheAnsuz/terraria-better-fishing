using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace BetterFishing.Multilure
{
    public class MultilureUtilities
    {

        internal MultilureUtilities() { }

        public static Vector2 CalculateSpread(Vector2 velocity, float spread)
        {
            return velocity + new Vector2(Main.rand.NextFloat(-spread, spread) * Multilure.SPREAD_MULTIPLIER, Main.rand.NextFloat(-spread, spread) * Multilure.SPREAD_MULTIPLIER);
        }

        public static Vector2 CalculateSpread(Vector2 velocity, float spreadX, float spreadY)
        {
            return velocity + new Vector2(Main.rand.NextFloat(-spreadX, spreadX), Main.rand.NextFloat(-spreadY, spreadY)) * Multilure.SPREAD_MULTIPLIER;
        }

        public static int CreateProjectile(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, Player player)
        {
            return Projectile.NewProjectile(source, position, speed, type, 0, 0f, player.whoAmI);
        }

        public static string GetDescriptionPath(string mod, string mode, string rod)
        {
            return $"{Multilure.LANGUAGE_PREFIX}.{mod}.{mode}.{rod}";
        }

        public static string GetDescription(string mod, string mode, string rod, params object[] args)
        {
            LocalizedText text = Language.GetOrRegister(GetDescriptionPath(mod, mode, rod));
            return text.Format(args);
        }
    }
}
