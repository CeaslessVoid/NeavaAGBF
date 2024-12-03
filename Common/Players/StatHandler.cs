﻿using Terraria;
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

namespace NeavaAGBF.Common.Players
{
    public class StatHandler : ModPlayer
    {
        public float BonusCritDamage = 0f;

        public int currentCharge = 0;
        public const int MaxCharge = 100;
        public bool readyToChargeAttack => currentCharge >= MaxCharge;

        public float chargeGainMultiplier = 1f;
        public float chargeAttackDamageMultiplier = 1f;



        public override void ResetEffects()
        {
            BonusCritDamage = 0f;

            chargeGainMultiplier = 1f;
            chargeAttackDamageMultiplier = 1f;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage += BonusCritDamage;
        }


        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NeavaAGBF.ChargeAttackKey.JustPressed && readyToChargeAttack)
            {
                if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
                {
                    globalItem.chargeAttack.Invoke(Player);

                    currentCharge = 0;
                    Main.NewText("Charge attack executed! Charge bar reset.", Color.Cyan);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HeldItem.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                float chargeGain = globalItem.chargeGain;
                float totalChargeGain = chargeGain * this.chargeGainMultiplier;

                currentCharge = Math.Min(currentCharge + (int)totalChargeGain, MaxCharge);

                Main.NewText($"Charge Gained! Current Charge: {currentCharge}/{MaxCharge}", Color.Cyan);
            }
        }


    }

}