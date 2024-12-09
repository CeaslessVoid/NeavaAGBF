﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills.Wind
{
    public class LightMight : WeaponSkill
    {
        public LightMight() : base("Light", "Might", Element.Light)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class LightAegis : WeaponSkill
    {
        public LightAegis() : base("Light", "Aegis", Element.Light)
        {
            HP = 3;
            HPPerLevel = 1f;
        }

    }

    public class LightMajesty : WeaponSkill
    {
        public LightMajesty() : base("Light", "Majesty", Element.Light)
        {
            HP = 3;
            HPPerLevel = 1f;

            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class LightVerity : WeaponSkill
    {
        public LightVerity() : base("Light", "Verity", Element.Light)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;
        }

    }

    public class LightGrace : WeaponSkill
    {
        public LightGrace() : base("Light", "Grace", Element.Light)
        {
            HP = 3f;
            HPPerLevel = 1f;

            DMGReduc = 0.6f;
            DMGReducPerLevel = 0.1f;
        }

    }

    public class LightMystery : WeaponSkill
    {
        public LightMystery() : base("Light", "Mystery", Element.Light)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;
        }

    }

    public class LightDeathstrike : WeaponSkill
    {
        public LightDeathstrike() : base("Light", "Deathstrike", Element.Light)
        {
            Echo = 1.25f;
            EchoPerLevel = 0.1f;
        }

    }

}
