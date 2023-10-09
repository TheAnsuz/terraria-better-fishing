using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BetterFishing.Model.Multilure
{
    public class MultilureMod
    {
        private static Dictionary<int, MultilureModRecord> _multilures = new();

        private static void AddRecord(int itemId, MultilureModRecord record)
        {
            _multilures[itemId] = record;
        }

        public static MultilureModRecord GetRecord(int itemId)
        {
            if (_multilures.ContainsKey(itemId))
                return _multilures[itemId];
            return null;
        }

        public static void ClearCache()
        {
            _multilures.Clear();
        }

        private readonly Mod _mod;

        public MultilureMod(Mod mod)
        {
            this._mod = mod;
        }

        public MultilureModRecord Register(string itemName)
        {
            MultilureModRecord record;

            if (_mod.TryFind(itemName, out ModItem item))
            {
                record = new(_mod.Name, item.Type);
                AddRecord(item.Type, record);
            }
            else
                record = MultilureModRecord.EmptyRecord;

            return record;
        }

        public MultilureModRecord Register(int itemId)
        {
            MultilureModRecord record = new("Vanilla", itemId);
            AddRecord(itemId, record);
            return record;
        }
    }
}
