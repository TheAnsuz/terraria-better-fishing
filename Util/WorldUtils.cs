using Terraria;

namespace BetterFishing.Util
{
    public class WorldUtils
    {
        private WorldUtils() { }

        public static float WindRaw => Main.windSpeedCurrent;

        public static int Wind
        {
            get
            {
                return (int)(Main.windSpeedCurrent * 50f);
            }
            set
            {
                Main.windSpeedTarget = value / 50f;
            }
        }

        public static bool Raining => Main.raining;

        public static bool Hardmode => Main.hardMode;
    }
}
