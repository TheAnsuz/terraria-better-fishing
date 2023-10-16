using BetterFishing.Multilure.Condition;
using Terraria;

namespace BetterFishing.Multilure
{
    internal class MultilureLine
    {
        public const short MODE_NORMAL = 0;
        public const short MODE_CONSECUTIVE = 1;
        public const short MODE_ALTERNATIVE = 2;

        private readonly int _min;
        private readonly int _max;

        public int Amount => Main.rand.Next(_min, _max + 1);
        public readonly float Spread;
        public readonly MultilureCondition Condition;
        //public MultilurePostThrow PostThrow;

        // MultilureLine modes:
        // 0 - Normal, executed regardless of previous
        // 1 - Consecutive, executed only if previous was correctly executed
        // 2 - Alternative, executed only if previous failed to execute
        public short _mode = 0;
        public bool IsNormal
        {
            get => _mode == 0;
            set => _mode = 0;
        }
        public bool IsConsecutive
        {
            get => _mode == 1;
            set => _mode = 1;
        }
        public bool IsAlternative
        {
            get => _mode == 2;
            set => _mode = 2;
        }

        public MultilureLine(int min, int max, int spread, MultilureCondition condition)
        {
            _min = min;
            _max = max;
            Spread = spread;
            Condition = condition;
        }
    }
}
