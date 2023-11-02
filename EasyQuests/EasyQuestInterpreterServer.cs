using BetterFishing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterFishing.EasyQuests
{
    public class EasyQuestInterpreterServer : EasyQuestInterpreter
    {
        protected static string DATA_TIMESTAMP_NOW => EasyQuestsSystem.DATA_TIMESTAMP_NOW;
        protected static string DATA_TIMESTAMP_NEXT => EasyQuestsSystem.DATA_TIMESTAMP_NEXT;

        protected double TimestampNow;
        protected double TimestampNext;

        public override bool ChangeQuestHook(On_Main.orig_AnglerQuestSwap orig)
        {
            // If not enough time has passed
            if (TimestampNow < TimestampNext)
                return false;

            // If should check players
            if (BetterFishing.Configuration.QuestPlayerPercent != 0)
            {
                // Counts the amount of players
                int players = EasyQuestUtils.CountPlayersOnServer();
                int completed = Main.anglerWhoFinishedToday.Count;

                int completedPercent = completed / players * 100;

                // If not enough players have completed the quest, it does not change
                if (completedPercent < BetterFishing.Configuration.QuestPlayerPercent)
                    return false;
            }

            orig();
            TimestampNext = EasyQuestUtils.CalculateNextTime();
            TimestampNow = 0;
            return true;
        }

        public override void Load(TagCompound tag)
        {
            tag.TryGet(DATA_TIMESTAMP_NOW, out TimestampNow);
            tag.TryGet(DATA_TIMESTAMP_NEXT, out TimestampNext);
        }

        public override void Notify()
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Netplay.Clients[i].State != 10)
                    continue;

                ModPacket packet = PacketHandler.Create(PacketID.ANGLER_QUEST, 0);
                packet.Send(i);
            }
        }

        public override void Save(TagCompound tag)
        {
            tag.Set(DATA_TIMESTAMP_NOW, TimestampNow);
            tag.Set(DATA_TIMESTAMP_NEXT, TimestampNext);
        }

        public override void Setup()
        {

        }

        public override bool UpdateTimer(double dayRate)
        {
            if (TimestampNow < TimestampNext)
            {
                TimestampNow += dayRate;
                return false;
            }

            return true;
        }

        public override double GetRemainingTime()
        {
            return TimestampNext - TimestampNow;
        }
    }
}
