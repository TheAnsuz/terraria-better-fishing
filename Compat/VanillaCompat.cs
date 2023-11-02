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

        protected void LoadMultilure(MultilureRegistry Reg)
        {
        }
    }
}
