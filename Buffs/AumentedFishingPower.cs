using BetterFishing.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.Buffs
{
    internal class AumentedFishingPower : ModBuff
    {
        public override LocalizedText Description => Language.GetOrRegister("Mods.BetterFishing.Buffs.AumentedFishingPower.Description");
        public override LocalizedText DisplayName => Language.GetOrRegister("Mods.BetterFishing.Buffs.AumentedFishingPower.DisplayName");

        int loop = 0;

        public override void Update(Player player, ref int buffIndex)
        {


            for (int k = 0; k < 10; k++)
            {
                Item item = player.armor[k];
                if (!item.IsAir && player.IsItemSlotUnlockedAndUsable(k) && (!item.expertOnly || Main.expertMode) && UpdateEquips_CanItemGrantBenefits(k, item))
                {
                    if (item.accessory)
                    {
                        player.GrantPrefixBenefits(item);
                    }
                    player.GrantArmorBenefits(item);
                }
            }

            if (player.sitting.TryGetSittingBlock(player, out Tile tile))
            {
                if (loop == 0)
                {
                    loop = 110;
                    CombatText.NewText(player.getRect(), CombatText.HealMana, tile.TileFrameY + " - " + tile.TileType);
                }
                    loop--;
            }

            if (player.canFloatInWater || player.accFishingBobber)
                player.GetModPlayer<ModPlayerHooks>().AumentedFishingPowerAmount += 1;
            else
                player.GetModPlayer<ModPlayerHooks>().AumentedFishingPowerAmount += 100;

        }

        private bool UpdateEquips_CanItemGrantBenefits(int itemSlot, Item item)
        {
            switch (itemSlot)
            {
                default:
                    return true;
                case 0:
                    return item.headSlot > -1;
                case 1:
                    return item.bodySlot > -1;
                case 2:
                    return item.legSlot > -1;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return item.accessory;
            }
        }
    }
}
