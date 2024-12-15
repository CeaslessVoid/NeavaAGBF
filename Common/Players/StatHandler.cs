using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using NeavaAGBF.Content.Items;
using Terraria.UI.Chat;
using ReLogic.Graphics;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.ID;
using System.Security.Policy;
using Terraria.ModLoader.IO;
using System;
using NeavaAGBF.WeaponSkills;
using NeavaAGBF.Common.Items;
using System.Linq;
using Microsoft.CodeAnalysis.Emit;

namespace NeavaAGBF.Common.Players
{
    public class StatHandler : ModPlayer
    {
        // Bonus stats and multipliers
        public float BonusCritDamage = 0f;
        public float enmityMod = 0.0f;
        public float staminaMod = 0.0f;
        public float damageAmp = 1.0f;
        public float ammoFree = 0.0f;
        public float echo = 0.0f;

        public int chargeAttackSuppliment = 0;
        public int attackSuppliment = 0;

        // Charge attack variables
        public float currentCharge = 0f;
        public int MaxCharge = 100;
        public bool readyToChargeAttack => currentCharge >= MaxCharge;

        public float chargeGainMultiplier = 1f;
        public float chargeAttackDamageMultiplier = 1f;

        // Grid and Buff States
        public Dictionary<string, int> GridCounts = new Dictionary<string, int>();
        public bool hasGungnir = false;
        public bool hasToxicosis = false;

        // Stat Multpliers

        //public float StatMultiplierWindOmega = 1f;
        //public float StatMultiplierFireOmega = 1f;
        //public float StatMultiplierWaterOmega = 1f;
        //public float StatMultiplierEarthOmega = 1f;
        //public float StatMultiplierLightOmega = 1f;
        //public float StatMultiplierDarkOmega = 1f;

        public float StatMultiplierWindNormal = 1f;
        public float StatMultiplierFireNormal = 1f;
        public float StatMultiplierWaterNormal = 1f;
        public float StatMultiplierEarthNormal = 1f;
        public float StatMultiplierLightNormal = 1f;
        public float StatMultiplierDarkNormal = 1f;

        //public float DamageCapBase = 100;
        //public float DamageCapMulti = 1f;

        public float GetStatMultiplier(string owner) => owner switch
        {
            "Wind" or "Whirlwind" or "Ventosus" => StatMultiplierWindNormal,
            "Fire" or "Hellfire" or "Inferno" => StatMultiplierFireNormal,
            "Water" or "Tsunami" or "Hoarfrost" => StatMultiplierWaterNormal,
            "Earth" or "Mountain" or "Terra" => StatMultiplierEarthNormal,
            "Light" or "Thunder" or "Zion" => StatMultiplierLightNormal,
            "Dark" or "Hatred" or "Oblivion" => StatMultiplierDarkNormal,
            _ => 1f,
        };

        public override void ResetEffects()
        {
            ammoFree = enmityMod = staminaMod = 0f;
            chargeGainMultiplier = 1f;

            //chargeAttackDamageMultiplier = 1f;
            //chargeAttackSuppliment = 0;

            hasGungnir = false;
            hasToxicosis = false;

            //StatMultiplierWindOmega = 1f;
            //StatMultiplierFireOmega = 1f;
            //StatMultiplierWaterOmega = 1f;
            //StatMultiplierEarthOmega = 1f;
            //StatMultiplierLightOmega = 1f;
            //StatMultiplierDarkOmega = 1f;

            StatMultiplierWindNormal = StatMultiplierFireNormal =
            StatMultiplierWaterNormal = StatMultiplierEarthNormal =
            StatMultiplierLightNormal = StatMultiplierDarkNormal = 1f;

            GridCounts.Clear();

        }

        public float CalculateEnmityAtkPercent() =>
            EnmityBonus() * enmityMod;

        private float EnmityBonus()
        {
            float hp = (float)Player.statLife / Player.statLifeMax2;
            return hp > 0.51f ? 0f : Utils.Clamp((0.51f - hp) / 0.41f, 0f, 1f);
        }

        public float CalculateStaminaAtkPercent() =>
            StaminaBonus() * staminaMod;

        private float StaminaBonus()
        {
            float hp = (float)Player.statLife / Player.statLifeMax2;
            return hp < 0.49f ? 0f : Utils.Clamp((hp - 0.49f) / 0.41f, 0f, 1f);
        }


        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= damageAmp;

        }

        // Split for ougi
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= damageAmp;

            //Suppli
            if (attackSuppliment > 0)
                modifiers.FinalDamage += CalcBonusDamage(target, 0.01f, attackSuppliment, 300, 1000);


            // CA Suppli
            if (chargeAttackSuppliment > 0 &&
                proj.TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj) &&
                globalProj.IsChargeAttack)
            {
                modifiers.FinalDamage += CalcBonusDamage(target, 0.05f, chargeAttackSuppliment, 10000, 100000);
            }
        }

        private float CalcBonusDamage(NPC target, float lifeFactor, int suppliment, float baseCap, float maxCap)
        {
            float cap = Math.Min(target.lifeMax * lifeFactor, baseCap);
            return Math.Min(cap * suppliment, maxCap) / 100f;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo) =>
            Main.rand.NextFloat() > ammoFree;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NeavaAGBF.ChargeAttackKey.JustPressed && readyToChargeAttack &&
                Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem) &&
                globalItem.chargeAttack != null)
            {
                globalItem.chargeAttack.Invoke(Player, chargeAttackDamageMultiplier);
                currentCharge = 0;
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) =>
            GainCharge(item);

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj) && !globalProj.CanGainCharge)
                return;

            GainCharge(Player.HeldItem, proj.DamageType == DamageClass.Summon ? 0.01f : 1f);
        }

        private void GainCharge(Item item, float baseGain = 1f)
        {
            if (item.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                currentCharge = Math.Min(currentCharge + (globalItem.chargeGain * chargeGainMultiplier * baseGain), MaxCharge);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (echo > 0)
                ApplyEchoDamage(target, hit);

            if (hasGungnir && target.defense > 30)
                target.defense--;

            if (hasToxicosis)
                ApplyToxicosis(target, hit);
        }

        private void ApplyEchoDamage(NPC target, NPC.HitInfo hit)
        {
            NPC.HitInfo echoHit = hit;
            echoHit.DamageType = DamageClass.Generic;
            echoHit.Knockback = 0f;
            echoHit.Damage = (int)(hit.Damage * echo * damageAmp);
            echoHit.Crit = false;

            target.StrikeNPC(echoHit);
        }

        private void ApplyToxicosis(NPC target, NPC.HitInfo hit)
        {
            target.AddBuff(BuffID.Poisoned, 300);

            if (target.HasBuff(BuffID.Poisoned))
            {
                NPC.HitInfo toxicosisHit = hit;
                toxicosisHit.DamageType = DamageClass.Generic;
                toxicosisHit.Knockback = 0f;
                toxicosisHit.Damage = (int)(toxicosisHit.Damage * 0.1f * damageAmp);
                toxicosisHit.Crit = false;

                target.StrikeNPC(toxicosisHit);
            }
        }

    }
}