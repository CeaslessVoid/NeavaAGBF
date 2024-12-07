using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using NeavaAGBF.Common.Players;
using Microsoft.Xna.Framework.Input;
using NeavaAGBF.Common.UI;
using NeavaAGBF.Common.Items;
using System.Linq;
using Terraria.ID;
using Terraria.GameContent;

namespace NeavaAGBF
{
    public class NeavaAGBF : Mod
    {
        public static ModKeybind ChargeAttackKey;

        public override void Load()
        {
            base.Load();
            On_Main.DrawEmoteBubblesButton += new On_Main.hook_DrawEmoteBubblesButton(this.On_Main_DrawEmoteBubblesButton);

            LoadUncapGroups.Initialize();

            ChargeAttackKey = KeybindLoader.RegisterKeybind(this, "Charge Attack", Keys.O);

            if (!Main.dedServ) // Only load UI on the client
            {
                ChargeBar.Instance = new ChargeBar();
                ChargeBar.Visible = false;
            }

        }

        public override void Unload()
        {
            ChargeBar.Instance = null;
            ChargeAttackKey = null;
            LoadUncapGroups.Clear();
        }

        private void On_Main_DrawEmoteBubblesButton(On_Main.orig_DrawEmoteBubblesButton orig, int pivotTopLeftX, int pivotTopLeftY)
        {
            NeavaAGBFPlayer.DrawWeaponGrid();
            NeavaAGBFPlayer.UpdateIsClicking();

            orig.Invoke(pivotTopLeftX, pivotTopLeftY);
        }

    }

}