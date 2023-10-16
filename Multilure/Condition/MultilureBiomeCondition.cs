using BetterFishing.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BetterFishing.Multilure.Condition
{
    internal class MultilureBiomeCondition : MultilureCondition
    {
        private readonly int[] _biomes;
        private readonly ModBiome[] _modBiomes;

        internal MultilureBiomeCondition(int[] biomes, ModBiome[] modBiomes)
        {
            this._biomes = biomes;
            this._modBiomes = modBiomes;
        }

        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return BiomeUtils.InAnyBiome(player, _biomes) && BiomeUtils.InAnyBiome(player, _modBiomes);
        }
    }
}
