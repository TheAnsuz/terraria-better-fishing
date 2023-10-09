using BetterFishing.Compat;
using BetterFishing.Model.Multilure;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing
{
    public class BetterFishing : Mod
    {
        public readonly static Configuration Configuration = ModContent.GetInstance<Configuration>();

        public override void Load()
        {
            new CalamityCompat().TryEnable();
            Setup(new MultilureMod(this));
        }

        public override void Unload()
        {
            MultilureMod.ClearCache();
            base.Unload();
        }

        public void Setup(MultilureMod Multilure)
        {
            Multilure.Register(ItemID.WoodFishingPole).Bobbler_FixedAmount(2).Description_Simple("Tooltip", Configuration.LureMultiplier * 2);
        }
    }
}