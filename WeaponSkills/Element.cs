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

        public Element(string name, Color color)
        {
            Name = name;
            TooltipColor = color;
        }

        public override string ToString()
        {
            return Name;
        }

        public static readonly Element Wind = new Element(Language.GetText("Mods.NeavaAGBF.Element.Wind").Value, Color.LightGreen);
        public static readonly Element Fire = new Element(Language.GetText("Mods.NeavaAGBF.Element.Fire").Value, Color.Red);
        public static readonly Element Water = new Element(Language.GetText("Mods.NeavaAGBF.Element.Water").Value, Color.Blue);
        public static readonly Element Earth = new Element(Language.GetText("Mods.NeavaAGBF.Element.Earth").Value, Color.SaddleBrown);

        public static readonly Element Light = new Element(Language.GetText("Mods.NeavaAGBF.Element.Light").Value, Color.Yellow);
        public static readonly Element Dark = new Element(Language.GetText("Mods.NeavaAGBF.Element.Dark").Value, Color.MediumPurple);

        public static readonly Element Speical = new Element("Special", Color.Transparent);

    }
}
