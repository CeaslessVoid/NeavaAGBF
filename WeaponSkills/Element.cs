using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills
{
    public class Element
    {
        public static readonly Element Water = new Element("Water");
        public static readonly Element Fire = new Element("Fire");
        public static readonly Element Earth = new Element("Earth");
        public static readonly Element Wind = new Element("Wind");
        public static readonly Element Dark = new Element("Dark");
        public static readonly Element Light = new Element("Light");

        public string Name { get; private set; }

        private Element(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
