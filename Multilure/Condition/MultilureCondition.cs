using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static BetterFishing.Multilure.Condition.MultilureCustomCondition;

namespace BetterFishing.Multilure.Condition
{
    public abstract class MultilureCondition
    {
        internal abstract bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback);

        public static MultilureCondition And(params MultilureCondition[] conditions)
        {
            return new MultilureANDCondition(conditions);
        }

        public static MultilureCondition Or(params MultilureCondition[] conditions)
        {
            return new MultilureORCondition(conditions);
        }

        public static MultilureCondition Biome(params int[] biome) => new MultilureBiomeCondition(biome, new ModBiome[0]);
        public static MultilureCondition Biome(params ModBiome[] biome) => new MultilureBiomeCondition(new int[0], biome);
        public static MultilureCondition Biome(ModBiome[] modBiome, int[] biome) => new MultilureBiomeCondition(biome, modBiome);

        public static MultilureCondition Chance(float chance, float max = 100f)
        {
            return new MultilureChanceCondition(chance, max);
        }

        public static MultilureCondition Custom(Match match) => new MultilureCustomCondition(match);

        public static MultilureCondition Not(MultilureCondition condition) => new MultilureNOTCondition(condition);

        public static MultilureCondition True()
        {
            return new MultilureTRUECondition();
        }
    }
}
