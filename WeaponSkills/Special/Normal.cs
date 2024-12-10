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
            ATKALLELE = 1.5f;
        }

    }

    public class NoneBalance : WeaponSkill
    {
        public NoneBalance() : base("null", "Balance", Element.Speical)
        {
            ATKALLELE = 2f;
        }

    }

    public class NoneEnchantment : WeaponSkill
    {
        public NoneEnchantment() : base("null", "Enchantment", Element.Speical)
        {
            ATKALLELE = 3;
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
            ATKALLELE = 3;
        }

    }

    public class NoneWaterMoon : WeaponSkill
    {
        public NoneWaterMoon() : base("null", "Blue Moon", Element.Water)
        {

            AttackSpeed = 3.0f;

            ATK = 2.5f;

            CritRate = 1.5f;
        }

    }

    public class NoneFireSun : WeaponSkill
    {
        public NoneFireSun() : base("null", "Red Sun", Element.Fire)
        {
            CritDamage = 1.5f;

            ATK = 2.5f;

            CritRate = 1.5f;
        }

    }

    public class NoneAmmoSave : WeaponSkill
    {
        public NoneAmmoSave() : base("null", "Efficiency", Element.Fire)
        {
            SaveAmmo = 5f;
        }

    }

    public class StarCannonMain : WeaponSkill
    {
        public StarCannonMain() : base("null", "(When Main Weapon)", Element.Speical)
        {
            CustomText = "90% Hit to charge bar gain. 200% Boost to charge attack damage.";
        }

    }
}
