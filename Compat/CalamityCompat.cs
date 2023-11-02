using BetterFishing.AnglerShop;
using BetterFishing.AnglerShop.SellCondition;
using BetterFishing.Config.Model;
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

        // Rods
        public const string EarlyBloomRod = "EarlyBloomRod";
        public const string FeralDoubleRod = "FeralDoubleRod";
        public const string HeronRod = "HeronRod";
        public const string NavyFishingRod = "NavyFishingRod";
        public const string RiftReeler = "RiftReeler";
        public const string SlurperPole = "SlurperPole";
        public const string TheDevourerofCods = "TheDevourerofCods";
        public const string VerstaltiteFishingRod = "VerstaltiteFishingRod";
        public const string WulfrumFishingPole = "WulfrumRod";

        // Quest fish
        public const string Brimlish = "Brimlish";
        public const string Slurpfish = "Slurpfish";
        public const string EutrophicSandfish = "EutrophicSandfish";
        public const string Serpentuna = "Serpentuna";
        public const string SurfClam = "SurfClam";

        // Bait
        public const string GrandMarquisBait = "GrandMarquisBait";

        protected override void LoadMultilure(MultilureModRegistry Reg)
        {
            // 2 simple, 2 normal
            Reg.Create(MultilureMode.NORMAL, WulfrumFishingPole)
                .AddLines(2)
                .AddTooltip(WulfrumFishingPole, 2)
                .FinishAlsoFor(MultilureMode.SIMPLE);

            // [2-3] simple, [2-3] normal
            Reg.Create(MultilureMode.NORMAL, NavyFishingRod)
               .AddLines(min: 2, max: 3)
               .AddTooltip(NavyFishingRod, 2, 3)
               .FinishAlsoFor(MultilureMode.SIMPLE);

            // 4 simple, 4 normal
            Reg.Create(MultilureMode.NORMAL, FeralDoubleRod)
                .AddLines(4)
                .AddTooltip(FeralDoubleRod, 4)
                .RemoveTooltip("Tooltip0")
                .FinishAlsoFor(MultilureMode.SIMPLE);

            // 6 simple, 6 + [0/2] normal
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

            // 10 simple, 10 normal
            Reg.Create(MultilureMode.NORMAL, TheDevourerofCods)
                .AddLines(10)
                .ReplaceTooltip("Tooltip0", TheDevourerofCods)
                .AddTooltip(TheDevourerofCods, 10)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(10)
                .ReplaceTooltip("Tooltip0", TheDevourerofCods)
                .AddTooltip(TheDevourerofCods, 10)
                .Finish();

            // 2 simple, [0-3] normal
            Reg.Create(MultilureMode.NORMAL, SlurperPole)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.SNOW, BiomeUtils.SPACE)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.UNDERWORLD))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.DESERT))
                .AddTooltip(SlurperPole, 1, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(2)
                .AddTooltip(SlurperPole, 2)
                .Finish();

            // 2 simple, [0-3] normal
            Reg.Create(MultilureMode.NORMAL, VerstaltiteFishingRod)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.UNDERWORLD, BiomeUtils.DESERT)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.SNOW))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.JUNGLE, BiomeUtils.MUSHROOM))
                .AddTooltip(VerstaltiteFishingRod, 1, 2)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(2)
                .AddTooltip(VerstaltiteFishingRod, 2)
                .Finish();

            // 3 simple, 1 + [0-3] normal
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
                .AddTooltip(HeronRod, 1, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(3)
                .AddTooltip(HeronRod, 3)
                .Finish();

            // [3-5] simple, [2-4] + [0-3] normal
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
        }

        protected override void LoadAnglerShop(ModdedShop Shop)
        {
            Shop.Sell(GrandMarquisBait)
                .AfterQuest(30)
                .WithCondition(SellCondition.Biome(BiomeUtils.OCEAN))
                .PriceCoins(0, 1, 0, 0);
        }

        protected override void AddAnglerQuestRewards(AnglerCoinRewardModded Rewards)
        {
            int normal = 2;
            int hard = 3;
            int hardmode = 2;

            Rewards.Add(Brimlish).Amount(hardmode + hard);
            Rewards.Add(Slurpfish).Amount(normal);
            Rewards.Add(EutrophicSandfish).Amount(hardmode + normal);
            Rewards.Add(Serpentuna).Amount(hard);
            Rewards.Add(SurfClam).Amount(normal);
        }
    }
}
