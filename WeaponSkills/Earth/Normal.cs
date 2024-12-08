using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Earth
{
    public class EarthMight : WeaponSkill
    {
        public EarthMight() : base("Earth", "Might", Element.Earth)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class EarthAegis : WeaponSkill
    {
        public EarthAegis() : base("Earth", "Aegis", Element.Earth)
        {

            HP = 3;
            HPPerLevel = 1f;
        }

    }

    public class EarthFortified : WeaponSkill
    {
        public EarthFortified() : base("Earth", "Fortified", Element.Earth)
        {

            DEF = 2;
            DEFPerLevel = 0.5f;
        }

    }

    public class EarthVerity : WeaponSkill
    {
        public EarthVerity() : base("Earth", "Verity", Element.Earth)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;
        }

    }


}
