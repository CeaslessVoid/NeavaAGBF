using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeavaAGBF.WeaponSkills
{
    public abstract class WeaponSkill
    {
        public string SkillOwner { get; private set; }
        public string SkillName { get; private set; }
        public Element SkillElement { get; private set; }

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

        protected WeaponSkill(string owner, string name, Element element)
        {
            SkillOwner = owner;
            SkillName = name;
            SkillElement = element;
        }

        public virtual void GetStatsAtLevel(int level, out float hp, out float atk, out float def)
        {
            hp = HP + HPPerLevel * level;
            atk = (ATK + ATKPerLevel * level) / 100f;
            def = DEF + DEFPerLevel * level;
        }

        public override string ToString()
        {
            return $"{SkillName} ({SkillOwner}) - Element: {SkillElement.Name}";
        }
    }

}
