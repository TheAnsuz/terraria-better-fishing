using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected static readonly Point NOTIFICATION_OFFSET = new(0, 25);

        protected double TimestampNow;
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
        }

        public override void Setup()
        {
            NotificationSound = SoundID.Chat;
            NotificationSound.Pitch = 0.5f;
            NotificationColor = Color.Aquamarine;
            NotificationText = Language.GetOrRegister("Mods.BetterFishing.AnglerQuests.NotificationText");
            NotificationChatText = Language.GetOrRegister("Mods.BetterFishing.AnglerQuests.NotificationChatText");
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
