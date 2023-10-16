using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.PostThrow
{
    internal class MultilureConsumeManaPostThrow : MultilurePostThrow
    {
        private readonly int _manaPerLine;

        internal MultilureConsumeManaPostThrow(int mana)
        {
            _manaPerLine = mana;
        }

        public override void PostThrow(int lines, Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.statMana -= _manaPerLine * lines;
        }
    }
}
