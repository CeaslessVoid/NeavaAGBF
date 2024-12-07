using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NeavaAGBF.WeaponSkills
{
    public class WeaponType
    {
        public string Name { get; }
        public string SpritePath { get; }
        public Color TooltipColor { get; }

        public WeaponType(string name, string spritePath, Color tooltipColor)
        {
            Name = name;
            SpritePath = spritePath;
            TooltipColor = tooltipColor;
        }

        public void DrawIcon(Vector2 position)
        {
            Texture2D texture = ModContent.Request<Texture2D>(SpritePath).Value;
            Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public static WeaponType Sword = new WeaponType("Sword", "NeavaAGBF/Content/Players/WeaponType/Sword", Color.Gold);
        public static WeaponType Gun = new WeaponType("Gun", "NeavaAGBF/Content/Players/WeaponType/Gun", Color.Silver);
        public static WeaponType Spear = new WeaponType("Spear", "NeavaAGBF/Content/Players/WeaponType/Spear", Color.Brown);
        public static WeaponType Throw = new WeaponType("Throw", "NeavaAGBF/Content/Players/WeaponType/Throw", Color.Gray);
        public static WeaponType Bow = new WeaponType("Bow", "NeavaAGBF/Content/Players/WeaponType/Bow", Color.Cyan);
        public static WeaponType Hand = new WeaponType("Hand", "NeavaAGBF/Content/Players/WeaponType/Hand", Color.Orange);
        public static WeaponType Special = new WeaponType("Special", "NeavaAGBF/Content/Players/WeaponType/Special", Color.Purple);
        public static WeaponType Whip = new WeaponType("Whip", "NeavaAGBF/Content/Players/WeaponType/Whip", Color.Red);
        public static WeaponType Arcane = new WeaponType("Arcane", "NeavaAGBF/Content/Players/WeaponType/Arcane", Color.Blue);

    }

}
