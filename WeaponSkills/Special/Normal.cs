﻿using System;
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

    public class NoneEnchantment : WeaponSkill
    {
        public NoneEnchantment() : base("null", "Enchantment", Element.Speical)
        {
            ATKALLELE = 1;
            ATKALLELEPerLevel = 0.5f;
        }

    }

    public class NoneBane : WeaponSkill
    {
        public NoneBane() : base("null", "Bane", Element.Speical)
        {
            DMGAmpU = 5;
        }

    }

    public class NoneStarTouch : WeaponSkill
    {
        public NoneStarTouch() : base("null", "Star Touch", Element.Speical)
        {
            ATKALLELE = 1;
            ATKALLELEPerLevel = 0.5f;
        }

    }
}
