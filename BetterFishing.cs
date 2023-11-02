using BetterFishing.AnglerShop;
using BetterFishing.Compat;
using BetterFishing.Config;
using BetterFishing.Config.Model;
using BetterFishing.EasyQuests;
using BetterFishing.Hooks;
using BetterFishing.Multilure;
using BetterFishing.Multilure.Condition;
using BetterFishing.Util;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing
{
    public class BetterFishing : Mod
    {
        public const byte PACKET_ANGLER_QUEST = 1;
        public const byte PACKET_ANGLER_TIME_REQUEST = 2;
        public const byte PACKET_ANGLER_TIME_ANSWER = 3;

        public const string LANGUAGE_BASE = "Mods.BetterFishing";
        public const string ASSETS_BASE = "BetterFishing/Assets";

        public readonly static ModConfiguration Configuration = ModContent.GetInstance<ModConfiguration>();

        public static BetterFishing Instance;
        public static CalamityCompat Calamity;
        public static VanillaCompat Vanilla;
        public static bool Errors = false;

        public override void Load()
        {
            Instance = this;

            Vanilla = new VanillaCompat();
            Calamity = new CalamityCompat();
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            LoadMultilure(MultilureRegistry.Vanilla());

            Vanilla.TryEnable();
            Calamity.TryEnable();
        }

        public override void Unload()
        {
            base.Unload();
            Calamity.TryDisable();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case PACKET_ANGLER_QUEST:
                    EasyQuestsSystem.Interpreter.Notify();
                    break;
                case PACKET_ANGLER_TIME_REQUEST:
                    ModPacket packet = GetPacket();
                    packet.Write(PACKET_ANGLER_TIME_ANSWER);
                    packet.Write(EasyQuestsSystem.Interpreter.GetRemainingTime());
                    packet.Send(reader.ReadByte());
                    break;
                case PACKET_ANGLER_TIME_ANSWER:
                    EasyQuestUtils.NotifyRemainingTime(reader.ReadDouble());
                    break;
            }
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
                    })
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

        public void AjustShop(VanillaShop shop)
        {

        }

        internal void AnglerRewards(AnglerCoinRewardVanilla Rewards)
        {
        }
    }
}