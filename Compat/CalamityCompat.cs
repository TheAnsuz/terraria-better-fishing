using BetterFishing.Model.Multilure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BetterFishing.Compat
{
    internal class CalamityCompat : AbstractCompat
    {
        public override string ModName => "CalamityMod";

        public const string EarlyBloomRod = "EarlyBloomRod";
        public const string FeralDoubleRod = "FeralDoubleRod";
        public const string HeronRod = "HeronRod";
        public const string NaviFishingRod = "NaviFishingRod";
        public const string RiftReeler = "RiftReeler";
        public const string SlurperPole = "SlurperPole";
        public const string TheDevourerofCods = "TheDevourerofCods";
        public const string VerstaltiteFishingRod = "VerstaltiteFishingRod";
        public const string WulfrumFishingPole = "WulfrumFishingPole";

        protected override void Load(MultilureMod Multilure)
        {
            Multilure.Register(EarlyBloomRod).Bobbler_FixedAmount(100).Description_Simple(EarlyBloomRod, 100);

            /*
             * Le pasas un nombre de registro
             * Del cual obtiene el ModItem y la id
             * El metodo SimpleDescription usará por defecto el nombre que le pasas en el registro
             * 
             * Multilure.Register(name).SimpleBobbler(bobblers: 5).SimpleDescription();
             * 
             *  * Usar bloque 'using'
             * 
             * 
             * 
             * Obtener datos:
             * Multilure.Get(id).BobblerProvider/DescriptionProvider
             */
        }
    }
}
