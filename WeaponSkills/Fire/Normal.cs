using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Fire
{
    public class FireMight : WeaponSkill
    {
        public FireMight() : base("Fire", "Might", Element.Fire)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class FireAegis : WeaponSkill
    {
        public FireAegis() : base("Fire", "Aegis", Element.Fire)
        {

            HP = 3;
            HPPerLevel = 1f;
        }

    }

    public class FireFortified : WeaponSkill
    {
        public FireFortified() : base("Fire", "Fortified", Element.Fire)
        {

            DEF = 2;
            DEFPerLevel = 0.5f;
        }

    }

    public class FireVerity : WeaponSkill
    {
        public FireVerity() : base("Fire", "Verity", Element.Fire)
        {
            CritRate = 1;
            CritRatePerLevel = 0.1f;
        }

    }
}
