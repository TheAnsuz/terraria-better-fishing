using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.EasyQuests
{
    internal class QuestTimeNotifier : GlobalNPC
    {
        // Only gets called on client (Singleplayer || Multiplayer Client)
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            if (npc.type != NPCID.Angler || !firstButton)
                return;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                EasyQuestUtils.NotifyRemainingTime(EasyQuestsSystem.Interpreter.GetRemainingTime());
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                EasyQuestsSystem.Interpreter.GetRemainingTime();
            }
        }

    }
}
