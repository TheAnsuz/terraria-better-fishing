using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.PostThrow
{
    internal class MultilureConsumeHealthPostThrow : MultilurePostThrow
    {
        private readonly int _healthPerLine;
        private readonly bool hurt;
        private readonly int _max;

        internal MultilureConsumeHealthPostThrow(int health, int max, bool hurtMode)
        {
            _max = max;
            _healthPerLine = health;
            hurt = hurtMode;
        }

        public override void PostThrow(int lines, Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (hurt)
                player.Hurt(PlayerDeathReason.ByPlayerItem(player.whoAmI, item), Math.Min(_healthPerLine * lines, _max), 0);
            else
            {
                player.statLife -= _healthPerLine * lines;

                if (player.statLife <= 0)
                    player.KillMe(PlayerDeathReason.ByPlayerItem(player.whoAmI, item), Math.Min(_healthPerLine * lines, _max), 0);
            }
        }
    }
}
