using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.None
{
    public class NoneArts : WeaponSkill
    {
        public NoneArts() : base("null", "Arts", Element.Speical)
        {
            ATKALLELE = 0.6f;
            ATKALLELEPerLevel = 0.3f;
        }

    }

    public class NoneBalance : WeaponSkill
    {
        public NoneBalance() : base("null", "Balance", Element.Speical)
        {
            ATKALLELE = 1;
            ATKALLELEPerLevel = 0.5f;
        }

    }
}
