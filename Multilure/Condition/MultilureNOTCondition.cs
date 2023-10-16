using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.Condition
{
    internal class MultilureNOTCondition : MultilureCondition
    {
        private readonly MultilureCondition _condition;

        internal MultilureNOTCondition(MultilureCondition condition)
        {
            this._condition = condition;
        }

        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return !_condition.Matches(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
