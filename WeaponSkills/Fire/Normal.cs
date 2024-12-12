﻿using System;
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

    public class FireMajesty : WeaponSkill
    {
        public FireMajesty() : base("Fire", "Majesty", Element.Fire)
        {

            HP = 3;
            HPPerLevel = 1f;

            ATK = 1;
            ATKPerLevel = 0.5f;
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

    public class FireMystery : WeaponSkill
    {
        public FireMystery() : base("Fire", "Mystery", Element.Fire)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;
        }

    }

    public class FireDeathstrike : WeaponSkill
    {
        public FireDeathstrike() : base("Fire", "Deathstrike", Element.Fire)
        {
            Echo = 1.25f;
            EchoPerLevel = 0.1f;
        }

    }

    public class FireMight2 : WeaponSkill
    {
        public FireMight2() : base("Hellfire", "Might II", Element.Fire)
        {
            ATK = 3;
            ATKPerLevel = 0.7f;

        }

    }
    public class FireVerity2 : WeaponSkill
    {
        public FireVerity2() : base("Hellfire", "Verity II", Element.Fire)
        {
            CritRate = 3.2f;
            CritRatePerLevel = 0.3f;
        }

    }

    public class FireDemolishment : WeaponSkill
    {
        public FireDemolishment() : base("Hellfire", "Demolishment", Element.Fire)
        {
            DMGAmp = 3f;

            UncapLevel = 3;
        }

    }
}
