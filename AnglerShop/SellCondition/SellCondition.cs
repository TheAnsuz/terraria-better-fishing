using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop.SellCondition
{
    public abstract class SellCondition
    {
        private static readonly ModBiome[] EMPTY_MOD_BIOME = new ModBiome[0];
        private static readonly int[] EMPTY_BIOME = new int[0];

        private static readonly SellCondition _true = new TrueSellCondition();
        private static readonly SellCondition _hardmode = new HardmodeSellCondition();
        private static readonly SellCondition _daytime = new DaytimeSellCondition();

        public static SellCondition True() => _true;
        public static SellCondition Or(SellCondition first, SellCondition second) => new OrSellCondition(first, second);
        public static SellCondition And(SellCondition first, SellCondition second) => new AndSellCondition(first, second);
        public static SellCondition Not(SellCondition condition) => new NotSellCondition(condition);
        public static SellCondition Hardmode() => _hardmode;
        public static SellCondition Daytime() => _daytime;
        public static SellCondition Biome(params int[] biomes) => new BiomeSellCondition(biomes, EMPTY_MOD_BIOME);
        public static SellCondition Biome(params ModBiome[] biomes) => new BiomeSellCondition(EMPTY_BIOME, biomes);
        public static SellCondition Biome(int[] biomes, ModBiome[] modBiomes) => new BiomeSellCondition(biomes, modBiomes);

        public abstract bool Acomplishes(NPC npc, Player player, Item[] items);
    }
}
