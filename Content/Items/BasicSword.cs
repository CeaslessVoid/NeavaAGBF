using NeavaAGBF.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NeavaAGBF;
using NeavaAGBF.WeaponSkills;
using NeavaAGBF.WeaponSkills.Aegis;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NeavaAGBF.WeaponSkills.Might;

namespace NeavaAGBF.Content.Items
{ 
	public class BasicSword : ModItem
	{
        private List<WeaponSkill> weaponSkills;
        private int currentLevel;
        private int maxLevel;
        private Element weaponElement;
        public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

            weaponSkills = new List<WeaponSkill>
            {
                new StormwyrmAegis(),
                new StormwyrmMight()
            };

            currentLevel = 0;
            maxLevel = 3;
            weaponElement = Element.Wind;
        }


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Insert(0, new TooltipLine(Mod, "Element", weaponElement.ToString())
            {
                OverrideColor = weaponElement.TooltipColor
            });

            tooltips.Add(new TooltipLine(Mod, "Spacer", "-------------------------------"));

            foreach (var skill in weaponSkills)
            {
                AddSkillTooltips(tooltips, skill, currentLevel, weaponElement);
            }

            tooltips.Add(new TooltipLine(Mod, "Level", $"Level: {currentLevel}/{maxLevel}"));
        }

        private void AddSkillTooltips(List<TooltipLine> tooltips, WeaponSkill skill, float level, Element element)
        {
            float hpBonus = skill.HP + (float)(skill.HPPerLevel * level);
            float atkBonus = skill.ATK + (float)(skill.ATKPerLevel * level);
            float defBonus = skill.DEF + (float)(skill.DEFPerLevel * level);
            float critRateBonus = skill.CritRate + (float)(skill.CritRatePerLevel * level);
            float critDamageBonus = skill.CritDamage + (float)(skill.CritDamagePerLevel * level);
            float attackSpeedBonus = skill.AttackSpeed + (float)(skill.AttackSpeedPerLevel * level);
            float movementSpeedBonus = skill.MovementSpeed + (float)(skill.MovementSpeedPerLevel * level);

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

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
