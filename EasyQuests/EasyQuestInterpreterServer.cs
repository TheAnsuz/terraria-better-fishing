using BetterFishing.Util;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterFishing.EasyQuests
{
    public class EasyQuestInterpreterServer : EasyQuestInterpreter
    {
        protected static string DATA_TIMESTAMP_NOW => EasyQuestsSystem.DATA_TIMESTAMP_NOW;
        protected static string DATA_TIMESTAMP_NEXT => EasyQuestsSystem.DATA_TIMESTAMP_NEXT;
        protected static string DATA_TIMESTAMP_OLD => EasyQuestsSystem.DATA_TIMESTAMP_OLD;
        protected static string DATA_TIMESTAMP_OLD_DAY => EasyQuestsSystem.DATA_TIMESTAMP_OLD_DAY;

        protected double TimestampNow;
        protected double TimestampNext;
        private double TimestampOld;
        private bool TimestampOldDay;

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
            tag.TryGet(DATA_TIMESTAMP_OLD, out TimestampOld);
            tag.TryGet(DATA_TIMESTAMP_OLD_DAY, out TimestampOldDay);
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
            tag.Set(DATA_TIMESTAMP_OLD, TimestampOld);
            tag.Set(DATA_TIMESTAMP_OLD_DAY, TimestampOldDay);
        }

        public override void Setup()
        {
            TimestampNow = 0;
            TimestampNext = EasyQuestUtils.CalculateNextTime();
            TimestampOld = Main.time;
            TimestampOldDay = Main.dayTime;
        }

        public override bool UpdateTimer(double dayRate)
        {
            double timeFix = TimestampOldDay == Main.dayTime
                ? 0
                : Main.dayTime ? Main.nightLength : Main.dayLength;

            double distance = Main.time + timeFix - TimestampOld;

            TimestampOld = Main.time;
            TimestampOldDay = Main.dayTime;

            if (TimestampNow < TimestampNext)
            {
                TimestampNow += distance;
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
