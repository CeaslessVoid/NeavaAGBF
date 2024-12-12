using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NeavaAGBF.WeaponSkills.Wind
{

    public class WindMight : WeaponSkill
    {
        public WindMight() : base("Wind", "Might", Element.Wind)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;
        }

    }

    public class WindAegis : WeaponSkill
    {
        public WindAegis() : base("Wind", "Aegis", Element.Wind)
        {
            
            HP = 3;
            HPPerLevel = 1f;
        }

    }

    public class WindMajesty : WeaponSkill
    {
        public WindMajesty() : base("Wind", "Majesty", Element.Wind)
        {
            ATK = 1;
            ATKPerLevel = 0.5f;

            HP = 3;
            HPPerLevel = 1f;
        }

    }

    // Fortified is defence as there is no base def
    // Sprite: Preemptive Wall
    public class WindFortified : WeaponSkill
    {
        public WindFortified() : base("Wind", "Fortified", Element.Wind)
        {

            DEF = 2;
            DEFPerLevel = 0.5f;
        }

    }

    public class WindVerity : WeaponSkill
    {
        public WindVerity() : base("Wind", "Verity", Element.Wind)
        {
            CritRate = 1;
            CritRatePerLevel = 0.2f;
        }

    }

    public class WindStamina : WeaponSkill
    {
        public WindStamina() : base("Wind", "Stamina", Element.Wind)
        {
            Stamina = 1.4f;
            StaminaPerLevel = 0.8f;
        }

    }

    public class WindMystery : WeaponSkill
    {
        public WindMystery() : base("Wind", "Mystery", Element.Wind)
        {
            ChargAttack = 1f;
            ChargAttackPerLevel = 0.5f;
        }

    }

    public class WindDeathstrike : WeaponSkill
    {
        public WindDeathstrike() : base("Wind", "Deathstrike", Element.Wind)
        {
            Echo = 1.25f;
            EchoPerLevel= 0.1f;
        }

    }

    public class WindEssence : WeaponSkill
    {
        public WindEssence() : base("Wind", "Essence", Element.Wind)
        {
            FlatAtk = 1.0f;
            FlatAtkPerLevel = 0.2f;
        }

    }

    public class WindMight2 : WeaponSkill
    {
        public WindMight2() : base("Whirlwind", "Might II", Element.Wind)
        {
            ATK = 3;
            ATKPerLevel = 0.7f;
        }

    }

    public class WindVerity2 : WeaponSkill
    {
        public WindVerity2() : base("Whirlwind", "Verity II", Element.Wind)
        {
            CritRate = 3.2f;
            CritRatePerLevel = 0.3f;
        }

    }

    public class WindAegis2 : WeaponSkill
    {
        public WindAegis2() : base("Wind", "Aegis II", Element.Wind)
        {

            HP = 6;
            HPPerLevel = 1.5f;
        }

    }

    public class WindStamina2 : WeaponSkill
    {
        public WindStamina2() : base("Wind", "Stamina II", Element.Wind)
        {
            Stamina = 3.2f;
            StaminaPerLevel = 1.1f;
        }

    }

}
