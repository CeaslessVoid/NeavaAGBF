using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace NeavaAGBF.Common.Player
{
    public class NeavaAGBFPlayer : ModPlayer
    {
        public Item[] WeaponGrid = new Item[9];

        public static bool _isMouseOverSlot = false;
        private static bool _isWeaponGridInitialized = false;

        public static bool IsWeaponGridOpen = false;
        public static bool IsClicking = false;


    }
}
