using NeavaAGBF.WeaponSkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using NeavaAGBF.Content.Items;
using NeavaAGBF.Common.Items;
using log4net.Core;

namespace NeavaAGBF.Common.Players
{
    public class WeaponGridHandler
    {
        //private Item[] WeaponGrid = new Item[9];

        public void ApplyWeaponGridEffects(Player player)
        {
            var playerMod = Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>();
            var modPlayer = player.GetModPlayer<StatHandler>();

            var totals = new StatTotals();

            Item heldItem = player.HeldItem;
            Element heldElement = heldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem heldWeapon)
                ? heldWeapon.weaponElement
                : null;

            foreach (var weaponItem in playerMod.WeaponGrid)
            {
                if (weaponItem == null || !weaponItem.active || !weaponItem.TryGetGlobalItem(out WeaponSkillsGlobalItem weaponData))
                    continue;

                foreach (var skill in weaponData.weaponSkills)
                {
                    float multiplier = modPlayer.GetStatMultiplier(skill.SkillOwner);
                    float currentLevel = weaponData.currentLevel;

                    // General stats
                    totals.HpBonusPercent += (skill.HP + (skill.HPPerLevel * currentLevel)) * multiplier;
                    totals.DefBonus += (skill.DEF + (skill.DEFPerLevel * currentLevel)) * multiplier;
                    totals.DamageReduction += (skill.DMGReduc + (skill.DMGReducPerLevel * currentLevel)) / 100f;

                    // Element-matching stats
                    if (heldElement != null && weaponData.weaponElement == heldElement)
                    {
                        AddMatchingElementStats(skill, multiplier, currentLevel, totals, modPlayer);
                    }

                    // Universal stats
                    totals.ChargeBarGain += ((skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * currentLevel)) * multiplier) / 100f;
                    totals.AtkPercent += ((skill.ATKALLELE + (skill.ATKALLELEPerLevel * currentLevel)) / 100f);
                    totals.AmmoEfficiency += skill.SaveAmmo / 100f;
                    totals.DamageAmp += skill.DMGAmpU / 100f;
                }
            }

            ApplyBonusesToPlayer(player, totals, modPlayer);    

        }


        private static void AddMatchingElementStats(WeaponSkill skill, float multiplier, float currentLevel, StatTotals totals, StatHandler modPlayer)
        {
            totals.AtkPercent += ((skill.ATK + (skill.ATKPerLevel * currentLevel)) * multiplier) / 100f;
            totals.CritRatePercent += (skill.CritRate + (skill.CritRatePerLevel * currentLevel)) * multiplier;
            totals.CritDamagePercent += ((skill.CritDamage + (skill.CritDamagePerLevel * currentLevel)) * multiplier) / 100f;
            totals.AttackSpeedPercent += ((skill.AttackSpeed + (skill.AttackSpeedPerLevel * currentLevel)) * multiplier) / 100f;
            totals.ChargeDamageGain += ((skill.ChargAttack + (skill.ChargAttackPerLevel * currentLevel)) * multiplier) / 100f;
            totals.FlatAtk += (skill.FlatAtk + (skill.FlatAtkPerLevel * currentLevel)) * multiplier;
            totals.DamageAmp += skill.DMGAmp / 100f;
            totals.Echo += ((skill.Echo + (skill.EchoPerLevel * currentLevel)) * multiplier) / 100f;

            // Special stats
            modPlayer.enmityMod += ((skill.Enmity + (skill.Enmity * currentLevel)) * multiplier) / 100f;
            modPlayer.staminaMod += ((skill.Stamina + (skill.StaminaPerLevel * currentLevel)) * multiplier) / 100f;
        }

        private static void ApplyBonusesToPlayer(Player player, StatTotals totals, StatHandler modPlayer)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * (totals.HpBonusPercent / 100f));
            player.statDefense += (int)totals.DefBonus;

            totals.AtkPercent += modPlayer.CalculateEnmityAtkPercent() + modPlayer.CalculateStaminaAtkPercent();

            player.GetAttackSpeed(DamageClass.Generic) += totals.AttackSpeedPercent;
            player.GetCritChance(DamageClass.Generic) += totals.CritRatePercent;
            player.GetDamage(DamageClass.Generic) += totals.AtkPercent;

            player.GetDamage(DamageClass.Generic).Flat += (int)totals.FlatAtk;
            player.endurance += totals.DamageReduction;

            // Custom stats
            modPlayer.BonusCritDamage = 1 + totals.CritDamagePercent;
            modPlayer.chargeGainMultiplier = 1 + totals.ChargeBarGain;
            modPlayer.chargeAttackDamageMultiplier = 1 + totals.ChargeDamageGain;
            modPlayer.damageAmp = totals.DamageAmp + 1f;
            modPlayer.ammoFree = totals.AmmoEfficiency;
            modPlayer.echo = totals.Echo;
        }

        private class StatTotals
        {
            public float HpBonusPercent = 0f;
            public float DefBonus = 0f;
            public float AtkPercent = 0f;
            public float CritRatePercent = 0f;
            public float CritDamagePercent = 0f;
            public float AttackSpeedPercent = 0f;
            public float ChargeBarGain = 0f;
            public float ChargeDamageGain = 0f;
            public float DamageReduction = 0f;
            public float DamageAmp = 0f;
            public float AmmoEfficiency = 0f;
            public float Echo = 0f;
            public float FlatAtk = 0f;
        }
    }


}
