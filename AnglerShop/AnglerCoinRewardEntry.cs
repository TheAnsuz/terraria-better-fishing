using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFishing.AnglerShop
{
    public class AnglerCoinRewardEntry
    {
        private readonly int ID;
        private int _amount;

        internal AnglerCoinRewardEntry(int id)
        {
            ID = id;
        }

        public AnglerCoinRewardEntry Amount(int amount)
        {
            _amount = amount;
            return this;
        }

        public int Amount()
        {
            return _amount;
        }

    }
}
