using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace BetterFishing.AnglerShop
{
    internal class AnglerShopUIState : UIState
    {
        private readonly static Vector2 SCALE = new Vector2(1, 1);
        private readonly static Vector2 ORIGIN = new Vector2(0, 0);
        private readonly static Vector2 POSITION_QUESTS = new Vector2(110, 435);
        private readonly static Vector2 POSITION_SCROLL_ICON = new Vector2(75, 435);
        private const int FLICK_TIME = 60;
        private readonly static Texture2D ScrollIcon = ModContent.Request<Texture2D>(name: $"{BetterFishing.ASSETS_BASE}/UI/MouseScrollIcon", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        private readonly static Texture2D ScrollIconFlick = ModContent.Request<Texture2D>(name: $"{BetterFishing.ASSETS_BASE}/UI/MouseScrollIconFlick", mode: ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        private int _flickFrames = 0;
        private bool _flick = false;

        public override void Draw(SpriteBatch spriteBatch)
        {
            LocalizedText questAmount = Language.GetOrRegister($"{BetterFishing.LANGUAGE_BASE}.AnglerShop.QuestsLabel");

            ChatManager.DrawColorCodedStringWithShadow(
                    spriteBatch,
                    FontAssets.MouseText.Value,
                    questAmount.Format(Main.LocalPlayer.anglerQuestsFinished),
                    POSITION_QUESTS,
                    Color.White,
                    0,
                    ORIGIN,
                    SCALE
                );

            if (AnglerShop.Line != 0 || AnglerShop.CanScroll(1))
            {

                if (_flickFrames > FLICK_TIME)
                {
                    _flickFrames = 0;
                    _flick = !_flick;
                }
                _flickFrames++;

                spriteBatch.Draw(_flick ? ScrollIcon : ScrollIconFlick,
                    POSITION_SCROLL_ICON,
                    Color.White);

            }

        }

    }
}
