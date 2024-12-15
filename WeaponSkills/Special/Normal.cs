using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace NeavaAGBF.WeaponSkills.None
{
    public class NoneArts : WeaponSkill
    {
        public NoneArts() : base("null", "Arts", Element.Special)
        {
            ATKALLELE = 1.5f;
        }

    }

    public class NoneBalance : WeaponSkill
    {
        public NoneBalance() : base("null", "Balance", Element.Special)
        {
            ATKALLELE = 2f;
        }

    }

    public class NoneBalance2 : WeaponSkill
    {
        public NoneBalance2() : base("null", "Balance II", Element.Special)
        {
            ATKALLELE = 3f;

            DMGAmpU = 1;
        }

    }

    public class NoneEnchantment : WeaponSkill
    {
        public NoneEnchantment() : base("null", "Enchantment", Element.Special)
        {
            ATKALLELE = 3;
        }

    }

    public class NoneBane : WeaponSkill
    {
        public NoneBane() : base("null", "Bane", Element.Special)
        {
            DMGAmpU = 3;
        }

    }

    public class NoneStarTouch : WeaponSkill
    {
        public NoneStarTouch() : base("null", "Star Touch", Element.Special)
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
        public StarCannonMain() : base("null", "(When Main Weapon)", Element.Special)
        {
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.StarCannonMain").Value;
        }

    }


    public class BreakerGuard : WeaponSkill
    {
        public BreakerGuard() : base("null", "Guard", Element.Special)
        {
            HP = 10;

            UncapLevel = 3;
        }

    }

    public class SpecialElementWeapon : WeaponSkill
    {
        public SpecialElementWeapon() : base("null", "(When Main Weapon)", Element.Special)
        {
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.AllElement").Value;
        }

    }

    public class NoneSummonCharge : WeaponSkill
    {
        public NoneSummonCharge() : base("null", "Essence", Element.Special)
        {
            ChargeBarGain = 5;
        }

    }

    public class NoneHpPerLight : WeaponSkill
    {
        public NoneHpPerLight() : base("null", "Stalwart's Protection", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HpPerLight";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.NoneHpPerLight").Value;
        }

    }

    public class NoneHpPerFire : WeaponSkill
    {
        public NoneHpPerFire() : base("null", "Ghastly Heart", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HpPerFire";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.NoneHpPerFire").Value;
        }

    }

    public class NoneHamBat : WeaponSkill
    {
        public NoneHamBat() : base("null", "Hearty Meal", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HamBatPassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.HamBatPassive").Value;
        }

    }

    public class ChosenBlade : WeaponSkill
    {
        public ChosenBlade() : base("null", "Smite", Element.Special)
        {
            DMGAmpU = 4;
        }

    }

    public class ChosenBlade2 : WeaponSkill
    {
        public ChosenBlade2() : base("null", "Chosen Blade", Element.Special)
        {
            UncapLevel = 4;
            SpecialKey = "ChosenBlade2";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.ChosenBlade2").Value;
        }

    }

    public class NoneEnchantment2 : WeaponSkill
    {
        public NoneEnchantment2() : base("null", "True Enchantment", Element.Special)
        {
            UncapLevel = 4;
            SpecialKey = "ChosenBlade2";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.ChosenBlade2").Value;
        }

    }

    public class TerraMight : WeaponSkill
    {
        public TerraMight() : base("null", "Terra's Might", Element.Special)
        {
            UncapLevel = 5;

            ATKALLELE = 10;
            SupplimentU = 1;
        }

    }

    public class NoneFlux : WeaponSkill
    {
        public NoneFlux() : base("null", "Flux", Element.Special)
        {
            UncapLevel = 4;
            SpecialKey = "FluxPassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.FluxPassive").Value;
        }

    }

    public class MeowMur : WeaponSkill
    {
        public MeowMur() : base("null", "Meowmur", Element.Special)
        {
            UncapLevel = 4;
            ATKALLELE = 10;
            SupplimentU = 1;
        }

    }

    public class NoneHelFire : WeaponSkill
    {
        public NoneHelFire() : base("null", "Hel Fire", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HelFirePassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.HelFirePassive").Value;
        }

    }

    public class NoneAmarok : WeaponSkill
    {
        public NoneAmarok() : base("null", "Amarok", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HelFirePassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.HelFirePassive").Value;
        }

    }

    public class NoneYelets : WeaponSkill
    {
        public NoneYelets() : base("null", "Yelets", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "HelFirePassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.HelFirePassive").Value;
        }

    }

    public class NoneTerrarian : WeaponSkill
    {
        public NoneTerrarian() : base("null", "Terrarian", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "TerrarianPassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.TerrarianPassive").Value;
        }

    }

    // Cant be fucked making more general stats
    public class NoneExploit1 : WeaponSkill
    {
        public NoneExploit1() : base("null", "Exploit", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "ExploitAPen1";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.ExploitAPen1").Value;
        }

    }

    public class GungnirPassive : WeaponSkill
    {
        public GungnirPassive() : base("null", "Piercing Heaven", Element.Special)
        {
            UncapLevel = 3;
            SpecialKey = "GungnirPassive";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.GungnirPassive").Value;
        }

    }

    public class NoneToxicosis : WeaponSkill
    {
        public NoneToxicosis() : base("null", "Toxicosis", Element.Special)
        {
            //UncapLevel = 3;
            SpecialKey = "Toxicosis";
            CustomText = Language.GetText("Mods.NeavaAGBF.CustomText.Toxicosis").Value;
        }

    }
}
