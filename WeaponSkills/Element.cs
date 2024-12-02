using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

        public static readonly Element Wind = new Element("Wind", Color.LightGreen);
        public static readonly Element Fire = new Element("Fire", Color.Red);
        public static readonly Element Water = new Element("Water", Color.Blue);
        public static readonly Element Earth = new Element("Earth", Color.Brown);
    }
}
