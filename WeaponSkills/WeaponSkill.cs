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
        public string SkillOwner { get; private set; }
        public string SkillName { get; private set; }
        public Element SkillElement { get; private set; }

        public int UncapLevel { get; private set; }

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

        public float ChargeBarGain { get; protected set; }
        public float ChargAttack { get; protected set; }

        public float ChargeBarGainPerLevel { get; protected set; }
        public float ChargAttackPerLevel { get; protected set; }

        public string CustomText { get; protected set; }

        public float ATKALLELE { get; protected set; }
        public float ATKALLELEPerLevel { get; protected set; }

        public float DamageCap { get; protected set; }
        public float DamageCapPerLevel { get; protected set; }

        public virtual Color TooltipColor { get; protected set; } = Color.Green;

        protected WeaponSkill(string owner, string name, Element element)
        {
            SkillOwner = owner;
            SkillName = name;
            SkillElement = element;
        }

        public override string ToString()
        {
            return $"{SkillName} ({SkillOwner}) - Element: {SkillElement.Name}";
        }
    }

}
