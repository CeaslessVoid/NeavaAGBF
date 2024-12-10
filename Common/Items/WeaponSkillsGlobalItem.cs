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
using NeavaAGBF.Common.Players;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Newtonsoft.Json.Linq;

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

        //public Action<Player> chargeAttack = (Player player) =>
        //{

        //};

        public Action<Player, float> chargeAttack = null;

        //public int baseUncap = 0;
        public int maxUncap = 0;

        public int currentUncap = 0;
        public int skillLevelPerCap = 1;

        private (int goldStars, int blueStars, int emptyStars) TooltipStarData;

        public UncapGroup UncapGroup = LoadUncapGroups.GetUncapGroup("uncapGroupTest");

        public WeaponType WeaponType { get; set; } = null;
        public override bool InstancePerEntity => true;

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
            if (!CanUpgrade(item) || player == null || item == null) return;

            int requiredGems = CalculateRequiredSkillGems();
            Main.mouseItem.stack -= requiredGems;
            if (Main.mouseItem.stack <= 0) Main.mouseItem.TurnToAir();

            item.stack++;
            currentLevel++;
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

            if (chargeAttackString != null)
            {
                tooltips.Add(new TooltipLine(Mod, "Charge", $"       {chargeName}"));
                tooltips.Add(new TooltipLine(Mod, "ChargeInfo", $"        {chargeAttackString}"));
            }

            foreach (var skill in weaponSkills)
            {
                AddSkillTooltips(tooltips, skill, currentLevel);
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

                Asset<Texture2D> textureAsset = null;

                if (ownerName != "null's")
                    textureAsset = ModContent.Request<Texture2D>(imagePath, AssetRequestMode.ImmediateLoad);
                else
                    textureAsset = ModContent.Request<Texture2D>("NeavaAGBF/WeaponSkills/Special/Special", AssetRequestMode.ImmediateLoad);


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


        private void AddSkillTooltips(List<TooltipLine> tooltips, WeaponSkill skill, int level)
        {

            Player player = Main.LocalPlayer;

            var modPlayer = player.GetModPlayer<StatHandler>();

            float modifier = modPlayer.GetStatMultiplier(skill.SkillOwner);
            Element element = skill.SkillElement;

            float hpBonus = (skill.HP + (skill.HPPerLevel * level)) * modifier;
            float atkBonus = (skill.ATK + (skill.ATKPerLevel * level)) * modifier;
            float defBonus = (skill.DEF + (skill.DEFPerLevel * level)) * modifier;
            float critRateBonus = (skill.CritRate + (skill.CritRatePerLevel * level)) * modifier;
            float critDamageBonus = (skill.CritDamage + (skill.CritDamagePerLevel * level)) * modifier;
            float attackSpeedBonus = (skill.AttackSpeed + (skill.AttackSpeedPerLevel * level)) * modifier;
            float movementSpeedBonus = (skill.MovementSpeed + (skill.MovementSpeedPerLevel * level)) * modifier;

            float chargeBarBonus = (skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * level)) * modifier;
            float chargeAttackBonus = (skill.ChargAttack + (skill.ChargAttackPerLevel * level)) * modifier;

            string customEffect = skill.CustomText;

            float allatackBonus = (skill.ATKALLELE + (skill.ATKALLELEPerLevel * level));

            float enmityBonus = (skill.Enmity + (skill.EnmityPerLevel * level)) * modifier;
            float damageReductionBonus = (skill.DMGReduc + (skill.DMGReducPerLevel * level)) * modifier;

            float staminaBonus = (skill.Stamina + (skill.StaminaPerLevel * level)) * modifier;

            float damageAmpBonus = (skill.DMGAmp) * modifier;
            float damageAmpBonusU = (skill.DMGAmpU);

            float echo = (skill.Echo + (skill.EchoPerLevel * level)) * modifier;

            float flatAtk = (skill.FlatAtk + (skill.FlatAtkPerLevel * level)) * modifier;

            string skillOwnerDisplay = string.IsNullOrEmpty(skill.SkillOwner) ? "null" : $"{skill.SkillOwner}'s";
            TooltipLine skillNameLine = new TooltipLine(Mod, "SkillName", $"{skillOwnerDisplay} {skill.SkillElement} {skill.SkillName}")
            {
                //OverrideColor = skill.TooltipColor
                OverrideColor = Color.Transparent,
            };
            tooltips.Add(skillNameLine);

            if (!string.IsNullOrEmpty(customEffect))
            {
                tooltips.Add(new TooltipLine(Mod, "Custom" ,$"        {customEffect}"));
                return;
            }

            bool isShiftHeld = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);

            List<string> bonuses = new List<string>(); // Fire!!! ICE :SKULL:

            if (!isShiftHeld)
            {
                if (hpBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Health").Value}", (float)Math.Round(hpBonus, 2), true, null);
                if (atkBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}", (float)Math.Round(atkBonus, 2), true, element.TooltipColor);
                if (defBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Def").Value}", (int)Math.Round(defBonus, 1), false, null);
                if (critRateBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CR").Value}", (float)Math.Round(critRateBonus, 1), true, element.TooltipColor);
                if (critDamageBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CD").Value}", (float)Math.Round(critDamageBonus, 1), true, element.TooltipColor);
                if (attackSpeedBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.AS").Value}", (float)Math.Round(attackSpeedBonus, 1), true, element.TooltipColor);
                if (movementSpeedBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.MS").Value}", (float)Math.Round(movementSpeedBonus, 1), true, null);
                if (chargeBarBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBGain").Value}", (float)Math.Round(chargeBarBonus, 1), true, element.TooltipColor);
                if (chargeAttackBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBDamage").Value}", (float)Math.Round(chargeAttackBonus, 1), true, element.TooltipColor);

                if (allatackBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}", (float)Math.Round(allatackBonus, 1), true, element.TooltipColor);

                if (enmityBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Enmity").Value}", (float)Math.Round(enmityBonus, 1), true, element.TooltipColor);
                if (staminaBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Stamina").Value}", (float)Math.Round(staminaBonus, 1), true, element.TooltipColor);

                if (damageReductionBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.DR").Value}", (float)Math.Round(damageReductionBonus, 1), true, element.TooltipColor);

                if (damageAmpBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.DamageAmp").Value}", (float)Math.Round(damageAmpBonus, 1), true, element.TooltipColor);
                if (damageAmpBonusU != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.DamageAmp").Value}", (float)Math.Round(damageAmpBonusU, 1), true, element.TooltipColor);

                if (skill.SaveAmmo != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.AmmoE").Value}", (float)Math.Round(skill.SaveAmmo, 1), true, element.TooltipColor);

                if (echo != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Echo").Value}", (float)Math.Round(echo, 1), true, element.TooltipColor);

                if (flatAtk != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.flatAtk").Value}", (float)Math.Round(flatAtk, 1), true, element.TooltipColor);
            }
            else
            {
                if (hpBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Health").Value}", skill.HPPerLevel == 0
                    ? $"{Math.Abs(skill.HP)} * {modifier}"
                    : $"({Math.Abs(skill.HP)} + {Math.Abs(skill.HPPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", null, hpBonus > 0);

                if (atkBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}", skill.ATKPerLevel == 0
                    ? $"{Math.Abs(skill.ATK)} * {modifier}"
                    : $"({Math.Abs(skill.ATK)} + {Math.Abs(skill.ATKPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", element.TooltipColor, atkBonus > 0);

                if (defBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Def").Value}", skill.DEFPerLevel == 0
                    ? $"{Math.Abs(skill.DEF)} * {modifier}"
                    : $"({Math.Abs(skill.DEF)} + {Math.Abs(skill.DEFPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", null, defBonus > 0);

                if (critRateBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CR").Value}", skill.CritRatePerLevel == 0
                    ? $"{Math.Abs(skill.CritRate)} * {modifier}"
                    : $"({Math.Abs(skill.CritRate)} + {Math.Abs(skill.CritRatePerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", element.TooltipColor, critRateBonus > 0);

                if (critDamageBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CD").Value}", skill.CritDamagePerLevel == 0
                    ? $"{Math.Abs(skill.CritDamage)} * {modifier}"
                    : $"({Math.Abs(skill.CritDamage)} + {Math.Abs(skill.CritDamagePerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", element.TooltipColor, critDamageBonus > 0);

                if (attackSpeedBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.AS").Value}", skill.AttackSpeedPerLevel == 0
                    ? $"{Math.Abs(skill.AttackSpeed)} * {modifier}"
                    : $"({Math.Abs(skill.AttackSpeed)} + {Math.Abs(skill.AttackSpeedPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", element.TooltipColor, attackSpeedBonus > 0);

                if (movementSpeedBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.MS").Value}", skill.MovementSpeedPerLevel == 0
                    ? $"{Math.Abs(skill.MovementSpeed)} * {modifier}"
                    : $"({Math.Abs(skill.MovementSpeed)} + {Math.Abs(skill.MovementSpeedPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}", null, movementSpeedBonus > 0);

                if (chargeBarBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBGain").Value}",
                    skill.ChargeBarGainPerLevel == 0
                    ? $"{skill.ChargeBarGain} * {modifier}"
                    : $"({skill.ChargeBarGain} + {skill.ChargeBarGainPerLevel} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}",
                    element.TooltipColor);

                if (chargeAttackBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.CBDamage").Value}",
                    skill.ChargAttackPerLevel == 0
                    ? $"{skill.ChargAttack} * {modifier}"
                    : $"({skill.ChargAttack} + {skill.ChargAttackPerLevel} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}",
                    element.TooltipColor);

                if (allatackBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.AllEleAtk").Value}",
                    skill.ATKALLELEPerLevel == 0
                    ? $"{Math.Abs(skill.ATKALLELE)}"
                    : $"({Math.Abs(skill.ATKALLELE)} + {Math.Abs(skill.ATKALLELEPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value})",
                    element.TooltipColor, allatackBonus > 0);

                if (enmityBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}",
                    skill.EnmityPerLevel == 0
                    ? $"{Math.Abs(skill.Enmity)} * {modifier} * {Language.GetText("Mods.NeavaAGBF.WeaponSkill.EnmityDesc").Value}"
                    : $"({Math.Abs(skill.Enmity)} + {Math.Abs(skill.EnmityPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier} * {Language.GetText("Mods.NeavaAGBF.WeaponSkill.EnmityDesc").Value}",
                    element.TooltipColor, enmityBonus > 0);

                if (staminaBonus != 0) AddStatString(bonuses, $"{element.Name} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.Attack").Value}",
                    skill.StaminaPerLevel == 0
                    ? $"{Math.Abs(skill.Stamina)} * {modifier} * {Language.GetText("Mods.NeavaAGBF.WeaponSkill.StaminaDesc").Value}"
                    : $"({Math.Abs(skill.Stamina)} + {Math.Abs(skill.StaminaPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier} * {Language.GetText("Mods.NeavaAGBF.WeaponSkill.StaminaDesc").Value}",
                    element.TooltipColor, staminaBonus > 0);

                if (damageReductionBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.DR").Value}",
                    skill.DMGReducPerLevel == 0
                    ? $"{Math.Abs(skill.DMGReduc)} * {modifier}"
                    : $"({Math.Abs(skill.DMGReduc)} + {Math.Abs(skill.DMGReducPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}",
                    element.TooltipColor, damageReductionBonus > 0);

                if (damageAmpBonus != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Damage").Value}",
                    $"{skill.DMGAmp}",
                    element.TooltipColor, damageAmpBonus > 0);

                if (damageAmpBonusU != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Damage").Value}",
                    $"{skill.DMGAmpU}",
                    element.TooltipColor, damageAmpBonusU > 0);

                if (skill.SaveAmmo != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.AmmoE").Value}",
                    $"{skill.SaveAmmo}",
                    element.TooltipColor, skill.SaveAmmo > 0);

                if (echo != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.Echo").Value}",
                    skill.Echo == 0
                    ? $"{Math.Abs(skill.Echo)} * {modifier}"
                    : $"({Math.Abs(skill.Echo)} + {Math.Abs(skill.EchoPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}",
                    element.TooltipColor, echo > 0);

                if (flatAtk != 0) AddStatString(bonuses, $"{Language.GetText("Mods.NeavaAGBF.WeaponSkill.flatAtk").Value}",
                    skill.Echo == 0
                    ? $"{Math.Abs(skill.FlatAtk)} * {modifier}"
                    : $"({Math.Abs(skill.FlatAtk)} + {Math.Abs(skill.FlatAtkPerLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) * {modifier}",
                    element.TooltipColor, flatAtk > 0);

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
                string formattedValue = isPercentage ? $"{sign}{Math.Abs(value)}%" : $"{sign}{value}";
                string isBoon = value > 0 ? "boost" : "hit";
                bonuses.Add($"{formattedValue} {isBoon} to {statName}");
            }
        }

        void AddStatString(List<string> bonuses, string statName, string detailedValue, Color? color, bool isPos = false)
        {

            if (!string.IsNullOrEmpty(detailedValue))
            {
                string isBoon = isPos ? "boost" : "hit";

                bonuses.Add($"{detailedValue} {isBoon} to {statName}");
            }
        }

        private string GetStatString(float baseStat, float perLevel, string modifierText)
        {
            if (perLevel == 0)
            {
                return $"{Math.Abs(baseStat)} {modifierText}";
            }

            return $"({Math.Abs(baseStat)} + {Math.Abs(perLevel)} {Language.GetText("Mods.NeavaAGBF.WeaponSkill.PL").Value}) {modifierText}";
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
