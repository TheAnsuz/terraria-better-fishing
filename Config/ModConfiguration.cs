using BetterFishing.Config.Model;
using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace BetterFishing.Config
{
    public class ModConfiguration : ModConfig
    {
        public const string LANGUAGE_SECTION = "Mods.BetterFishing.Configs";

        public override LocalizedText DisplayName => Language.GetOrRegister($"{LANGUAGE_SECTION}.ConfigName");

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

        [Header($"${LANGUAGE_SECTION}.Header.Multilure")]
        [DefaultValue(MultilureMode.NORMAL)]
        [DrawTicks]
        [LabelKey($"${LANGUAGE_SECTION}.MultilureMode.Label")]
        [TooltipKey($"${LANGUAGE_SECTION}.MultilureMode.Tooltip")]
        public MultilureMode MultilureMode;

        /*
        [DefaultValue(MultilurePower.NORMAL)]
        [DrawTicks]
        [ReloadRequired]
        [LabelKey($"${LANGUAGE_SECTION}.MultilurePower.Label")]
        [TooltipKey($"${LANGUAGE_SECTION}.MultilurePower.Tooltip")]
        public MultilurePower MultilurePower;
        */
    }
}
