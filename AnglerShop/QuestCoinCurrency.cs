using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterFishing.AnglerShop
{
    internal class QuestCoinCurrency : CustomCurrencySingleCoin
    {
        public QuestCoinCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
        {
            CurrencyTextKey = Language.GetOrRegister($"{BetterFishing.LOCALIZATION_PATH}.AnglerShop.CurrencyName").Value;
            CurrencyTextColor = Color.LightCoral;
        }
    }
}
