using Microsoft.Xna.Framework.Graphics;
using NeavaAGBF.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NeavaAGBF.Common.UI
{
    public class ChargeBar : UIState
    {
        public static ChargeBar Instance { get; set; }
        public static bool Visible { get; set; } = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible) return;

            Player player = Main.LocalPlayer;
            if (player.TryGetModPlayer<StatHandler>(out var modPlayer))
            {

                float chargePercent = modPlayer.currentCharge / modPlayer.MaxCharge;
                chargePercent = MathHelper.Clamp(chargePercent, 0f, 1f); // Ensure it's between 0% and 100%
                string chargeText = $"{(int)(chargePercent * 100)}%";

                Vector2 position = new Vector2(500, 40);
                int width = 200;
                int height = 20;

                Texture2D backgroundTexture = TextureAssets.MagicPixel.Value;
                spriteBatch.Draw(backgroundTexture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.Gray);

                Color fillColor = modPlayer.currentCharge >= modPlayer.MaxCharge ? GetRainbowColor() : Color.Yellow;
                spriteBatch.Draw(backgroundTexture, new Rectangle((int)position.X, (int)position.Y, (int)(width * chargePercent), height), fillColor);


                Texture2D ChargeBarSprite = ModContent.Request<Texture2D>("NeavaAGBF/Content/Players/ChargeBar").Value;

                Vector2 spritePosition = position - new Vector2(20,15);
                spriteBatch.Draw(ChargeBarSprite, spritePosition, Color.White);

                Vector2 textPosition = position + new Vector2(width / 2, height / 2);
                Utils.DrawBorderString(spriteBatch, chargeText, textPosition, Color.White, 1f, 0.5f, 0.5f);
            }
        }

        private Color GetRainbowColor()
        {
            float hue = (float)(Main.GameUpdateCount % 60) / 60f;
            return Main.hslToRgb(hue, 1f, 0.5f);
        }

    }
}
