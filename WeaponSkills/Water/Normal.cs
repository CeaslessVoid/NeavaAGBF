using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Water
{
    public class WaterMight : WeaponSkill
    {
        public WaterMight() : base("Water", "Might", Element.Water)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class WaterAegis : WeaponSkill
    {
        public WaterAegis() : base("Water", "Aegis", Element.Water)
        {

            HP = 3;
            HPPerLevel = 1f;
        }

    }

    public class WaterTrium : WeaponSkill
    {
        public WaterTrium() : base("Water", "Trium", Element.Water)
        {

            AttackSpeed = 1;
            AttackSpeedPerLevel = 0.4f;
        }

    }

    public class WaterVerity : WeaponSkill
    {
        public WaterVerity() : base("Water", "Verity", Element.Water)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;
        }

    }

    public class WaterSpearhead : WeaponSkill
    {
        public WaterSpearhead() : base("Water", "Spearhead", Element.Water)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;

            AttackSpeed = 1;
            AttackSpeedPerLevel = 0.4f;
        }

    }

    public class WaterMystery : WeaponSkill
    {
        public WaterMystery() : base("Water", "Mystery", Element.Water)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;
        }

    }

    public class WaterDeathstrike : WeaponSkill
    {
        public WaterDeathstrike() : base("Water", "Deathstrike", Element.Water)
        {
            Echo = 1.25f;
            EchoPerLevel = 0.1f;
        }

    }


}
