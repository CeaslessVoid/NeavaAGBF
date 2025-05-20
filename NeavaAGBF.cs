using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace NeavaAGBF
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class NeavaAGBF : Mod
    {
        public override void Load()
        {
            On_Main.DrawEmoteBubblesButton += new On_Main.hook_DrawEmoteBubblesButton(this.On_Main_DrawEmoteBubblesButton);
        }

        // I don't acutally know how to make a fucking GUI
        private void On_Main_DrawEmoteBubblesButton(On_Main.orig_DrawEmoteBubblesButton orig, int pivotTopLeftX, int pivotTopLeftY)
        {
            //NeavaAGBFPlayer.DrawWeaponGrid();
            //NeavaAGBFPlayer.UpdateIsClicking();

            orig.Invoke(pivotTopLeftX, pivotTopLeftY);
        }
    }

}
