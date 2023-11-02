using BetterFishing.AnglerShop;
using BetterFishing.Compat;
using BetterFishing.Config;
using BetterFishing.Config.Model;
using BetterFishing.EasyQuests;
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

        public override void Load()
        {
            Instance = this;

            Vanilla = new VanillaCompat();
            Vanilla = new VanillaCompat();
            Calamity = new CalamityCompat();
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            Vanilla.TryEnable();
            Calamity.TryEnable();
        }

        public override void Unload()
        {
            base.Unload();

            Vanilla.TryDisable();
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

    }
}