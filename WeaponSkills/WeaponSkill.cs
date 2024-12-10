using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NeavaAGBF.Common.Players;

namespace NeavaAGBF.WeaponSkills
{
    public abstract class WeaponSkill
    {
        public string SkillOwner { get; }
        public string SkillName { get; }
        public Element SkillElement { get; }

        public int UncapLevel { get; private set; }

        // Stats and bonuses
        public float ATK { get; protected set; }
        public float HP { get; protected set; }
        public float DEF { get; protected set; }
        public float CritRate { get; protected set; }
        public float CritDamage { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public float MovementSpeed { get; protected set; }

        public float ATKPerLevel { get; protected set; }
        public float HPPerLevel { get; protected set; }
        public float DEFPerLevel { get; protected set; }
        public float CritRatePerLevel { get; protected set; }
        public float CritDamagePerLevel { get; protected set; }
        public float AttackSpeedPerLevel { get; protected set; }
        public float MovementSpeedPerLevel { get; protected set; }

        // Charge-related stats
        public float ChargeBarGain { get; protected set; }
        public float ChargAttack { get; protected set; }
        public float ChargeBarGainPerLevel { get; protected set; }
        public float ChargAttackPerLevel { get; protected set; }

        // Miscellaneous
        public string CustomText { get; protected set; }

        public float ATKALLELE { get; protected set; }
        public float ATKALLELEPerLevel { get; protected set; }

        public float Enmity { get; protected set; }
        public float EnmityPerLevel { get; protected set; }
        public float Stamina { get; protected set; }
        public float StaminaPerLevel { get; protected set; }
        public float DMGReduc { get; protected set; }
        public float DMGReducPerLevel { get; protected set; }
        public float DMGAmp { get; protected set; }
        public float DMGAmpU { get; protected set; }
        public float SaveAmmo { get; protected set; }
        public float Echo { get; protected set; }
        public float EchoPerLevel { get; protected set; }
        public float FlatAtk { get; protected set; }
        public float FlatAtkPerLevel { get; protected set; }


        public Color TooltipColor { get; protected set; } = Color.Green;

        protected WeaponSkill(string owner, string name, Element element)
        {
            SkillOwner = owner;
            SkillName = name;
            SkillElement = element;
        }

        public override string ToString() => $"{SkillName} ({SkillOwner}) - Element: {SkillElement.Name}";
    }

}
