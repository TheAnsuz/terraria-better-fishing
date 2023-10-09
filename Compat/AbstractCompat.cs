using BetterFishing.Model.Multilure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BetterFishing.Compat
{
    public abstract class AbstractCompat
    {
        public static List<AbstractCompat> _compats = new List<AbstractCompat>();

        public abstract string ModName { get; }

        public bool IsEnabled() => Mod != null;

        public Mod Mod;

        public void TryEnable()
        {
            if (ModLoader.TryGetMod(ModName, out Mod))
            {
                MultilureMod multilureMod = new(Mod);
                _compats.Add(this);
                //Load(multilureMod);
            }
        }

        protected abstract void Load(MultilureMod Multilure);
    }
}
