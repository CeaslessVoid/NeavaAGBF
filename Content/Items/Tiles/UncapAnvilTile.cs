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
using Terraria.Localization;
using Terraria.ObjectData;
using NeavaAGBF.Common.UI;
using NeavaAGBF.Common.Players;

namespace NeavaAGBF.Content.Items.Tiles
{
    public class UncapAnvil_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileTable[(int)base.Type] = true;
            Main.tileSolidTop[(int)base.Type] = true;
            Main.tileNoAttach[(int)base.Type] = true;
            Main.tileLavaDeath[(int)base.Type] = true;
            Main.tileFrameImportant[(int)base.Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[(int)base.Type] = true;
            base.DustType = 309;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                18
            };
            TileObjectData.addTile((int)base.Type);
            base.AddMapEntry(new Color(0, 0, 200), Language.GetText("ItemName.WorkBench"));
        }

        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = (fail ? 1 : 3);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            base.KillMultiTile(i, j, frameX, frameY);

            ModContent.GetInstance<AnvilUISystem>().HideMyUI();
        }

        public unsafe override bool RightClick(int i, int j)
        {
            ModContent.GetInstance<AnvilUISystem>().ShowMyUI();
            Main.LocalPlayer.GetModPlayer<NeavaAGBFPlayer>().ForgeLocation = new Point?(new Point(i - (int)(Main.tile[i, j].TileFrameX / 16), j));
            return true;
        }
    }
}
