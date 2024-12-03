using NeavaAGBF.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NeavaAGBF;
using NeavaAGBF.WeaponSkills;
using NeavaAGBF.WeaponSkills.Aegis;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NeavaAGBF.WeaponSkills.Might;
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
                new StormwyrmAegis(),
                new StormwyrmMight()
            };

                globalItem.currentLevel = 0;
                globalItem.maxLevel = 3;
                globalItem.weaponElement = Element.Wind;

				globalItem.chargeGain = 100;

                globalItem.chargeName = "Tester's Strike";
                globalItem.chargeAttackString = "Launches a projectile that doesn 100% damage to a foe. Gain 'Well Fed' buff ";

                globalItem.chargeAttack = (Player player) =>
                {
                    Main.NewText($"{player.name} used the Basic Sword Charge Attack!", Color.Yellow);

                    StatHandler playerMod = Main.LocalPlayer.GetModPlayer<StatHandler>();

                    Vector2 playerCenter = player.Center;
                    Vector2 cursorPosition = Main.MouseWorld;

                    Vector2 velocity = Vector2.Normalize(cursorPosition - playerCenter) * 10f;

                    int projectileType = ModContent.ProjectileType<ProjectilNoCharge>();
                    int proj = Projectile.NewProjectile(
                        spawnSource: player.GetSource_ItemUse(Item),
                        position: playerCenter,
                        velocity: velocity,
                        Type: projectileType,
                        Damage: (int)(Item.damage * playerMod.chargeAttackDamageMultiplier),
                        KnockBack: 0,
                        Owner: player.whoAmI
                    );

                    if (Main.projectile[proj].TryGetGlobalProjectile(out ChargeControlGlobalProjectile globalProj))
                    {
                        globalProj.CanGainCharge = false;
                    }


                    player.AddBuff(BuffID.WellFed, 600);
                };
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

	}
}
