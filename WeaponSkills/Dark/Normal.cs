﻿using System;
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

    public class DarkMajesty : WeaponSkill
    {
        public DarkMajesty() : base("Dark", "Majesty", Element.Dark)
        {
            HP = 3;
            HPPerLevel = 1f;

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

    public class DarkMystery : WeaponSkill
    {
        public DarkMystery() : base("Dark", "Mystery", Element.Dark)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;

        }

    }

    public class DarkDeathstrike : WeaponSkill
    {
        public DarkDeathstrike() : base("Dark", "Deathstrike", Element.Dark)
        {
            Echo = 1.25f;
            EchoPerLevel = 0.1f;
        }

    }

    public class DarkEssence : WeaponSkill
    {
        public DarkEssence() : base("Dark", "Essence", Element.Dark)
        {
            FlatAtk = 1.0f;
            FlatAtkPerLevel = 0.2f;
        }

    }

    public class DarkAegis2 : WeaponSkill
    {
        public DarkAegis2() : base("Hatred", "Aegis II", Element.Dark)
        {
            HP = 6;
            HPPerLevel = 1.5f;
        }

    }

    public class DarkMight2 : WeaponSkill
    {
        public DarkMight2() : base("Hatred", "Might II", Element.Dark)
        {
            ATK = 3;
            ATKPerLevel = 0.7f;
        }

    }

    public class DarkVerity2 : WeaponSkill
    {
        public DarkVerity2() : base("Hatred", "Verity II", Element.Dark)
        {
            CritRate = 3.2f;
            CritRatePerLevel = 0.3f;
        }

    }

    public class DarkEnmity2 : WeaponSkill
    {
        public DarkEnmity2() : base("Hatred", "Enmity II", Element.Dark)
        {
            Enmity = 9f;
            EnmityPerLevel = 1.4f;
        }

    }

    public class DarkMajesty2 : WeaponSkill
    {
        public DarkMajesty2() : base("Hatred", "Majesty", Element.Dark)
        {
            HP = 6;
            HPPerLevel = 1.5f;

            ATK = 3;
            ATKPerLevel = 0.7f;
        }

    }

    public class DarkSpearhead : WeaponSkill
    {
        public DarkSpearhead() : base("Hatred", "Spearhead", Element.Dark)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;

            AttackSpeed = 1;
            AttackSpeedPerLevel = 0.4f;
        }

    }

    public class DarkDemolishment: WeaponSkill
    {
        public DarkDemolishment() : base("Nightmare", "Demolishment", Element.Dark)
        {
            DMGAmp = 2f;

            UncapLevel = 3;
        }

    }

    public class DarkEnrage2 : WeaponSkill
    {
        public DarkEnrage2() : base("Hatred", "Enrage II", Element.Dark)
        {
            CritDamage = 2.0f;
            CritDamagePerLevel = 1.0f;
        }

    }

    public class DarkTyranny : WeaponSkill
    {
        public DarkTyranny() : base("Hatred", "Tyranny", Element.Dark)
        {
            ATK = 9;
            ATKPerLevel = 2f;

            HP = -10f;
        }

    }

    public class DarkTrium2 : WeaponSkill
    {
        public DarkTrium2() : base("Hatred", "Trium", Element.Dark)
        {
            AttackSpeed = 2;
            AttackSpeedPerLevel = 0.6f;
        }

    }

    public class DarkPrimacy : WeaponSkill
    {
        public DarkPrimacy() : base("Hatred", "Primacy", Element.Dark)
        {
            ATK = 3;
            ATKPerLevel = 0.7f;

            AttackSpeed = 2;
            AttackSpeedPerLevel = 0.6f;
        }

    }
}
