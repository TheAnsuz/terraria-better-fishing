using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BetterFishing.Model.Multilure
{
    public class BobblerProviders
    {
        private static readonly Dictionary<int, float> _spread = new();

        /*
         * Bobbler provider that offers a specific amount of baits to be thrown, never chaning
         */
        public static BobblerProvider FixedAmount(int itemId, int amount, float spread)
        {
            _fixedAmountBobblers.Add(itemId, amount);
            _spread.Add(itemId, spread);
            return FixedAmountProvider;
        }
        private static readonly Dictionary<int, int> _fixedAmountBobblers = new();

        private static void FixedAmountProvider(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int bobblers = _fixedAmountBobblers[item.type];
            float spread = _spread[item.type];

            for (int i = 0; i < bobblers; ++i)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spread, spread) * 0.05f, Main.rand.NextFloat(-spread, spread) * 0.05f);

                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
        }

        /*
         * Bobbler provider that offers a amount of baits that varies in between a range of two values
         */
        public static BobblerProvider RangeAmount(int itemId, int amountMin, int amountMax, float spread)
        {
            _rangeAmountBobblersMin.Add(itemId, amountMin);
            _rangeAmountBobblersMax.Add(itemId, amountMax);
            _spread.Add(itemId, spread);
            return RangedAmountProvider;
        }
        private static readonly Dictionary<int, int> _rangeAmountBobblersMin = new();
        private static readonly Dictionary<int, int> _rangeAmountBobblersMax = new();

        private static void RangedAmountProvider(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int bobblers = Main.rand.Next(_rangeAmountBobblersMin[item.type], _rangeAmountBobblersMax[item.type]);
            float spread = _spread[item.type];

            for (int i = 0; i < bobblers; ++i)
            {
                Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spread, spread) * 0.05f, Main.rand.NextFloat(-spread, spread) * 0.05f);

                Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
            }
        }

        /*
         * Bobbler provider that offers a amount of baits that varies in between a range of two values
         */
        public static BobblerProvider DefaultAmount(int itemId, float spread)
        {
            return FixedAmount(itemId, 1, spread);
        }

    }
}
