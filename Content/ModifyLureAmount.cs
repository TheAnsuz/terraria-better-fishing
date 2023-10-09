using BetterFishing.Model.Multilure;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BetterFishing.Content
{
    internal class ModifyLureAmount : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // If item is not a fishing pole
            if (item.fishingPole <= 0)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            MultilureModRecord record = MultilureMod.GetRecord(item.type);

            if (record == null)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            BobblerProvider provider = record.GetBobbler();

            if (provider == null)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            provider(item, player, source, position, velocity, type, damage, knockback);
            return false;

        }
    }
}
