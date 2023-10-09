using BetterFishing.Compat;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace BetterFishing
{
    public class Configuration : ModConfig
    {
        public const string LANGUAGE_SECTION = "Mods.BetterFishing.Configuration";

        public override LocalizedText DisplayName => Language.GetText($"{LANGUAGE_SECTION}.HostConfiguration");

        public override ConfigScope Mode => ConfigScope.ServerSide;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            switch (Mod.NetID)
            {
                case Terraria.ID.NetmodeID.SinglePlayer:
                case Terraria.ID.NetmodeID.Server:
                    return true;

                case Terraria.ID.NetmodeID.MultiplayerClient:
                default:
                    if (whoAmI == 0)
                        return true; // On a steam-hosted match. the first one to join is always the host
                    message = Language.GetTextValue($"{LANGUAGE_SECTION}.NoPermission");
                    return false;
            }
        }

        public override void OnChanged()
        {
        }

        [LabelKey($"${LANGUAGE_SECTION}.LureMultiplier.Label")]
        [TooltipKey($"${LANGUAGE_SECTION}.LureMultiplier.Tooltip")]
        [Range(1f,5f)]
        [DefaultValue(1f)]
        [Increment(.25f)]
        public float LureMultiplier = 1;

    }
}
