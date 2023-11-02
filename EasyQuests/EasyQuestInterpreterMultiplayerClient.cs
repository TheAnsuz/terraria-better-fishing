using BetterFishing.Util;
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
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterFishing.EasyQuests
{
    public class EasyQuestInterpreterMultiplayerClient : EasyQuestInterpreter
    {
        protected static readonly Point NOTIFICATION_OFFSET = new(0, 25);

        protected LocalizedText NotificationText;
        protected LocalizedText NotificationChatText;
        protected SoundStyle NotificationSound;
        protected Color NotificationColor;

        public override bool ChangeQuestHook(On_Main.orig_AnglerQuestSwap orig)
        {
            return false;
        }

        public override double GetRemainingTime()
        {
            ModPacket packet = PacketHandler.Create(PacketID.ANGLER_TIME_REQUEST, sizeof(byte));
            packet.Write((byte)Main.myPlayer);
            packet.Send(255);

            return -1;
        }

        public override void Load(TagCompound tag)
        {

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
            return false;
        }
    }
}
