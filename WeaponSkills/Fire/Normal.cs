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

    public class FireSun : WeaponSkill
    {
        public FireSun() : base("Fire", "Sun", Element.Fire)
        {
            CritDamage = 1.5f;

            ATK = 2.5f;

            CritRate = 1.5f;
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

    public class FireCelere : WeaponSkill
    {
        public FireCelere() : base("Fire", "Celere", Element.Fire)
        {

            CritRate = 1;
            CritRatePerLevel = 0.2f;

            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class FireEnrage : WeaponSkill
    {
        public FireEnrage() : base("Fire", "Enrage", Element.Fire)
        {

            CritDamage = 1f;
            CritDamagePerLevel = 0.5f;
        }

    }

    public class FireVerity : WeaponSkill
    {
        public FireVerity() : base("Fire", "Verity", Element.Fire)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;
        }

    }
}
