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

        public float DamageCapBase = 10;
        public float DamageCapMulti = 1f;

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
            BonusCritDamage = 0f;

            chargeGainMultiplier = 1f;
            chargeAttackDamageMultiplier = 1f;

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

            DamageCapMulti = 1f;

        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage += BonusCritDamage;
        }


        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            //if (NeavaAGBF.ChargeAttackKey.JustPressed && readyToChargeAttack)
            //{
            //    if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem) && globalItem.chargeAttack != null)
            //    {
            //        globalItem.chargeAttack.Invoke(Player);

            //        currentCharge = 0;
            //    }
            //}
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithItem(item,target, hit, damageDone);

            //if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            //{
            //    float chargeGain = globalItem.chargeGain;
            //    float totalChargeGain = chargeGain * this.chargeGainMultiplier;

            //    currentCharge = Math.Min(currentCharge + totalChargeGain, MaxCharge);

            //    //Main.NewText($"Charge Gained! Current Charge: {currentCharge}/{MaxCharge}", Color.Cyan);
            //}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj,target,hit, damageDone);

            //if (proj.TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj) && !globalProj.CanGainCharge)
            //    return;

            //if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            //{
            //    float chargeGain = globalItem.chargeGain;
            //    float totalChargeGain = chargeGain * this.chargeGainMultiplier;

            //    if (proj.DamageType == DamageClass.Summon)
            //        totalChargeGain = 0.1f;

            //    currentCharge = Math.Min(currentCharge + totalChargeGain, MaxCharge);

            //    //Main.NewText($"Charge Gained! Current Charge: {(int)currentCharge}/{MaxCharge}", Color.Cyan);
            //}
        }

        

    }

}