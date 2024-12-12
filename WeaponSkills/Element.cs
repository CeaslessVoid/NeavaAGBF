using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace NeavaAGBF.WeaponSkills
{
    public class Element
    {
        public string Name { get; set; }
        public Color TooltipColor { get; set; }

        public string RealName { get; set; }

        public Element(string name, Color color, string realName)
        {
            Name = name;
            TooltipColor = color;
            RealName = realName;
        }

        public override string ToString()
        {
            return Name;
        }

        public static readonly Element Wind = new Element(Language.GetText("Mods.NeavaAGBF.Element.Wind").Value, Color.LightGreen, "Wind");
        public static readonly Element Fire = new Element(Language.GetText("Mods.NeavaAGBF.Element.Fire").Value, Color.Red, "Fire");
        public static readonly Element Water = new Element(Language.GetText("Mods.NeavaAGBF.Element.Water").Value, Color.Blue, "Water");
        public static readonly Element Earth = new Element(Language.GetText("Mods.NeavaAGBF.Element.Earth").Value, Color.SaddleBrown, "Earth");

        public static readonly Element Light = new Element(Language.GetText("Mods.NeavaAGBF.Element.Light").Value, Color.Yellow, "Light");
        public static readonly Element Dark = new Element(Language.GetText("Mods.NeavaAGBF.Element.Dark").Value, Color.MediumPurple, "Dark");

        public static readonly Element Special = new Element(Language.GetText("Mods.NeavaAGBF.Element.Special").Value, Color.White, "Speical");

    }
}
