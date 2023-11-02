using BetterFishing.AnglerShop;
using BetterFishing.AnglerShop.SellCondition;
using BetterFishing.Multilure;
using BetterFishing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace BetterFishing.Compat
{
    public class VanillaCompat
    {

        public void TryEnable()
        {
            AddAnglerQuestRewards(AnglerCoinReward.Vanilla());
            LoadAnglerShop(AnglerShop.AnglerShop.Vanilla());
            LoadMultilure(MultilureRegistry.Vanilla());
        }

        protected void AddAnglerQuestRewards(AnglerCoinRewardVanilla Rewards)
        {
            int easy = 1;
            int normal = 2;
            int hard = 3;
            int hardmode = 2;

            Rewards.Add(ItemID.AmanitaFungifin).Amount(normal);
            Rewards.Add(ItemID.Angelfish).Amount(hard);
            Rewards.Add(ItemID.Batfish).Amount(easy);
            Rewards.Add(ItemID.BloodyManowar).Amount(normal);
            Rewards.Add(ItemID.Bonefish).Amount(normal);
            Rewards.Add(ItemID.BumblebeeTuna).Amount(hard);
            Rewards.Add(ItemID.Bunnyfish).Amount(easy);
            Rewards.Add(ItemID.CapnTunabeard).Amount(hardmode + easy);
            Rewards.Add(ItemID.Catfish).Amount(normal);
            Rewards.Add(ItemID.Cloudfish).Amount(hard);
            Rewards.Add(ItemID.Clownfish).Amount(normal);
            Rewards.Add(ItemID.Cursedfish).Amount(hardmode + normal);
            Rewards.Add(ItemID.DemonicHellfish).Amount(hard);
            Rewards.Add(ItemID.Derpfish).Amount(hardmode + normal);
            Rewards.Add(ItemID.Dirtfish).Amount(easy);
            Rewards.Add(ItemID.DynamiteFish).Amount(easy);
            Rewards.Add(ItemID.EaterofPlankton).Amount(normal);
            Rewards.Add(ItemID.FallenStarfish).Amount(easy);
            Rewards.Add(ItemID.TheFishofCthulu).Amount(easy);
            Rewards.Add(ItemID.Fishotron).Amount(normal);
            Rewards.Add(ItemID.Fishron).Amount(hardmode + normal);
            Rewards.Add(ItemID.GuideVoodooFish).Amount(normal);
            Rewards.Add(ItemID.Harpyfish).Amount(normal);
            Rewards.Add(ItemID.Hungerfish).Amount(hardmode + normal);
            Rewards.Add(ItemID.Ichorfish).Amount(hardmode + normal);
            Rewards.Add(ItemID.InfectedScabbardfish).Amount(normal);
            Rewards.Add(ItemID.Jewelfish).Amount(normal);
            Rewards.Add(ItemID.MirageFish).Amount(hardmode + normal);
            Rewards.Add(ItemID.Mudfish).Amount(normal);
            Rewards.Add(ItemID.MutantFlinxfin).Amount(normal);
            Rewards.Add(ItemID.Pengfish).Amount(normal);
            Rewards.Add(ItemID.Pixiefish).Amount(hardmode + normal);
            Rewards.Add(ItemID.ScarabFish).Amount(normal);
            Rewards.Add(ItemID.ScorpioFish).Amount(normal);
            Rewards.Add(ItemID.Slimefish).Amount(easy);
            Rewards.Add(ItemID.Spiderfish).Amount(normal);
            Rewards.Add(ItemID.TropicalBarracuda).Amount(normal);
            Rewards.Add(ItemID.TundraTrout).Amount(normal);
            Rewards.Add(ItemID.UnicornFish).Amount(hardmode + easy);
            Rewards.Add(ItemID.Wyverntail).Amount(hardmode + hard);
            Rewards.Add(ItemID.ZombieFish).Amount(easy);
        }

        protected void LoadAnglerShop(VanillaShop Shop)
        {
            // Equipamiento

            Shop.Sell(ItemID.AnglerHat)
                .Price(4)
                .AfterQuest(10);

            Shop.Sell(ItemID.AnglerVest)
                .Price(4)
                .AfterQuest(15);

            Shop.Sell(ItemID.AnglerPants)
                .Price(4)
                .AfterQuest(20);

            Shop.Sell(ItemID.FinWings)
                .Price(25)
                .AfterQuest(10)
                .WithCondition(SellCondition.Biome(BiomeUtils.SPACE))
                .EnforceCondition(SellCondition.Hardmode());

            // Cañas de pescar

            Shop.Sell(ItemID.GoldenFishingRod)
                .Price(35)
                .AfterQuest(30);

            Shop.Sell(ItemID.HotlineFishingHook)
                .Price(30)
                .AfterQuest(25)
                .WithCondition(SellCondition.Biome(BiomeUtils.UNDERWORLD))
                .EnforceCondition(SellCondition.Hardmode());


            // Cubos infinitos de liquidos

            Shop.Sell(ItemID.BottomlessBucket)
                .Price(15)
                .WithCondition(SellCondition.Not(SellCondition.Biome(BiomeUtils.BEEHIVE)))
                .AfterQuest(25);

            Shop.Sell(ItemID.BottomlessHoneyBucket)
                .Price(15)
                .WithCondition(SellCondition.Biome(BiomeUtils.BEEHIVE))
                .AfterQuest(25);

            // Esponjas superabsorventes de liquidos

            Shop.Sell(ItemID.SuperAbsorbantSponge)
                .Price(15)
                .WithCondition(SellCondition.Not(SellCondition.Biome(BiomeUtils.BEEHIVE)))
                .AfterQuest(10);

            Shop.Sell(ItemID.HoneyAbsorbantSponge)
                .Price(15)
                .WithCondition(SellCondition.Biome(BiomeUtils.BEEHIVE))
                .AfterQuest(10);

            // Objetos relativamente inutiles

            Shop.Sell(ItemID.FuzzyCarrot)
                .Price(2)
                .AfterQuest(5);

            Shop.Sell(ItemID.GoldenBugNet)
                .Price(5);

            Shop.Sell(ItemID.FishHook)
                .Price(5);

            Shop.Sell(ItemID.FishMinecart)
                .Price(5);

            // Objetos utiles

            Shop.Sell(ItemID.FishingBobber)
                .Price(10)
                .AfterQuest(1);

            Shop.Sell(ItemID.HighTestFishingLine)
                .Price(10)
                .AfterQuest(4);

            Shop.Sell(ItemID.AnglerEarring)
                .Price(10)
                .AfterQuest(7);

            Shop.Sell(ItemID.TackleBox)
                .Price(10)
                .AfterQuest(10);

            Shop.Sell(ItemID.WeatherRadio)
                .Price(10)
                .AfterQuest(13);

            Shop.Sell(ItemID.Sextant)
                .Price(10)
                .AfterQuest(16);

            Shop.Sell(ItemID.FishermansGuide)
                .Price(10)
                .AfterQuest(19);

            // Pociones y consumibles

            Shop.Sell(ItemID.FishingPotion)
                .PriceCoins(0, 1, 0, 0)
                .AfterQuest(2);

            Shop.Sell(ItemID.SonarPotion)
                .PriceCoins(0, 1, 25, 0)
                .AfterQuest(4);

            Shop.Sell(ItemID.CratePotion)
                .PriceCoins(0, 2, 25, 0)
                .AfterQuest(8);

            Shop.Sell(ItemID.ApprenticeBait)
                .PriceCoins(0, 0, 15, 0)
                .AfterQuest(1);

            Shop.Sell(ItemID.JourneymanBait)
                .PriceCoins(0, 0, 45, 0)
                .AfterQuest(11);

            Shop.Sell(ItemID.MasterBait)
                .PriceCoins(0, 0, 75, 0)
                .AfterQuest(19);
        }

        public void LoadMultilure(MultilureRegistry Reg)
        {
            // 2 simple, 2 normal
            Reg.Create(MultilureMode.SIMPLE, ItemID.WoodFishingPole)
                .AddLines(2, spread: 5)
                .AddTooltip("WoodFishingPole", 2)
                .FinishAlsoFor(MultilureMode.NORMAL);

            // 2 simple, 2 normal
            Reg.Create(MultilureMode.SIMPLE, ItemID.ReinforcedFishingPole)
                .AddLines(2, spread: 5)
                .AddTooltip("ReinforcedFishingPole", 2)
                .FinishAlsoFor(MultilureMode.NORMAL);

            // 2 simple, 2 + [0/2] normal
            Reg.Create(MultilureMode.SIMPLE, ItemID.FisherofSouls)
                .AddLines(2, spread: 5)
                .AddTooltip("FisherofSouls", 2)
                .FinishAnd(MultilureMode.NORMAL)
                .AddLines(2, spread: 5)
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.CORRUPTION))
                .AddTooltip("Fleshcatcher", 2, 2)
                .Finish();

            // 2 simple, 2 + [0/2] normal
            Reg.Create(MultilureMode.SIMPLE, ItemID.Fleshcatcher)
                .AddLines(2, spread: 5)
                .AddTooltip("Fleshcatcher", 2)
                .FinishAnd(MultilureMode.NORMAL)
                .AddLines(2, spread: 5)
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.CRIMSON))
                .AddTooltip("Fleshcatcher", 2, 2)
                .Finish();

            // 3 simple, 1 + [0-3] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.ScarabFishingRod)
                .AddLines(1)
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        if (player.statMana >= 40)
                        {
                            player.statMana -= 40;
                            CombatText.NewText(player.getRect(), CombatText.HealMana, -40);
                            return true;
                        }
                        return false;
                    }
                    )
                )
                .RepeatLast(2)
                .AddTooltip("ScarabFishingRod", 1, 1, 40, 3)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(3)
                .AddTooltip("ScarabFishingRod", 3)
                .Finish();

            // 4 simple, 1 + [0-4] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.BloodFishingRod)
                .AddLines(1)
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        if (player.statLife > 6)
                        {
                            player.statLife -= 6;
                            CombatText.NewText(player.getRect(), CombatText.LifeRegenNegative, 6);
                            return true;
                        }
                        return false;
                    })
                )
                .RepeatLast(3)
                .AddTooltip("BloodFishingRod", 1, 1, 6, 4)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(4)
                .AddTooltip("BloodFishingRod", 4)
                .Finish();

            // 7 simple, 5 + [0/5] + [0/10] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.GoldenFishingRod)
                .AddLines(5)
                .AddLines(5, MultilureCondition.Chance(20))
                .AddConsecutiveLines(10, MultilureCondition.Chance(25))
                .AddTooltip("GoldenFishingRod", 5, 5, 20, 10, 5)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(7)
                .AddTooltip("GoldenFishingRod", 7)
                .Finish();

            // 3 simple, 1 + [0/1] + [0-2] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.HotlineFishingHook)
                .AddLines(1)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.SNOW, BiomeUtils.SPACE)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.UNDERWORLD))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.DESERT))
                .AddTooltip("HotlineFishingHook", 1, 4)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(3)
                .AddTooltip("HotlineFishingHook", 3)
                .Finish();

            // [3-5] simple, [3-5] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.SittingDucksFishingRod)
                .AddLines(min: 3, max: 5)
                .AddTooltip("SittingDucksFishingRod", 3, 4)
                .FinishAlsoFor(MultilureMode.SIMPLE);

            // [3-4] simple, [3-4] + [0/1] normal
            Reg.Create(MultilureMode.NORMAL, ItemID.MechanicsRod)
                .AddLines(min: 3, max: 4)
                .AddLines(1, MultilureCondition.Custom(
                    (Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) =>
                    {
                        return !Main.IsItDay();
                    })
                )
                .AddTooltip("MechanicsRod", 2, 4, 1)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(min: 3, max: 4)
                .AddTooltip("MechanicsRod", 3, 4)
                .Finish();



            Reg.Create(MultilureMode.NORMAL, ItemID.FiberglassFishingPole)
                .AddLines(1)
                .AddLines(1, MultilureCondition.Not(MultilureCondition.Biome(BiomeUtils.DESERT, BiomeUtils.CORRUPTION, BiomeUtils.CRIMSON)))
                .AddLines(2, MultilureCondition.Biome(BiomeUtils.JUNGLE, BiomeUtils.MUSHROOM))
                .AddAlternativeLines(1, MultilureCondition.Biome(BiomeUtils.OCEAN, BiomeUtils.SNOW))
                .AddTooltip("FiberglassFishingPole", 1, 4)
                .FinishAnd(MultilureMode.SIMPLE)
                .AddLines(3)
                .AddTooltip("FiberglassFishingPole", 3)
                .Finish();
            /*
            */
        }
    }
}
