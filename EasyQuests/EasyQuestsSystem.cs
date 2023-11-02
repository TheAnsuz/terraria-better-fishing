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

        internal static EasyQuestInterpreter Interpreter;

        public override void Load()
        {
            On_Main.AnglerQuestSwap += CustomAnglerQuestSwap;
        }

        public override void OnWorldLoad()
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

        public override void LoadWorldData(TagCompound tag)
        {
            Interpreter.Load(tag);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            Interpreter.Save(tag);
        }

        private void CustomAnglerQuestSwap(On_Main.orig_AnglerQuestSwap orig)
        {
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
