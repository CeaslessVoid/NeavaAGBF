using NeavaAGBF.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NeavaAGBF;
using NeavaAGBF.WeaponSkills;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NeavaAGBF.WeaponSkills.Wind;
using NeavaAGBF.Common.Items;
using NeavaAGBF.Common.Projectiles;

namespace NeavaAGBF.Content.Items
{ 
	public class BasicSword : ModItem
	{
        public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

            if (Item.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                globalItem.weaponSkills = new List<WeaponSkill>
                {
                    //new StormwyrmAegis(),
                    //new StormwyrmMight()
                };

                globalItem.maxLevel = 3;
                globalItem.weaponElement = Element.Wind;

				globalItem.chargeGain = 100;


                globalItem.chargeName = "Tester's Strike";
                globalItem.chargeAttackString = "Launches a projectile that does 100% damage to a foe. Gain 'Well Fed' buff ";

                globalItem.chargeAttack = (Player player, float multi) => ChargeAttacks.Test(player, Item, multi);

                globalItem.maxUncap = 5;
                globalItem.currentUncap = 0;

                globalItem.WeaponType = WeaponType.Sword;
            }
        }

	}
}
