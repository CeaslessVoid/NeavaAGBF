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

namespace NeavaAGBF.Common.Players
{
    public class WeaponGridHandler
    {
        //private Item[] WeaponGrid = new Item[9];

        public void ApplyWeaponGridEffects(Player player)
        {
            float totalHpBonusPercent = 0f;
            float totalDefBonus = 0f;

            float totalAtkPercent = 0f;
            float totalCritRatePercent = 0f;
            float totalCritDamagePercent = 0f;
            float totalAttackSpeedPercent = 0f;

            Item heldItem = player.HeldItem;
            Element heldElement = null;

            if (heldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem heldWeapon))
            {
                heldElement = heldWeapon.weaponElement;
            }

            foreach (var weaponItem in NeavaAGBFPlayer.WeaponGrid)
            {
                if (weaponItem == null || !weaponItem.active || !weaponItem.TryGetGlobalItem(out WeaponSkillsGlobalItem weaponData))
                {
                    continue;
                }

                foreach (var skill in weaponData.weaponSkills)
                {
                    float currentLevel = weaponData.currentLevel;
                    totalHpBonusPercent += skill.HP + (skill.HPPerLevel * currentLevel);
                    totalDefBonus += skill.DEF + (skill.DEFPerLevel * currentLevel);

                    if (heldElement != null && weaponData.weaponElement == heldElement)
                    {
                        totalAtkPercent += (skill.ATK + (skill.ATKPerLevel * currentLevel)) / 100f;
                        totalCritRatePercent += (skill.CritRate + (skill.CritRatePerLevel * currentLevel));
                        totalCritDamagePercent += (skill.CritDamage + (skill.CritDamagePerLevel * currentLevel)) / 100f;
                        totalAttackSpeedPercent += (skill.AttackSpeed + (skill.AttackSpeedPerLevel * currentLevel)) / 100f;
                    }
                }
            }

            ApplyBonusesToPlayer(player, totalHpBonusPercent, totalDefBonus, totalAtkPercent, totalCritRatePercent, totalCritDamagePercent, totalAttackSpeedPercent);

        }


        private static void ApplyBonusesToPlayer(Player player, float hpBonusPercent, float defBonus, float atkPercent, float critRatePercent, float critDamagePercent, float attackSpeedPercent)
        {
            player.statLifeMax2 += (int)(player.statLifeMax * (hpBonusPercent / 100f));
            player.statDefense += (int)defBonus;

            if (atkPercent > 0 || critRatePercent > 0 || critDamagePercent > 0 || attackSpeedPercent > 0)
            {
                var modPlayer = player.GetModPlayer<StatHandler>();

                player.GetAttackSpeed(DamageClass.Generic) += attackSpeedPercent;
                player.GetCritChance(DamageClass.Generic) += critRatePercent;
                player.GetDamage(DamageClass.Generic) += atkPercent;

                // Custom Stats go here
                modPlayer.BonusCritDamage += critDamagePercent;
            }
        }
    }


}
