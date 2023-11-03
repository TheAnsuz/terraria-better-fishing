using BetterFishing.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterFishing.EasyQuests
{
    internal class EasyQuestsSystem : ModSystem
    {
        internal const string DATA_TIMESTAMP_NOW = "timestamp_now";
        internal const string DATA_TIMESTAMP_NEXT = "timestamp_next";
        internal const string DATA_TIMESTAMP_OLD = "timestamp_old";
        internal const string DATA_TIMESTAMP_OLD_DAY = "timestamp_old_day";

        internal static EasyQuestInterpreter Interpreter;

        public override void Load()
        {
            On_Main.AnglerQuestSwap += CustomAnglerQuestSwap;
            PacketHandler.SetListener(PacketID.ANGLER_TIME_REQUEST, Packet_AnglerTimeRequest);
            PacketHandler.SetListener(PacketID.ANGLER_TIME_ANSWER, Packet_AnglerTimeAnswer);
            PacketHandler.SetListener(PacketID.ANGLER_QUEST, Packet_AnglerQuest);
        }


        private void Packet_AnglerTimeAnswer(BinaryReader reader, PacketType type, int senderIndex)
        {
            EasyQuestUtils.NotifyRemainingTime(reader.ReadDouble());
        }

        private void Packet_AnglerQuest(BinaryReader reader, PacketType type, int senderIndex)
        {
            Interpreter.Notify();
        }

        private void Packet_AnglerTimeRequest(BinaryReader reader, PacketType type, int senderIndex)
        {
            ModPacket packet = PacketHandler.Create(PacketID.ANGLER_TIME_ANSWER, sizeof(double));
            packet.Write(Interpreter.GetRemainingTime());
            packet.Send(reader.ReadByte());
        }

        public override void OnWorldLoad()
        {
            BetterFishing.Instance.Logger.Debug("World load");
            AssignInterpreter();
        }

        internal void AssignInterpreter()
        {
            Interpreter = null;

            switch (Main.netMode)
            {
                case NetmodeID.SinglePlayer:
                    Interpreter = new EasyQuestInterpreterSingleplayer();
                    break;

                case NetmodeID.MultiplayerClient:
                    Interpreter = new EasyQuestInterpreterMultiplayerClient();
                    break;

                case NetmodeID.Server:
                    Interpreter = new EasyQuestInterpreterServer();
                    break;
            }

            BetterFishing.Instance.Logger.Debug("Loaded " + Interpreter.ToString());
            Interpreter.Setup();
        }

        public override void OnWorldUnload()
        {
            BetterFishing.Instance.Logger.Debug("Unloaded " + Interpreter.ToString());
            Interpreter.Dispose();
            Interpreter = null;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            BetterFishing.Instance.Logger.Debug("Load world data");
            Interpreter.Load(tag);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            // When a new world is generated, the save is invoked
            // however there is no need to save contents as Load implements a default behavior when no tags are present
            if (Interpreter == null)
                return;

            Interpreter.Save(tag);
        }

        private void CustomAnglerQuestSwap(On_Main.orig_AnglerQuestSwap orig)
        {
            // This method is invoked whenever a world is generated
            if (Interpreter == null)
                return;

            if (Interpreter.ChangeQuestHook(orig))
                Interpreter.Notify();
        }

        public override void PostUpdateTime()
        {
            if (Interpreter.UpdateTimer(Main.dayRate))
                Main.AnglerQuestSwap();
        }
    }
}
