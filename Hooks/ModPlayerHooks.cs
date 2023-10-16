using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BetterFishing.Hooks
{
    internal class ModPlayerHooks : ModPlayer
    {
        public override void OnEnterWorld()
        {
            base.OnEnterWorld();
#if DEBUG
            if (BetterFishing.Errors)
                Main.NewText("BetterFishing loaded with errors", Color.Red);
#endif
        }
    }
}
