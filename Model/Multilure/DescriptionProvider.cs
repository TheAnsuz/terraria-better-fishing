using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace BetterFishing.Model.Multilure
{
    public delegate void DescriptionProvider(Mod mod, Item item, List<TooltipLine> tooltips);
}
