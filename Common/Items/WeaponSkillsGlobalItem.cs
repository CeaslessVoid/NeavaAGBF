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
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI.Chat;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using NeavaAGBF.Content.Items.Others;
using Terraria.Audio;
using Terraria.ID;

namespace NeavaAGBF.Common.Items
{
    public class WeaponSkillsGlobalItem : GlobalItem
    {
        public List<WeaponSkill> weaponSkills = new();
        public int currentLevel = 0;
        public int maxLevel = 0;
        public Element weaponElement = null;

        public float chargeGain = 0.1f;
        public float chargeAttackDamage = 1f;

        public string chargeName = null;
        public string chargeAttackString = null;

        public Action<Player> chargeAttack = (Player player) =>
        {

        };

        //public int baseUncap = 0;
        public int maxUncap = 0;

        public int currentUncap = 0;
        public int skillLevelPerCap = 1;

        private (int goldStars, int blueStars, int emptyStars) TooltipStarData;

        public UncapGroup UncapGroup = LoadUncapGroups.GetUncapGroup("uncapGroupTest");

        public WeaponType WeaponType { get; set; } = null;
        public override bool InstancePerEntity => true;

        public WeaponSkillsGlobalItem()
        {
            //currentUncap = baseUncap; // ???
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            base.SaveData(item, tag);

            tag["currentLevel"] = currentLevel;
            tag["maxLevel"] = maxLevel;
            tag["currentUncap"] = currentUncap;

        }

        public override void LoadData(Item item, TagCompound tag)
        {
            base.LoadData(item, tag);

            if (tag.ContainsKey("currentLevel"))
                currentLevel = tag.GetInt("currentLevel");

            if (tag.ContainsKey("maxLevel"))
                maxLevel = tag.GetInt("maxLevel");

            if (tag.ContainsKey("currentUncap"))
                currentUncap = tag.GetInt("currentUncap");
        }

        public override bool CanRightClick(Item item)
        {
            return CanUpgrade(item) && (Main.mouseItem.stack >= CalculateRequiredSkillGems() && item.type != ModContent.ItemType<SkillGem>());

        }
        private bool CanUpgrade(Item item)
        {
            return currentLevel < maxLevel && WeaponType != null;
        }

        private int CalculateRequiredSkillGems()
        {
            return (int)Math.Round(Math.PI * Math.Exp(currentLevel / Math.PI) - 2);
        }

        public override void RightClick(Item item, Player player)
        {
            int requiredGems = CalculateRequiredSkillGems();
            Item temp = item.Clone();

            if (player == null || item == null)
                return;

            WeaponSkillsGlobalItem clonedItem = temp.GetGlobalItem<WeaponSkillsGlobalItem>();

            clonedItem.currentLevel++;

            player.GetItem(Main.myPlayer, temp, GetItemSettings.InventoryEntityToPlayerInventorySettings);

            Main.mouseItem.stack -= requiredGems;
            if (Main.mouseItem.stack <= 0)
            {
                Main.mouseItem.TurnToAir();
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (weaponElement != null)
            {
                tooltips.Insert(1, new TooltipLine(Mod, "Element", $"{weaponElement.ToString()} /")
                {
                    OverrideColor = weaponElement.TooltipColor
                });

                tooltips.Add(new TooltipLine(Mod, "Spacer", "-------------------------------"));
            }

            int goldStars = Math.Min(3, currentUncap);
            int blueStars = Math.Max(0, currentUncap - 3);
            int emptyStars = maxUncap - currentUncap;

            if (maxUncap > 0)
            {
                tooltips.Insert(1, new TooltipLine(Mod, "Stars", "-")
                {
                    OverrideColor = Color.Transparent
                });
            }

            TooltipStarData = (goldStars, blueStars, emptyStars);

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
                tooltips.Add(new TooltipLine(Mod, "Level", $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.SkillLevel").Value}: {currentLevel}/{maxLevel}"));
            }

            if (currentLevel < maxLevel)
            {
                int requiredGems = CalculateRequiredSkillGems();
                string gemText = $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Requires").Value} {requiredGems} ";
                gemText += $"[i:{ModContent.ItemType<SkillGem>()}] {Language.GetText("Mods.NeavaAGBF.WeaponSkill.SkillGems").Value}";

                tooltips.Add(new TooltipLine(Mod, "UpgradeRequirement", gemText)
                {
                    OverrideColor = Color.Orange
                });
            }

        }

        public override void PostDrawTooltipLine(Item item, DrawableTooltipLine line)
        {
            if (line.Name == "Stars" && line.Mod == Mod.Name)
            {
                var (goldStars, blueStars, emptyStars) = TooltipStarData;

                DynamicSpriteFont font = FontAssets.MouseText.Value;
                float scale = 1f;
                Vector2 start = new Vector2(line.X, line.Y);

                Color goldColor = new Color(255, 215, 0);
                Color blueColor = new Color(135, 206, 250);
                Color grayColor = new Color(192, 192, 192);

                // Draw gold stars
                for (int i = 0; i < goldStars; i++)
                {
                    string star = "★";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, star, start, goldColor, 0f, Vector2.Zero, new Vector2(scale));
                    start.X += font.MeasureString(star).X * scale;
                }

                // Draw blue stars
                for (int i = 0; i < blueStars; i++)
                {
                    string star = "★";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, star, start, blueColor, 0f, Vector2.Zero, new Vector2(scale));
                    start.X += font.MeasureString(star).X * scale;
                }

                // Draw empty stars
                for (int i = 0; i < emptyStars; i++)
                {
                    string star = "☆";
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, star, start, grayColor, 0f, Vector2.Zero, new Vector2(scale));
                    start.X += font.MeasureString(star).X * scale;
                }
            }

            if (line.Name == "Element" && line.Mod == Mod.Name && WeaponType != null)
            {
                DynamicSpriteFont font = FontAssets.MouseText.Value;
                float elementTextWidth = font.MeasureString(line.Text).X;

                Vector2 position = new Vector2(line.X + elementTextWidth, line.Y);

                WeaponType.DrawIcon(position);

                Vector2 typeNamePosition = new Vector2(position.X + 25, line.Y);

                ChatManager.DrawColorCodedStringWithShadow(
                    Main.spriteBatch,
                    font,
                    WeaponType.Name,
                    typeNamePosition,
                    weaponElement.TooltipColor,
                    0f,
                    Vector2.Zero,
                    Vector2.One
                );
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

            string skillOwnerDisplay = string.IsNullOrEmpty(skill.SkillOwner) ? "" : $"{skill.SkillOwner}'s";
            TooltipLine skillNameLine = new TooltipLine(Mod, "SkillName", $"{skillOwnerDisplay} {skill.SkillName}")
            {
                OverrideColor = skill.TooltipColor
            };
            tooltips.Add(skillNameLine);


            AddStatTooltip(tooltips, Language.GetText("Mods.NeavaAGBF.WeaponSkill.Health").Value, (float)Math.Round(hpBonus, 1), true, null);
            AddStatTooltip(tooltips, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}", (float)Math.Round(atkBonus, 1), true, element.TooltipColor);
            AddStatTooltip(tooltips, Language.GetText("Mods.NeavaAGBF.WeaponSkill.Def").Value, (int)Math.Round(defBonus, 1), false, null);
            AddStatTooltip(tooltips, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CR").Value}", (int)Math.Round(critRateBonus, 1), true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CD").Value}", (float)Math.Round(critDamageBonus, 1), true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.AS").Value}", (float)Math.Round(attackSpeedBonus, 1), true, element.TooltipColor);
            AddStatTooltip(tooltips, Language.GetText("Mods.NeavaAGBF.WeaponSkill.MS").Value, (float)Math.Round(movementSpeedBonus, 1), true, null);

            AddStatTooltip(tooltips, Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBGain").Value, (float)Math.Round(chargeBarBonus, 1), true, element.TooltipColor);
            AddStatTooltip(tooltips, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBDamage").Value}", (float)Math.Round(chargeAtackBonus, 1), true, element.TooltipColor);
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
