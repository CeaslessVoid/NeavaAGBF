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
using NeavaAGBF.WeaponSkills.None;
using Terraria.GameContent;

namespace NeavaAGBF.Common.Players
{
    public class WeaponGridHandler
    {
        //private Item[] WeaponGrid = new Item[9];
        private readonly Dictionary<string, Action<StatHandler, StatTotals, int>> specialKeyEffects = new();

        public WeaponGridHandler()
        {
            specialKeyEffects["HpPerLight"] = NoneHpPerLight;
            specialKeyEffects["HamBatPassive"] = HamBatPassive;
            specialKeyEffects["ChosenBlade2"] = ChosenBlade;
        }

        public void ApplyWeaponGridEffects(Player player)
        {
            var playerMod = Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>();
            var modPlayer = player.GetModPlayer<StatHandler>();

            var totals = new StatTotals();
            var specialKeys = new List<string>();

            Item heldItem = player.HeldItem;
            Element heldElement = heldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem heldWeapon)
                ? heldWeapon.weaponElement
                : null;

            foreach (var weaponItem in playerMod.WeaponGrid)
            {
                if (weaponItem == null || !weaponItem.active || !weaponItem.TryGetGlobalItem(out WeaponSkillsGlobalItem weaponData))
                    continue;

                string elementName = weaponData.weaponElement.RealName;

                if (!modPlayer.GridCounts.ContainsKey(weaponData.weaponElement.RealName))
                {
                    modPlayer.GridCounts[weaponData.weaponElement.RealName] = 0;
                }
                modPlayer.GridCounts[weaponData.weaponElement.RealName]++;

                if (!modPlayer.GridCounts.ContainsKey(weaponData.WeaponType.RealName))
                {
                    modPlayer.GridCounts[weaponData.WeaponType.RealName] = 0;
                }
                modPlayer.GridCounts[weaponData.WeaponType.RealName]++;


                foreach (var skill in weaponData.weaponSkills)
                {
                    if (weaponData.currentUncap < skill.UncapLevel)
                        continue;

                    float multiplier = modPlayer.GetStatMultiplier(skill.SkillOwner);
                    float currentLevel = weaponData.currentLevel;

                    // General stats
                    totals.HpBonusPercent += (skill.HP + (skill.HPPerLevel * currentLevel)) * multiplier;
                    totals.DefBonus += (skill.DEF + (skill.DEFPerLevel * currentLevel)) * multiplier;
                    totals.DamageReduction += (skill.DMGReduc + (skill.DMGReducPerLevel * currentLevel)) / 100f;

                    // Element-matching stats
                    if (heldElement != null &&( weaponData.weaponElement == heldElement || heldElement == Element.Special ))
                    {
                        AddMatchingElementStats(skill, multiplier, currentLevel, totals, modPlayer);
                    }

                    // Universal stats
                    totals.ChargeBarGain += ((skill.ChargeBarGain + (skill.ChargeBarGainPerLevel * currentLevel)) * multiplier) / 100f;
                    totals.AtkPercent += ((skill.ATKALLELE + (skill.ATKALLELEPerLevel * currentLevel)) / 100f);
                    totals.AmmoEfficiency += skill.SaveAmmo / 100f;
                    totals.DamageAmp += skill.DMGAmpU / 100f;

                    if (!string.IsNullOrEmpty(skill.SpecialKey))
                    {
                        specialKeys.Add(skill.SpecialKey);
                    }
                }
            }

            ProcessSpecialKeys(modPlayer, totals, specialKeys);

            ApplyBonusesToPlayer(player, totals, modPlayer);    

        }

        private void ProcessSpecialKeys(StatHandler modPlayer, StatTotals totals, List<string> specialKeys)
        {
            var groupedKeys = specialKeys.GroupBy(key => key);

            foreach (var group in groupedKeys)
            {
                string key = group.Key;
                int stackSize = group.Count();

                if (specialKeyEffects.TryGetValue(key, out var effectFunction))
                {
                    effectFunction(modPlayer, totals, stackSize);
                }
            }
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



        // Special Weapon Skills

        private static void NoneHpPerLight(StatHandler modPlayer, StatTotals totals, int stack)
        {
            if (modPlayer.GridCounts.TryGetValue("Light", out int lightCount))
            {
                totals.HpBonusPercent += Math.Min(5 * lightCount * stack, 50);
            }
        }

        private static void HamBatPassive(StatHandler modPlayer, StatTotals totals, int stack)
        {
            if (modPlayer.Player.HasBuff(207))
            {
                totals.AtkPercent += 0.15f;
                totals.DefBonus += 5;
            }
            else if (modPlayer.Player.HasBuff(206))
            {
                totals.AtkPercent += 0.10f;
                totals.DefBonus += 3;
            }
            else if(modPlayer.Player.HasBuff(26))
            {
                totals.AtkPercent += 0.05f;
                totals.DefBonus += 1;
            }
        }

        private static void ChosenBlade(StatHandler modPlayer, StatTotals totals, int stack)
        {
            if (modPlayer.GridCounts.TryGetValue("Sword", out int swordCount))
            {
                totals.AtkPercent += (Math.Min(2 * swordCount * stack, 20)) / 100f;
            }
        }
    }

}
