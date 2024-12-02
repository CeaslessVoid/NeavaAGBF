using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NeavaAGBF.WeaponSkills.Aegis
{
    public class StormwyrmAegis : WeaponSkill
    {
        public StormwyrmAegis() : base("Stormwyrm", "Aegis", Element.Wind)
        {
            HP = 1;
            HPPerLevel = 0.6f;
        }

        public override Color TooltipColor => Color.Cyan;
    }


}
