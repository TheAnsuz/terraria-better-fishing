﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace BetterFishing.Multilure.Condition
{
    internal class MultilureANDCondition : MultilureCondition
    {
        private readonly MultilureCondition[] _conditions;

        internal MultilureANDCondition(params MultilureCondition[] conditions)
        {
            this._conditions = conditions;
        }

        internal override bool Matches(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            foreach (MultilureCondition condition in _conditions)
            {
                if (condition.Matches(item, player, source, position, velocity, type, damage, knockback))
                    continue;
                else
                    return false;
            }
            return true;
        }
    }
}
