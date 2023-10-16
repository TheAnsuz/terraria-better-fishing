using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.Condition
{
    public class MultilureCustomCondition : MultilureCondition
    {
        private readonly Match _match;

        internal MultilureCustomCondition(Match match)
        {
            this._match = match;
        }

        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return _match(item, player, source, position, velocity, type, damage, knockback);
        }

        public delegate bool Match(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback);
    }
}
