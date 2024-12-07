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
    public class NormalRing_Wind : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatHandler>().StatMultiplierWindNormal += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AnkletoftheWind)
                .AddIngredient(ItemID.DiamondRing, 1)
                .AddIngredient(ItemID.RainCloud, 5)
                .AddIngredient(ItemID.Cloud, 15)
                .AddTile(TileID.SkyMill)
                .Register();
        }
    }

    public class NormalRing_Fire : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatHandler>().StatMultiplierFireNormal += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MagmaStone)
                .AddIngredient(ItemID.DiamondRing, 1)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddIngredient(ItemID.Obsidian, 15)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }

    public class NormalRing_Water : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatHandler>().StatMultiplierWaterNormal += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WaterBucket)
                .AddIngredient(ItemID.DiamondRing, 1)
                .AddIngredient(ItemID.SharkFin, 5)
                .AddIngredient(ItemID.Coral, 15)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }

    public class NormalRing_Earth : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatHandler>().StatMultiplierEarthNormal += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.NaturesGift)
                .AddIngredient(ItemID.DiamondRing, 1)
                .AddIngredient(ItemID.Stinger, 5)
                .AddIngredient(ItemID.JungleSpores, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
