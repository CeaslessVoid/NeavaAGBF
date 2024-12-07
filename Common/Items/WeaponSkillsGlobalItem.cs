﻿using NeavaAGBF.WeaponSkills;
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
using NeavaAGBF.Common.Players;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;

namespace NeavaAGBF.Common.Items
{
    public class WeaponSkillsGlobalItem : GlobalItem
    {

        public List<WeaponSkill> weaponSkills = new();
        public int currentLevel = 0;
        public int maxLevel = 0;
        public Element weaponElement = null;

        public float chargeGain = 0.25f;
        public float chargeAttackDamage = 1f;

        public string chargeName = null;
        public string chargeAttackString = null;

        //public Action<Player> chargeAttack = (Player player) =>
        //{

        //};

        public Action<Player> chargeAttack = null;

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
            tag["currentLevel"] = currentLevel;
            tag["maxLevel"] = maxLevel;
            tag["currentUncap"] = currentUncap;

        }

        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.ContainsKey("currentLevel"))
                currentLevel = tag.GetInt("currentLevel");

            if (tag.ContainsKey("maxLevel"))
                maxLevel = tag.GetInt("maxLevel");

            if (tag.ContainsKey("currentUncap"))
                currentUncap = tag.GetInt("currentUncap");
        }

        public override bool CanRightClick(Item item)
        {

            return CanUpgrade(item) && Main.mouseItem.stack >= CalculateRequiredSkillGems() && Main.mouseItem.type == ModContent.ItemType<SkillGem>();

        }
        private bool CanUpgrade(Item item)
        {
            return currentLevel < maxLevel && WeaponType != null && maxLevel > 0;
        }

        private int CalculateRequiredSkillGems()
        {
            return (int)Math.Round(Math.PI * Math.Exp(currentLevel / Math.PI) - 2);
        }

        public override void RightClick(Item item, Player player)
        {

            WeaponSkillsGlobalItem weaponItem = item.GetGlobalItem<WeaponSkillsGlobalItem>();

            if (weaponItem.maxLevel < 1)
            {
                Main.NewText(item.stack.ToString());

                return;
            }

            int requiredGems = CalculateRequiredSkillGems();

            if (!CanUpgrade(item))return;

            if (player == null || item == null)
                return;

            Main.mouseItem.stack -= requiredGems;

            if (Main.mouseItem.stack <0)
            {
                Main.mouseItem.TurnToAir();
            }

            item.stack++;

            
            weaponItem.currentLevel++;
        }
        
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (weaponElement != null)
            {
                tooltips.Insert(1, new TooltipLine(Mod, "Element", $"{weaponElement.ToString()} /")
                {
                    OverrideColor = weaponElement.TooltipColor
                });
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

            //if (chargeAttackString != null)
            //{
            //    tooltips.Add(new TooltipLine(Mod, "Spacer", $"-")
            //    {
            //        //OverrideColor = skill.TooltipColor
            //        OverrideColor = Color.Transparent,
            //    });

            //    tooltips.Add(new TooltipLine(Mod, "Charge", $"       {chargeName}"));
            //    tooltips.Add(new TooltipLine(Mod, "ChargeInfo", $"        {chargeAttackString}"));

            //    tooltips.Add(new TooltipLine(Mod, "Spacer", $"-")
            //    {
            //        //OverrideColor = skill.TooltipColor
            //        OverrideColor = Color.Transparent,
            //    });
            //}

            foreach (var skill in weaponSkills)
            {
                AddSkillTooltips(tooltips, skill, currentLevel, weaponElement);
            }

            if (maxLevel > 0)
            {
                bool isShiftHeld = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);

                tooltips.Add(new TooltipLine(Mod, "Level", $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.SkillLevel").Value}: {currentLevel}/{maxLevel} [{(isShiftHeld? $"{skillLevelPerCap + " " +Language.GetText("Mods.NeavaAGBF.WeaponSkill.PerLevel").Value}" : Language.GetText("Mods.NeavaAGBF.SimpleText.HoldShift").Value)}] "));
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

            else if (line.Name == "Element" && line.Mod == Mod.Name && WeaponType != null)
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

            else if(line.Name == "SkillName" && line.Mod == Mod.Name)
            {
                string[] splitText = line.Text.Split(new[] { ' ' }, 3);
                if (splitText.Length < 2)
                    return;

                string ownerName = splitText[0];
                string elementName = splitText[1];
                string skillName = splitText[2];

                string formattedSkillName = skillName.Replace(" ", "_");

                string imagePath = $"NeavaAGBF/WeaponSkills/{elementName}/{formattedSkillName}";

                Asset<Texture2D> textureAsset = ModContent.Request<Texture2D>(imagePath, AssetRequestMode.ImmediateLoad);
                if (textureAsset == null)
                    return;

                Texture2D skillIcon = textureAsset.Value;

                float iconWidth = skillIcon.Width;
                line.X += (int)iconWidth;

                //line.MaxWidth += (int)iconWidth;

                Vector2 iconPosition = new Vector2(line.X - skillIcon.Width - 4, line.Y);

                Main.spriteBatch.Draw(
                    skillIcon,
                    iconPosition,
                    Color.White
                );

                Vector2 textPosition = new Vector2(iconPosition.X + iconWidth + 4, line.Y);

                string skillOwnerDisplay = ownerName == "null's" ? "" : $"{ownerName}";

                ChatManager.DrawColorCodedStringWithShadow(
                    Main.spriteBatch,
                    FontAssets.MouseText.Value,
                    $"{skillOwnerDisplay} {skillName}", // Reconstruct the line
                    textPosition,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    Vector2.One
                );
            }

            else if(line.Name == "Charge" && line.Mod == Mod.Name)
            {
                // We will probably do special ones later on

                string imagePath = $"NeavaAGBF/Content/Items/ChargeAttackBasic";

                Asset<Texture2D> textureAsset = ModContent.Request<Texture2D>(imagePath, AssetRequestMode.ImmediateLoad);
                if (textureAsset == null)
                    return;

                Texture2D Icon = textureAsset.Value;

                Vector2 iconPosition = new Vector2(line.X - 4, line.Y);

                Main.spriteBatch.Draw(
                    Icon,
                    iconPosition,
                    Color.White
                );
            }

        }


        private void AddSkillTooltips(List<TooltipLine> tooltips, WeaponSkill skill, int level, Element element)
        {

            Player player = Main.LocalPlayer;

            var modPlayer = player.GetModPlayer<StatHandler>();

            float modifier = modPlayer.GetStatMultiplier(skill.SkillOwner);

            float hpBonus = (skill.HP + (skill.HPPerLevel * level)) * modifier;
            float atkBonus = (skill.ATK + (skill.ATKPerLevel * level) * modifier);
            float defBonus = (skill.DEF + (skill.DEFPerLevel * level) * modifier);
            float critRateBonus = (skill.CritRate + (skill.CritRatePerLevel * level) * modifier);
            float critDamageBonus = (skill.CritDamage + (skill.CritDamagePerLevel * level) * modifier);
            float attackSpeedBonus = (skill.AttackSpeed + (skill.AttackSpeedPerLevel * level) * modifier);
            float movementSpeedBonus = (skill.MovementSpeed + (skill.MovementSpeedPerLevel * level) * modifier);

            float chargeBarBonus = (skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * level) * modifier);
            float chargeAttackBonus = (skill.ChargAttack + (skill.ChargAttackPerLevel * level) * modifier);

            string customEffect = skill.CustomText;

            float allatackBonus = (skill.ATKALLELE + (skill.ATKALLELEPerLevel * level));

            float CapBonus = (skill.DamageCap + (skill.DamageCapPerLevel * level));

            string skillOwnerDisplay = string.IsNullOrEmpty(skill.SkillOwner) ? "null" : $"{skill.SkillOwner}'s";
            TooltipLine skillNameLine = new TooltipLine(Mod, "SkillName", $"{skillOwnerDisplay} {skill.SkillElement} {skill.SkillName}")
            {
                //OverrideColor = skill.TooltipColor
                OverrideColor = Color.Transparent,
            };
            tooltips.Add(skillNameLine);

            if (!string.IsNullOrEmpty(customEffect))
            {
                tooltips.Add(new TooltipLine(Mod, "Custom" ,$"{customEffect}"));
                return;
            }

            bool isShiftHeld = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);

            List<string> bonuses = new List<string>(); // Fire!!!

            if (!isShiftHeld)
            {
                if (hpBonus != 0) AddStatString(bonuses, "Health", (float)Math.Round(hpBonus, 1), true, null);
                if (atkBonus != 0) AddStatString(bonuses, $"{element.Name} Attack", (float)Math.Round(atkBonus, 1), true, element.TooltipColor);
                if (defBonus != 0) AddStatString(bonuses, "Defense", (int)Math.Round(defBonus, 1), false, null);
                if (critRateBonus != 0) AddStatString(bonuses, $"{element.Name} Critical Rate", (int)Math.Round(critRateBonus, 1), true, element.TooltipColor);
                if (critDamageBonus != 0) AddStatString(bonuses, $"{element.Name} Critical Damage", (float)Math.Round(critDamageBonus, 1), true, element.TooltipColor);
                if (attackSpeedBonus != 0) AddStatString(bonuses, $"{element.Name} Attack Speed", (float)Math.Round(attackSpeedBonus, 1), true, element.TooltipColor);
                if (movementSpeedBonus != 0) AddStatString(bonuses, "Movement Speed", (float)Math.Round(movementSpeedBonus, 1), true, null);
                if (chargeBarBonus != 0) AddStatString(bonuses, "Charge Bar Gain", (float)Math.Round(chargeBarBonus, 1), true, element.TooltipColor);
                if (chargeAttackBonus != 0) AddStatString(bonuses, $"{element.Name} Charge Attack Damage", (float)Math.Round(chargeAttackBonus, 1), true, element.TooltipColor);

                if (allatackBonus != 0) AddStatString(bonuses, $"All Element Attack Damage", (float)Math.Round(allatackBonus, 1), true, element.TooltipColor);

                if (CapBonus != 0) AddStatString(bonuses, $"Damage Cap", (float)Math.Round(CapBonus, 1), true, element.TooltipColor);
            }
            else
            {
                if (hpBonus != 0) AddStatString(bonuses, "Health", $"({skill.HP} + {skill.HPPerLevel} per level) * {modifier}", null);
                if (atkBonus != 0) AddStatString(bonuses, $"{element.Name} Attack", $"({skill.ATK} + {skill.ATKPerLevel} per level) * {modifier}", element.TooltipColor);
                if (defBonus != 0) AddStatString(bonuses, "Defense", $"({skill.DEF} + {skill.DEFPerLevel} per level) * {modifier}", null);
                if (critRateBonus != 0) AddStatString(bonuses, $"{element.Name} Critical Rate", $"({skill.CritRate} + {skill.CritRatePerLevel} per level) * {modifier}", element.TooltipColor);
                if (critDamageBonus != 0) AddStatString(bonuses, $"{element.Name} Critical Damage", $"({skill.CritDamage} + {skill.CritDamagePerLevel} per level) * {modifier}", element.TooltipColor);
                if (attackSpeedBonus != 0) AddStatString(bonuses, $"{element.Name} Attack Speed", $"({skill.AttackSpeed} + {skill.AttackSpeedPerLevel} per level) * {modifier}", element.TooltipColor);
                if (movementSpeedBonus != 0) AddStatString(bonuses, "Movement Speed", $"({skill.MovementSpeed} + {skill.MovementSpeedPerLevel} per level) * {modifier}", null);
                if (chargeBarBonus != 0) AddStatString(bonuses, "Charge Bar Gain", $"({skill.ChargeBarGain} + {skill.ChargeBarGainPerLevel} per level) * {modifier}", element.TooltipColor);
                if (chargeAttackBonus != 0) AddStatString(bonuses, $"{element.Name} Charge Attack Damage", $"({skill.ChargAttack} + {skill.ChargAttackPerLevel} per level) * {modifier}", element.TooltipColor);

                if (allatackBonus != 0) AddStatString(bonuses, $"All Element Attack Damage", $"({skill.ATKALLELE} + {skill.ATKALLELEPerLevel} per level)", element.TooltipColor);

                if (CapBonus != 0) AddStatString(bonuses, $"Damage Cap", $"({skill.DamageCap} + {skill.DamageCapPerLevel} per level)", element.TooltipColor);
            }

            if (bonuses.Count > 0)
            {
                string combinedText = CombineBonusesIntoSentence(bonuses);

                TooltipLine statLine = new TooltipLine(Mod, "CombinedBonuses", "        " + combinedText)
                {
                    OverrideColor = Color.White
                };
                tooltips.Add(statLine);
            }

        }

        private void AddStatString(List<string> bonuses, string statName, float value, bool isPercentage, Color? color)
        {
            if (value != 0)
            {
                string sign = value > 0 ? "+" : "";
                string formattedValue = isPercentage ? $"{sign}{value}%" : $"{sign}{value}";
                bonuses.Add($"{formattedValue} boost to {statName}");
            }
        }

        void AddStatString(List<string> bonuses, string statName, string detailedValue, Color? color)
        {
            if (!string.IsNullOrEmpty(detailedValue))
            {
                bonuses.Add($"{detailedValue} boost to {statName}");
            }
        }

        private string CombineBonusesIntoSentence(List<string> bonuses)
        {
            if (bonuses.Count == 1)
            {
                return bonuses[0] + ".";
            }

            string result = string.Join(", ", bonuses.Take(bonuses.Count - 1));
            result += $", and {bonuses.Last()}.";
            return result;
        }
    }
}
