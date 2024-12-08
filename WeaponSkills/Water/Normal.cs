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


}
