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

    //public class StormwyrmAegis : WeaponSkill
    //{
    //    public StormwyrmAegis() : base("Stormwyrm", "Aegis", Element.Wind)
    //    {
    //        HP = 1;
    //        HPPerLevel = 0.6f;
    //    }

    //}

    //public class StormwyrmMight : WeaponSkill
    //{
    //    public StormwyrmMight() : base("Stormwyrm", "Might", Element.Wind)
    //    {
    //        ATK = 1;
    //        ATKPerLevel = 0.5f;
    //    }

    //}


}
