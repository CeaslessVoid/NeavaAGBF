using NeavaAGBF.WeaponSkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using NeavaAGBF.Content.Items;

namespace NeavaAGBF.Common.Items
{
    public class WeaponSkillsGlobalItem : GlobalItem
    {
        public List<WeaponSkill> weaponSkills = new();
        public int currentLevel = 0;
        public int maxLevel = 0;
        public Element weaponElement = null;

        public float chargeGain = 1f;
        public float chargeAttackDamage = 1f;

        public string chargeName = null;
        public string chargeAttackString = null;

        public Action<Player> chargeAttack = (Player player) =>
        {
            Main.NewText("Default Charge Attack activated!", Color.LightGreen);
        };

        public int baseUncap = 0;
        public int maxUncap = 0;
        public int currentUncap = 0;

        public int skillLevelPerCap = 1;

        public override bool InstancePerEntity => true;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (weaponElement != null)
            {
                tooltips.Insert(1, new TooltipLine(Mod, "Element", weaponElement.ToString())
                {
                    OverrideColor = weaponElement.TooltipColor
                });

                tooltips.Add(new TooltipLine(Mod, "Spacer", "-------------------------------"));
            }

            if (chargeAttackString != null)
            {
                tooltips.Add(new TooltipLine(Mod, "Charge", $"{chargeName}"));
                tooltips.Add(new TooltipLine(Mod, "Charge", $"{chargeAttackString}"));

                tooltips.Add(new TooltipLine(Mod, "Spacer", "-------------------------------"));
            }

            foreach (var skill in weaponSkills)
            {
                AddSkillTooltips(tooltips, skill, currentLevel, weaponElement);
            }

            if (maxLevel > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "Level", $"Level: {currentLevel}/{maxLevel}"));
            }
        }

        private void AddSkillTooltips(List<TooltipLine> tooltips, WeaponSkill skill, int level, Element element)
        {
            float hpBonus = skill.HP + (skill.HPPerLevel * level);
            float atkBonus = skill.ATK + (skill.ATKPerLevel * level);
            float defBonus = skill.DEF + (skill.DEFPerLevel * level);
            float critRateBonus = skill.CritRate + (skill.CritRatePerLevel * level);
            float critDamageBonus = skill.CritDamage + (skill.CritDamagePerLevel * level);
            float attackSpeedBonus = skill.AttackSpeed + (skill.AttackSpeedPerLevel * level);
            float movementSpeedBonus = skill.MovementSpeed + (skill.MovementSpeedPerLevel * level);

            float chargeBarBonus = skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * level);
            float chargeAtackBonus = skill.ChargAttack + (skill.ChargAttackPerLevel * level);

            TooltipLine skillNameLine = new TooltipLine(Mod, "SkillName", $"{skill.SkillOwner}'s {skill.SkillName}")
            {
                OverrideColor = skill.TooltipColor
            };
            tooltips.Add(skillNameLine);

            AddStatTooltip(tooltips, "Health", hpBonus, true, null);
            AddStatTooltip(tooltips, $"{element.Name} Attack", atkBonus, true, element.TooltipColor);
            AddStatTooltip(tooltips, "Defense", defBonus, false, null);
            AddStatTooltip(tooltips, $"{element.Name} Crit Rate", critRateBonus, true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} Crit Damage", critDamageBonus, true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} Attack Speed", attackSpeedBonus, true, element.TooltipColor);
            AddStatTooltip(tooltips, "Movement Speed", movementSpeedBonus, true, null);

            AddStatTooltip(tooltips, $"Charge Bar Gain", chargeBarBonus, true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} Charge Attack", chargeAtackBonus, true, element.TooltipColor);
        }

        private void AddStatTooltip(List<TooltipLine> tooltips, string statName, float value, bool isPercentage, Color? color)
        {
            if (value != 0)
            {
                string sign = value > 0 ? "+" : "";
                string formattedValue = isPercentage ? $"{sign}{value}%" : $"{sign}{value}";
                TooltipLine statLine = new TooltipLine(Mod, $"{statName}Bonus", $"   {statName} {formattedValue}");
                if (color.HasValue)
                {
                    statLine.OverrideColor = color.Value;
                }
                tooltips.Add(statLine);
            }
        }
    }
}
