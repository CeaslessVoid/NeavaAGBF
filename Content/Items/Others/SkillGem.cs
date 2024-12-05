using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using NeavaAGBF.Common.Items;

namespace NeavaAGBF.Content.Items.Others
{
    public class SkillGem : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 40;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            TooltipLine nameLine = tooltips.Find(line => line.Mod == "Terraria" && line.Name == "ItemName");
            if (nameLine != null)
            {
                float time = Main.GlobalTimeWrappedHourly * 0.25f;
                float pulse = (float)((Math.Sin(time) + 1f) / 2f);
                Color startColor = new Color(128, 0, 128);
                Color endColor = new Color(80, 0, 80);

                Color smoothColor = Color.Lerp(startColor, endColor, pulse);

                nameLine.OverrideColor = smoothColor;
            }
        }
    }
}
