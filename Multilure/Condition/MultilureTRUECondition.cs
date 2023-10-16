using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.Condition
{
    internal class MultilureTRUECondition : MultilureCondition
    {
        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
