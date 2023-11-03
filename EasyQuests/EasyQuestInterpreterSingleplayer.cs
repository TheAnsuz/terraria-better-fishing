using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace BetterFishing.EasyQuests
{
    public class EasyQuestInterpreterSingleplayer : EasyQuestInterpreter
    {
        protected static string DATA_TIMESTAMP_NOW => EasyQuestsSystem.DATA_TIMESTAMP_NOW;
        protected static string DATA_TIMESTAMP_NEXT => EasyQuestsSystem.DATA_TIMESTAMP_NEXT;
        protected static string DATA_TIMESTAMP_OLD => EasyQuestsSystem.DATA_TIMESTAMP_OLD;
        protected static string DATA_TIMESTAMP_OLD_DAY => EasyQuestsSystem.DATA_TIMESTAMP_OLD_DAY;

        protected static readonly Point NOTIFICATION_OFFSET = new(0, 25);

        protected double TimestampNow;
        protected double TimestampOld;
        protected bool TimestampOldDay;
        protected double TimestampNext;
        protected LocalizedText NotificationText;
        protected LocalizedText NotificationChatText;
        protected SoundStyle NotificationSound;
        protected Color NotificationColor;

        public override bool ChangeQuestHook(On_Main.orig_AnglerQuestSwap orig)
        {
            // If not enough time has passed
            if (TimestampNow < TimestampNext)
                return false;

            // If should check players
            if (BetterFishing.Configuration.QuestPlayerPercent != 0)
            {
                // Counts the amount of players
                if (!Main.anglerQuestFinished)
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
            NPC angler = null;

            foreach (NPC npc in Main.npc)
            {
                if (npc.type == NPCID.Angler)
                {
                    angler = npc;
                    break;
                }
            }

            if (angler == null || !angler.active)
            {
                return;
            }

            SoundEngine.PlaySound(NotificationSound, angler.position);

            Rectangle rect = angler.getRect();
            rect.Offset(NOTIFICATION_OFFSET);

            CombatText.NewText(rect, NotificationColor, text: NotificationText.Value, true);

            if (Main.anglerQuestFinished && angler.position.Distance(Main.LocalPlayer.position) > 100)
            {
                Main.NewText(NotificationChatText.Value, NotificationColor);
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

            NotificationSound = SoundID.Chat;
            NotificationSound.Pitch = 0.5f;
            NotificationColor = Color.Aquamarine;
            NotificationText = Language.GetOrRegister("Mods.BetterFishing.AnglerQuests.NotificationText");
            NotificationChatText = Language.GetOrRegister("Mods.BetterFishing.AnglerQuests.NotificationChatText");
        }

        public override bool UpdateTimer(double dayRate)
        {
            double timeFix = TimestampOldDay == Main.dayTime
                ? 0
                : Main.dayTime ? Main.dayLength : Main.nightLength;

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
