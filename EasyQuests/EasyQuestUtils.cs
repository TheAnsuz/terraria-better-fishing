using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;

namespace BetterFishing.EasyQuests
{
    public class EasyQuestUtils
    {
        public static int CalculateNextTime(int addedTime = 0)
        {
            // 60 ticks / second * N seconds (ingame minutes)
            addedTime += 60 * BetterFishing.Configuration.QuestTimeMinutes;

            // 60 ticks / second * 60 seconds / minute * N minutes
            addedTime += 60 * 60 * BetterFishing.Configuration.QuestTimeHours;

            // 60 ticks / second * 60 seconds / minute * 24 mins / ingame day
            addedTime += 60 * 60 * 24 * BetterFishing.Configuration.QuestTimeDays;

            return addedTime;
        }

        public static int CountPlayersOnServer()
        {
            int players = 0;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Main.player[i] != null)
                    players++;
            }

            return players;
        }

        public static void SetQuestFish(int fishId)
        {
            for (int i = 0; i < Main.anglerQuestItemNetIDs.Length; i++)
            {
                if (Main.anglerQuestItemNetIDs[i] == fishId)
                {
                    SetQuest(i);
                    return;
                }
            }
        }

        public static void SetQuest(int anglerQuestId)
        {
            Main.anglerQuest = anglerQuestId;
            NetMessage.SendAnglerQuest(-1);
        }

        public static void Revert()
        {
            SetQuest(Main.anglerQuest);
        }

        private const double DIVIDER_DAY = 60 * 60 * 24;
        private const double DIVIDER_HOUR = 60 * 60;
        private const double DIVIDER_MINUTE = 60;

        public static void NotifyRemainingTime(double time)
        {
            int days = (int)(time / DIVIDER_DAY);

            time %= DIVIDER_DAY;
            int hours = (int)(time / DIVIDER_HOUR);

            time %= DIVIDER_HOUR;
            int minutes = (int)(time / DIVIDER_MINUTE);

            string line = "";

            if (days > 0)
                line += days + "d ";

            if (hours > 0)
                line += hours + "h ";

            if (minutes > 0)
                line += minutes + "m ";

            Main.NewText(Language.GetOrRegister("Mods.BetterFishing.AnglerQuests.RemainingTime").Format(line));
        }
    }
}
