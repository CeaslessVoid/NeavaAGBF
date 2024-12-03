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

                globalItem.chargeAttack = (Player player) =>
                {
                    Main.NewText($"{player.name} used the Basic Sword Charge Attack!", Color.Yellow);
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
