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

    public class EarthMajesty : WeaponSkill
    {
        public EarthMajesty() : base("Earth", "Majesty", Element.Earth)
        {

            HP = 3;
            HPPerLevel = 1f;
            ATK = 1;
            ATKPerLevel = 0.5f;
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

    public class EarthMystery : WeaponSkill
    {
        public EarthMystery() : base("Earth", "Mystery", Element.Earth)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;
        }

    }

    public class EarthDeathstrike : WeaponSkill
    {
        public EarthDeathstrike() : base("Earth", "Deathstrike", Element.Earth)
        {
            Echo = 1.25f;
            EchoPerLevel = 0.1f;
        }

    }


}
