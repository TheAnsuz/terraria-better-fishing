using BetterFishing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop.SellCondition
{
    public class BiomeSellCondition : SellCondition
    {
        private readonly int[] _biomes;
        private readonly ModBiome[] _modBiomes;

        internal BiomeSellCondition(int[] biomes, ModBiome[] modBiomes)
        {
            _biomes = biomes;
            _modBiomes = modBiomes;
        }

        public override bool Acomplishes(NPC npc, Player player, Item[] items)
        {
            return BiomeUtils.InAnyBiome(player, _biomes) || BiomeUtils.InAnyBiome(player, _modBiomes);
        }
    }
}
