using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Dark
{
    public class DarkMight : WeaponSkill
    {
        public DarkMight() : base("Dark", "Might", Element.Dark)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class DarkVerity : WeaponSkill
    {
        public DarkVerity() : base("Dark", "Verity", Element.Dark)
        {
            CritRate = 1;
            CritRatePerLevel = 0.1f;
        }

    }
}
