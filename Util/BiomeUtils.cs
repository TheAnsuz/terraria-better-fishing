using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.Util
{
    public class BiomeUtils
    {
        private BiomeUtils() { }

        // Height based
        public const int SPACE = 5;
        public const int SURFACE = 4;
        public const int UNDERGROUND = 3;
        public const int CAVERN = 2;
        public const int UNDERWORLD = 1;
        // Type based
        public const int FOREST = 101;
        public const int SNOW = 102;
        public const int SNOW_UNDERGROUND = 112;
        public const int DESERT = 103;
        public const int DESERT_UNDERGROUND = 113;

        // Faltan todas las variantes de desierto y nieve encantadas, corruptas y crimson
        public const int DESERT_CORRUPTION = 123;
        public const int DESERT_CORRUPTION_UNDERGROUND = 133;
        public const int DESERT_CRIMSON = 143;
        public const int DESERT_CRIMSON_UNDERGROUND = 153;
        public const int DESERT_HALLOWED = 163;
        public const int DESERT_HALLOWED_UNDERGROUND = 173;

        public const int SNOW_CORRUPTION = 122;
        public const int SNOW_CORRUPTION_UNDERGROUND = 132;
        public const int SNOW_CRIMSON = 142;
        public const int SNOW_CRIMSON_UNDERGROUND = 152;
        public const int SNOW_HALLOWED = 162;
        public const int SNOW_HALLOWED_UNDERGROUND = 172;

        public const int CORRUPTION = 104;
        public const int CORRUPTION_UNDERGROUND = 114;
        public const int CRIMSON = 105;
        public const int CRIMSON_UNDERGROUND = 115;
        public const int JUNGLE = 106;
        public const int JUNGLE_UNDERGROUND = 116;
        public const int OCEAN = 107;
        public const int MUSHROOM = 108;
        public const int MUSHROOM_UNDERGROUND = 118;
        public const int HALLOWED = 109;
        public const int HALLOWED_UNDERGROUND = 119;
        // Special based
        public const int DUNGEON = 50;

        public static bool InBiome(Player player, ModBiome biome)
        {
            return player.InModBiome(biome);
        }

        public static bool InAnyBiome(Player player, params ModBiome[] biomes)
        {
            if (biomes.Length == 0) return true;

            foreach (ModBiome biome in biomes)
                if (InBiome(player, biome)) return true;

            return false;
        }

        public static bool InBiome(Player player, int biome)
        {
            return biome switch
            {
                SPACE => player.ZoneSkyHeight,
                SURFACE => player.ZoneOverworldHeight,
                UNDERGROUND => player.ZoneDirtLayerHeight,
                CAVERN => player.ZoneRockLayerHeight,
                UNDERWORLD => player.ZoneUnderworldHeight,

                FOREST => player.ZoneForest,

                SNOW => player.ZoneSnow,
                SNOW_UNDERGROUND => player.ZoneSnow && player.ZoneUnderworldHeight,
                SNOW_CORRUPTION => player.ZoneSnow && player.ZoneCorrupt,
                SNOW_CORRUPTION_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneCorrupt && player.ZoneSnow,
                SNOW_CRIMSON => player.ZoneSnow && player.ZoneCrimson,
                SNOW_CRIMSON_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneCrimson && player.ZoneSnow,
                SNOW_HALLOWED => player.ZoneSnow && player.ZoneHallow,
                SNOW_HALLOWED_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneHallow && player.ZoneSnow,

                DESERT => player.ZoneDesert,
                DESERT_UNDERGROUND => player.ZoneDesert && player.ZoneUnderworldHeight,
                DESERT_CORRUPTION => player.ZoneDesert && player.ZoneCorrupt,
                DESERT_CORRUPTION_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneCorrupt && player.ZoneDesert,
                DESERT_CRIMSON => player.ZoneDesert && player.ZoneCrimson,
                DESERT_CRIMSON_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneCrimson && player.ZoneDesert,
                DESERT_HALLOWED => player.ZoneDesert && player.ZoneHallow,
                DESERT_HALLOWED_UNDERGROUND => player.ZoneUnderworldHeight && player.ZoneHallow && player.ZoneDesert,

                CORRUPTION => player.ZoneCorrupt,
                CORRUPTION_UNDERGROUND => player.ZoneCorrupt && player.ZoneUnderworldHeight,

                CRIMSON => player.ZoneCrimson,
                CRIMSON_UNDERGROUND => player.ZoneCrimson && player.ZoneUnderworldHeight,

                JUNGLE => player.ZoneJungle,
                JUNGLE_UNDERGROUND => player.ZoneJungle && player.ZoneUnderworldHeight,

                OCEAN => player.ZoneBeach,

                MUSHROOM => player.ZoneGlowshroom,
                MUSHROOM_UNDERGROUND => player.ZoneGlowshroom && player.ZoneUnderworldHeight,

                HALLOWED => player.ZoneHallow,
                HALLOWED_UNDERGROUND => player.ZoneHallow && player.ZoneUnderworldHeight,
                _ => false
            };
        }

        public static bool InAnyBiome(Player player, params int[] biomes)
        {
            if (biomes.Length == 0) return true;

            foreach (int biome in biomes)
                if (InBiome(player, biome)) return true;

            return false;
        }
    }
}
