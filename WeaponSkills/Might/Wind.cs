using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Might
{
    public class StormwyrmMight : WeaponSkill
    {
        public StormwyrmMight() : base("Stormwyrm", "Might", Element.Wind)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }


}
