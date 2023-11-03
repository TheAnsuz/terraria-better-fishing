using BetterFishing.Multilure;
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
            if (item.fishingPole <= 0)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            if (BetterFishing.Configuration.MultilureMode == Config.Model.MultilureMode.DISABLED)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            MultilureLine[] lines = Multilure.Multilure.GetLines(item.type);

            if (lines == null)
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            bool matches = true;
            int totalLines = 0;

            foreach (MultilureLine line in lines)
            {
                if (matches && line.IsAlternative)
                    continue;

                else if (!matches && line.IsConsecutive)
                    continue;

                matches = line.Condition.Matches(item, player, source, position, velocity, type, damage, knockback);

                if (!matches)
                    continue;

                int reps = line.Amount;
                for (int i = 0; i < reps; i++)
                {
                    Vector2 bobblerSpread = MultilureUtilities.CalculateSpread(velocity, line.Spread);
                    MultilureUtilities.CreateProjectile(source, position, bobblerSpread, type, player);
                    totalLines++;
                }

                //if (line.PostThrow != null)
                //    line.PostThrow.PostThrow(reps, item, player, source, position, velocity, type, damage, knockback);
            }
            /*
            MultilurePostThrow postThrow = Multilure.Multilure.GetPostThrow(item.type);

            if (postThrow != null)
                postThrow.PostThrow(totalLines, item, player, source, position, velocity, type, damage, knockback);
            */
            return false;
        }
    }
}
