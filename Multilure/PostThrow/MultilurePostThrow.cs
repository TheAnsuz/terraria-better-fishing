using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.PostThrow
{
    public abstract class MultilurePostThrow
    {
        public static MultilurePostThrow ConsumeMana(int mana) => new MultilureConsumeManaPostThrow(mana);
        public static MultilurePostThrow ConsumeHealth(int health, int max, bool hurt) => new MultilureConsumeHealthPostThrow(health, max, hurt);

        public abstract void PostThrow(int lines, Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback);
    }
}
