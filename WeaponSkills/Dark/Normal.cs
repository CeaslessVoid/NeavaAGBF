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
            CritRatePerLevel = 0.2f;
        }

    }

    public class DarkEnmity : WeaponSkill
    {
        public DarkEnmity() : base("Dark", "Enmity", Element.Dark)
        {
            Enmity = 3f;
            EnmityPerLevel = 1.0f;
        }

    }

    public class DarkBloodshed : WeaponSkill
    {
        public DarkBloodshed() : base("Dark", "Bloodshed", Element.Dark)
        {
            ATK = 6;
            ATKPerLevel = 1.5f;

            HP = -10f;

        }

    }
}
