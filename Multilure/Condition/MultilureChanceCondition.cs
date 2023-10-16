using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.Condition
{
    internal class MultilureChanceCondition : MultilureCondition
    {
        private readonly float _chance;
        private readonly float _maxChance;

        internal MultilureChanceCondition(float chance, float max)
        {
            _chance = chance;
            _maxChance = max;
        }

        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return Main.rand.NextFloat(0, _maxChance) < _chance;
        }
    }
}
