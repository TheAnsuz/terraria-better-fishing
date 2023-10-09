using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BetterFishing.Model.Multilure
{
    public class MultilureModRecord
    {
        internal static readonly MultilureModRecord EmptyRecord = new("Vanilla", -1);

        private BobblerProvider _bobblerProvider;
        private DescriptionProvider _descriptionProvider;
        public readonly int ItemID;
        public readonly string ModName;

        internal MultilureModRecord(string modName, int itemId)
        {
            ItemID = itemId;
            ModName = modName;
        }
        public DescriptionProvider GetDescription() => _descriptionProvider;
        public BobblerProvider GetBobbler() => _bobblerProvider;

        public MultilureModRecord Bobbler_FixedAmount(int amount, float spread = 75f)
        {
            _bobblerProvider = BobblerProviders.FixedAmount(ItemID, amount, spread);
            return this;
        }

        public MultilureModRecord Bobbler_RangedAmount(int min, int max, float spread = 75f)
        {
            _bobblerProvider = BobblerProviders.RangeAmount(ItemID, min, max, spread);
            return this;
        }

        public MultilureModRecord Bobbler_Custom(BobblerProvider provider)
        {
            _bobblerProvider = provider;
            return this;
        }

        public MultilureModRecord Bobbler_Default(float spread = .75f)
        {
            _bobblerProvider = BobblerProviders.DefaultAmount(ItemID, spread);
            return this;
        }

        public MultilureModRecord Description_Simple(string key, params object[] args)
        {
            _descriptionProvider = DescriptionProviders.SimpleCustomDescription(ModName, key, args);
            return this;
        }
    }
}
