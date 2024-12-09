using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using System.Collections.ObjectModel;
using Terraria.Localization;

namespace NeavaAGBF.Content.Items.Tiles
{
    public class UncapAnvil : ModItem
    {
        public override void SetDefaults()
        {
            base.Item.useStyle = 1;
            base.Item.useTurn = true;
            base.Item.useAnimation = 15;
            base.Item.useTime = 10;
            base.Item.autoReuse = true;
            base.Item.maxStack = 1;
            base.Item.consumable = true;
            base.Item.createTile = ModContent.TileType<UncapAnvilTile>();
            base.Item.width = 28;
            base.Item.height = 14;
            base.Item.value = 7600;
            base.Item.rare = 2;
            base.SetDefaults();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadAnvil)
                .Register();

        }
    }
}
