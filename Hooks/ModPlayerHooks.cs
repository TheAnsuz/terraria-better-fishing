using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BetterFishing.Hooks
{
    internal class ModPlayerHooks : ModPlayer
    {
        internal float AumentedFishingPowerAmount = 0;

        public override void ResetEffects()
        {
            base.ResetEffects();
            AumentedFishingPowerAmount = 0;
        }

        internal static PlayerFishingConditions CustomFishingConditions(On_Player.orig_GetFishingConditions orig, Player self)
        {
            PlayerFishingConditions conds = orig(self);

            conds.FinalFishingLevel += (int)self.GetModPlayer<ModPlayerHooks>().AumentedFishingPowerAmount;

            return conds;
        }

        public override void OnEnterWorld()
        {
            base.OnEnterWorld();
#if DEBUG
            if (BetterFishing.Errors)
                Main.NewText("BetterFishing loaded with errors", Color.Red);
#endif
        }

        public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
        {
            //fishingLevel += AumentedFishingPowerAmount;
        }
    }
}
