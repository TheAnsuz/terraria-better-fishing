﻿using BetterFishing.Config.Model;
using BetterFishing.Multilure;
using BetterFishing.Multilure.Condition;
using BetterFishing.Util;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Compat
{
    public class CalamityCompat : AbstractCompat
    {
        public override string ModName => "CalamityMod";

        public const string EarlyBloomRod = "EarlyBloomRod";
        public const string FeralDoubleRod = "FeralDoubleRod";
        public const string HeronRod = "HeronRod";
        public const string NavyFishingRod = "NavyFishingRod";
        public const string RiftReeler = "RiftReeler";
        public const string SlurperPole = "SlurperPole";
        public const string TheDevourerofCods = "TheDevourerofCods";
        public const string VerstaltiteFishingRod = "VerstaltiteFishingRod";
        public const string WulfrumFishingPole = "WulfrumRod";

        protected override void LoadMultilure(MultilureModRegistry Reg)
        {
            Reg.Create(MultilureMode.NORMAL, WulfrumFishingPole)
                .AddLines(1)
                .AddTooltip(WulfrumFishingPole, 1)
                .FinishAlsoFor(MultilureMode.SIMPLE);

            Reg.Create(MultilureMode.NORMAL, NavyFishingRod)
               .AddLines(min: 2, max: 3)
               .AddTooltip(NavyFishingRod, 2, 3)
               .FinishAlsoFor(MultilureMode.SIMPLE);

            Reg.Create(MultilureMode.NORMAL, FeralDoubleRod)
                .AddLines(4)
                .AddTooltip(FeralDoubleRod, 4)
                .RemoveTooltip("Tooltip0")
                .FinishAlsoFor(MultilureMode.SIMPLE);

            Reg.Create(MultilureMode.NORMAL, EarlyBloomRod)
                .AddLines(6)
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.JUNGLE))
                .AddTooltip(EarlyBloomRod, 6, 2)
                .RemoveTooltip("Tooltip0")
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(6)
                .AddTooltip(EarlyBloomRod, 6)
                .RemoveTooltip("Tooltip0")
                .Finish();

            Reg.Create(MultilureMode.NORMAL, TheDevourerofCods)
                .AddLines(10)
                .ReplaceTooltip("Tooltip0", TheDevourerofCods)
                .AddTooltip(TheDevourerofCods, 10)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(10)
                .ReplaceTooltip("Tooltip0", TheDevourerofCods)
                .AddTooltip(TheDevourerofCods, 10)
                .Finish();

            Reg.Create(MultilureMode.NORMAL, SlurperPole)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.SNOW, BiomeUtils.SPACE)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.UNDERWORLD))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.DESERT))
                .AddTooltip(SlurperPole, 1, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(2)
                .AddTooltip(SlurperPole, 3)
                .Finish();

            Reg.Create(MultilureMode.NORMAL, VerstaltiteFishingRod)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.UNDERWORLD, BiomeUtils.DESERT)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.SNOW))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.JUNGLE, BiomeUtils.MUSHROOM))
                .AddTooltip(VerstaltiteFishingRod, 1, 2)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(2)
                .AddTooltip(VerstaltiteFishingRod, 2)
                .Finish();

            Reg.Create(MultilureMode.NORMAL, HeronRod)
                .AddLines(1)
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        return Math.Abs(WorldUtils.Wind) > 24;
                    })
                )
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        return Math.Abs(WorldUtils.Wind) >= 30;
                    })
                )
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        return Math.Abs(WorldUtils.Wind) > 18;
                    })
                )
                .AddTooltip(HeronRod, 2, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(2)
                .AddTooltip(HeronRod, 3)
                .Finish();

            Reg.Create(MultilureMode.NORMAL, RiftReeler)
                .AddLines(min: 2, max: 4)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.SNOW, BiomeUtils.SPACE)))
                .AddLines(3, MultilureCondition.Biome(BiomeUtils.UNDERWORLD))
                .AddAlternativeLines(2, MultilureCondition.Biome(BiomeUtils.DESERT))
                .RemoveTooltip("Tooltip0")
                .AddTooltip(RiftReeler, 2, 4, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(min: 3, max: 5)
                .RemoveTooltip("Tooltip0")
                .AddTooltip(RiftReeler, 3, 5)
                .Finish();
            /*

            Reg.Create(MultilureMode.NORMAL, TheDevourerofCods)
                .AddLines(2)
                .AddTooltip(TheDevourerofCods, 2)
                .RemoveTooltip("Tooltip0")
                .RemoveTooltip("Tooltip1")
                .FinishAlsoFor(MultilureMode.SIMPLE);

            /*
            Multilure.Item(WulfrumFishingPole).
                SetMultilure(MultilureShootProvider.RangedAmount(1, 2)).
                SetDescription(MultilureDescriptionProvider.AddDescription(Mod, WulfrumFishingPole, "1", "2"));
            */
            //Multilure.Register(EarlyBloomRod).Bobbler_FixedAmount(100).Description_Simple(EarlyBloomRod, 100);

            /*
             * Le pasas un nombre de registro
             * Del cual obtiene el ModItem y la id
             * El metodo SimpleDescription usará por defecto el nombre que le pasas en el registro
             * 
             * Multilure.Register(name).SimpleBobbler(bobblers: 5).SimpleDescription();
             * 
             *  * Usar bloque 'using'
             * 
             * 
             * 
             * Obtener datos:
             * Multilure.Get(id).BobblerProvider/DescriptionProvider
             */
        }
    }
}
