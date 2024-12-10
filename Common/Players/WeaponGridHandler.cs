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
            NeavaAGBFPlayer playerMod = Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>();
            var modPlayer = player.GetModPlayer<StatHandler>();

            float totalHpBonusPercent = 0f;
            float totalDefBonus = 0f;

            float totalAtkPercent = 0f;
            float totalCritRatePercent = 0f;
            float totalCritDamagePercent = 0f;
            float totalAttackSpeedPercent = 0f;

            float totalChargeBarGain = 0f;
            float totalChargeDamageGain = 0f;

            float totalDamageReduction = 0f;

            float totalDamageCap = 0f;

            float totalAmp = 0f;

            float totalAmmoEf = 0f;

            float totalEcho = 0f;

            float totalFlatAtk = 0f;

            Item heldItem = player.HeldItem;
            Element heldElement = null;


            if (heldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem heldWeapon))
            {
                heldElement = heldWeapon.weaponElement;
            }

            foreach (var weaponItem in playerMod.WeaponGrid)
            {
                if (weaponItem == null || !weaponItem.active || !weaponItem.TryGetGlobalItem(out WeaponSkillsGlobalItem weaponData))
                {
                    continue;
                }

                foreach (var skill in weaponData.weaponSkills)
                {
                    

                    float multiplier = modPlayer.GetStatMultiplier(skill.SkillOwner);

                    float currentLevel = weaponData.currentLevel;
                    totalHpBonusPercent += (skill.HP + (skill.HPPerLevel * currentLevel)) * multiplier;
                    totalDefBonus += (skill.DEF + (skill.DEFPerLevel * currentLevel)) * multiplier;

                    totalDamageReduction += (skill.DMGReduc + (skill.DMGReducPerLevel * currentLevel)) / 100f;

                    if (heldElement != null && weaponData.weaponElement == heldElement)
                    {
                        totalAtkPercent += ((skill.ATK + (skill.ATKPerLevel * currentLevel)) * multiplier) / 100f;
                        totalCritRatePercent += (skill.CritRate + (skill.CritRatePerLevel * currentLevel)) * multiplier;
                        totalCritDamagePercent += ((skill.CritDamage + (skill.CritDamagePerLevel * currentLevel)) * multiplier) / 100f;
                        totalAttackSpeedPercent += ((skill.AttackSpeed + (skill.AttackSpeedPerLevel * currentLevel)) * multiplier) / 100f;
                        
                        totalChargeDamageGain += ((skill.ChargAttack + (skill.ChargAttackPerLevel * currentLevel)) * multiplier) / 100f;

                        // Enmity
                        modPlayer.enmityMod += ((skill.Enmity + (skill.Enmity * currentLevel)) * multiplier) / 100f;

                        // Stamina
                        modPlayer.staminaMod += ((skill.Stamina + (skill.StaminaPerLevel * currentLevel)) * multiplier) / 100f;

                        totalAmp += skill.DMGAmp / 100f;

                        totalEcho += ((skill.Echo + (skill.EchoPerLevel * currentLevel)) * multiplier) / 100f;

                        totalFlatAtk += (skill.FlatAtk + (skill.FlatAtkPerLevel * currentLevel)) * multiplier;
                    }

                    totalChargeBarGain += ((skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * currentLevel)) * multiplier) / 100f;

                    totalAtkPercent += ((skill.ATKALLELE + (skill.ATKALLELEPerLevel * currentLevel)) / 100f);

                    totalDamageCap += ((skill.DamageCap + (skill.DamageCapPerLevel * currentLevel)) / 100f);

                    totalAmmoEf += skill.SaveAmmo / 100f;

                    // Amp unconditional
                    totalAmp += skill.DMGAmpU / 100f;

                }
            }

            ApplyBonusesToPlayer(player, totalHpBonusPercent, totalDefBonus, totalAtkPercent, totalCritRatePercent, totalCritDamagePercent, totalAttackSpeedPercent, totalChargeBarGain, totalChargeDamageGain, totalDamageCap, totalDamageReduction, totalAmp, totalAmmoEf, totalEcho, totalFlatAtk);

        }


        private static void ApplyBonusesToPlayer(Player player, float hpBonusPercent, float defBonus, float atkPercent, float critRatePercent, float critDamagePercent, float attackSpeedPercent, float totalChargeBarGain, float totalChargeDamageGain, float totalDamageCap, float totalDamageReduction, float totalAmp, float totalAmmoEf, float totalEcho, float totalFlatAtk)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * (hpBonusPercent / 100f));
            player.statDefense += (int)defBonus;

            var modPlayer = player.GetModPlayer<StatHandler>();

            atkPercent += modPlayer.CalculateEnmityAtkPercent() + modPlayer.CalculateStaminaAtkPercent();

            player.GetAttackSpeed(DamageClass.Generic) += attackSpeedPercent;
            player.GetCritChance(DamageClass.Generic) += critRatePercent;
            player.GetDamage(DamageClass.Generic) += atkPercent;

            player.GetDamage(DamageClass.Generic).Flat += (int)totalFlatAtk;

            player.endurance += totalDamageReduction;

            // Custom Stats go here
            modPlayer.BonusCritDamage = 1 + critDamagePercent;

            modPlayer.chargeGainMultiplier = 1 + totalChargeBarGain;
            modPlayer.chargeAttackDamageMultiplier = 1 + totalChargeDamageGain;

            modPlayer.damageAmp = totalAmp + 1f;

            modPlayer.ammoFree = totalAmmoEf;

            modPlayer.echo = totalEcho;
        }
    }


}
