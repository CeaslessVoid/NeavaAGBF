using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Wind
{
    public class LightMight : WeaponSkill
    {
        public LightMight() : base("Light", "Might", Element.Light)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class LightVerity : WeaponSkill
    {
        public LightVerity() : base("Light", "Verity", Element.Light)
        {
            CritRate = 1;
            CritRatePerLevel = 0.1f;
        }

    }

}
