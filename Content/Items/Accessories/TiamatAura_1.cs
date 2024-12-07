using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NeavaAGBF.Common.Players;

namespace NeavaAGBF.Content.Items.Accessories
{
    public class TiamatAura_1 : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 15);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatHandler>().StatMultiplierWindOmega += 0.6f;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //        .AddIngredient(ItemID.LeafWand)
        //        .AddIngredient(ItemID.AnkletoftheWind, 25)
        //        .AddIngredient(ItemID.SoulofFlight, 10)
        //        .AddIngredient(ItemID.Cloud, 5)
        //        .AddTile(TileID.SkyMill)
        //        .Register();
        //}
    }
}
