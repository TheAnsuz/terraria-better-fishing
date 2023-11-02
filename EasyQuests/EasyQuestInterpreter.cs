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
    public abstract class EasyQuestInterpreter
    {
        protected internal static readonly Point offset = new(0, 25);
        protected internal static readonly SoundStyle notificationSound = SoundID.Chat;
        protected internal static readonly Color notificatioColor = Color.Aquamarine;
        protected internal static readonly int notificationChatDistance = 100;

        public abstract void Setup();
        public abstract void Load(TagCompound tag);
        public abstract void Save(TagCompound tag);
        public abstract void Notify();
        public abstract bool ChangeQuestHook(On_Main.orig_AnglerQuestSwap orig);
        public abstract bool UpdateTimer(double dayRate);
        public abstract double GetRemainingTime();
    }
}
