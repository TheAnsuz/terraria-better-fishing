using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;

namespace BetterFishing.AnglerShop
{
    internal class ModifyAnglerReward
    {
        internal static void GiveRewards(On_Player.orig_GetAnglerReward_MainReward orig, Player self, List<Item> rewardItems, IEntitySource source, int questsDone, float rarityReduction, int questItemType, ref GetItemSettings anglerRewardSettings)
        {
            orig(self, rewardItems, source, questsDone, rarityReduction, questItemType, ref anglerRewardSettings);

            AnglerCoinRewardEntry reward = AnglerCoinReward.GetReward(questItemType);

            Item item = new(AnglerShopSystem.QuestCoinID, stack: reward.Amount());

            rewardItems.Add(item);
        }
    }
}
