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
        public float BonusCritDamage = 0f;

        public float currentCharge = 0;
        public int MaxCharge = 100;
        public bool readyToChargeAttack => currentCharge >= MaxCharge;

        public float chargeGainMultiplier = 1f;
        public float chargeAttackDamageMultiplier = 1f;

        public float enmityMod = 0.0f;

        public float staminaMod = 0.0f;

        public float damageAmp = 1.0f;

        public float ammoFree = 0.0f;

        public float echo = 0.0f;

        // Stat Multpliers

        public float StatMultiplierWindOmega = 1f;
        public float StatMultiplierFireOmega = 1f;
        public float StatMultiplierWaterOmega = 1f;
        public float StatMultiplierEarthOmega = 1f;
        public float StatMultiplierLightOmega = 1f;
        public float StatMultiplierDarkOmega = 1f;

        

        public float StatMultiplierWindNormal = 1f;
        public float StatMultiplierFireNormal = 1f;
        public float StatMultiplierWaterNormal = 1f;
        public float StatMultiplierEarthNormal = 1f;
        public float StatMultiplierLightNormal = 1f;
        public float StatMultiplierDarkNormal = 1f;

        //public float DamageCapBase = 100;
        //public float DamageCapMulti = 1f;

        public float GetStatMultiplier(String owner)
        {

            if (owner == "Stormwyrm")
                return StatMultiplierWindOmega;
            else if (owner == "Ironflame")
                return StatMultiplierFireOmega;
            else if (owner == "Oceansoul")
                return StatMultiplierWaterOmega;
            else if (owner == "Lifetree")
                return StatMultiplierEarthOmega;
            else if (owner == "Knightcode")
                return StatMultiplierLightOmega;
            else if (owner == "Mistfall")
                return StatMultiplierDarkOmega;

            else if (owner == "Wind" | owner == "Whirlwind" | owner == "Ventosus")
                return StatMultiplierWindNormal;
            else if (owner == "Fire" | owner == "Hellfire" | owner == "Inferno")
                return StatMultiplierFireNormal;
            else if (owner == "Water" | owner == "Tsunami" | owner == "Hoarfrost")
                return StatMultiplierWaterNormal;
            else if (owner == "Earth" | owner == "Mountain" | owner == "Terra")
                return StatMultiplierEarthNormal;
            else if (owner == "Light" | owner == "Thunder" | owner == "Zion")
                return StatMultiplierLightNormal;
            else if (owner == "Dark" | owner == "Hatred" | owner == "Oblivion")
                return StatMultiplierDarkNormal;

            // Gacha

            return 1f;
        }

        public override void ResetEffects()
        {
            ammoFree = 0f;

            enmityMod = 0f;
            staminaMod = 0f;

            chargeGainMultiplier = 1f;
            //chargeAttackDamageMultiplier = 1f;

            StatMultiplierWindOmega = 1f;
            StatMultiplierFireOmega = 1f;
            StatMultiplierWaterOmega = 1f;
            StatMultiplierEarthOmega = 1f;
            StatMultiplierLightOmega = 1f;
            StatMultiplierDarkOmega = 1f;

            StatMultiplierWindNormal = 1f;
            StatMultiplierFireNormal = 1f;
            StatMultiplierWaterNormal = 1f;
            StatMultiplierEarthNormal = 1f;
            StatMultiplierLightNormal = 1f;
            StatMultiplierDarkNormal = 1f;

        }

        public static void ResetMultis()
        {
            //StatHandler player = Main.LocalPlayer.GetModPlayer<StatHandler>();

            //player.BonusCritDamage = 0f;

            //player.enmityMod = 0f;
            //player.staminaMod = 0f;

            //player.chargeGainMultiplier = 1f;
            //player.chargeAttackDamageMultiplier = 1f;

            //player.ammoFree = 0f;
        }

        public float CalculateEnmityAtkPercent()
        {

            float healthPercentage = (float)Player.statLife / Player.statLifeMax2;

            if (healthPercentage > 0.51f)
            {
                return 0f;
            }

            float scaledHealth = Utils.Clamp((0.51f - healthPercentage) / (0.51f - 0.10f), 0f, 1f);
            float atkBonus = scaledHealth * enmityMod;

            return atkBonus;
        }

        public float CalculateStaminaAtkPercent()
        {

            float healthPercentage = (float)Player.statLife / Player.statLifeMax2;

            if (healthPercentage < 0.49f)
            {
                return 0f;
            }

            float scaledHealth = Utils.Clamp((healthPercentage - 0.49f) / (0.9f - 0.49f), 0f, 1f);
            float atkBonus = scaledHealth * staminaMod;

            return atkBonus;
        }


        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= damageAmp;

            base.ModifyHitNPC(target, ref modifiers);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            return Main.rand.NextFloat() > ammoFree;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NeavaAGBF.ChargeAttackKey.JustPressed && readyToChargeAttack)
            {
                if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem) && globalItem.chargeAttack != null)
                {
                    globalItem.chargeAttack.Invoke(Player, chargeAttackDamageMultiplier);

                    currentCharge = 0;
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {

            if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                float chargeGain = globalItem.chargeGain;
                float totalChargeGain = chargeGain * this.chargeGainMultiplier;

                currentCharge = Math.Min(currentCharge + totalChargeGain, MaxCharge);

                //Main.NewText($"Charge Gained! Current Charge: {currentCharge}/{MaxCharge}", Color.Cyan);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {

            if (proj.TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj) && !globalProj.CanGainCharge)
                return;

            if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                float chargeGain = globalItem.chargeGain;
                float totalChargeGain = chargeGain;

                if (proj.DamageType == DamageClass.Summon)
                    totalChargeGain = 0.01f;

                totalChargeGain *= this.chargeGainMultiplier;

                currentCharge = Math.Min(currentCharge + totalChargeGain, MaxCharge);

                //Main.NewText($"Charge Gained! Current Charge: {(int)currentCharge}/{MaxCharge}", Color.Cyan);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (echo > 0)
            {
                hit.DamageType = DamageClass.Generic;
                hit.Crit = false;
                hit.Knockback = 0f;
                hit.Damage = (int)(hit.Damage * echo);

                target.StrikeNPC(hit);
            }
        }
    }
}