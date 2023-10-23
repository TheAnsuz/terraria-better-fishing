using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace BetterFishing.AnglerShop
{
    public class AnglerShopEntry
    {
        public readonly int ID;
        private bool _vanillaCurrency;
        private int _price;
        private int _afterQuest;
        private SellCondition.SellCondition _sellCondition = SellCondition.SellCondition.True();
        private SellCondition.SellCondition _forcedSellCondition = SellCondition.SellCondition.True();

        protected internal bool UseVanillaCurrency => _vanillaCurrency;
        protected internal int Cost => _price;
        protected internal int QuestsToUnlock => _afterQuest;
        protected internal SellCondition.SellCondition Condition => _sellCondition;
        protected internal SellCondition.SellCondition ForcedCondition => _forcedSellCondition;

        internal AnglerShopEntry(int itemId)
        {
            ID = itemId;
        }

        public AnglerShopEntry PriceCoins(int platinum, int gold, int silver, int copper)
        {
            _price = Item.buyPrice(platinum, gold, silver, copper);
            _vanillaCurrency = true;
            return this;
        }

        public AnglerShopEntry Price(int price)
        {
            _price = price;
            _vanillaCurrency = false;
            return this;
        }

        public AnglerShopEntry AfterQuest(int quests)
        {
            _afterQuest = quests;
            return this;
        }

        public AnglerShopEntry WithCondition(SellCondition.SellCondition sellCondition)
        {
            _sellCondition = sellCondition;
            return this;
        }

        public AnglerShopEntry EnforceCondition(SellCondition.SellCondition forcedSellCondition)
        {
            _forcedSellCondition = forcedSellCondition;
            return this;
        }
    }
}
