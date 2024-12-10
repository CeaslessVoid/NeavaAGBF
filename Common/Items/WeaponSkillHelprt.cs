using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeavaAGBF.WeaponSkills;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using NeavaAGBF.Content.Items;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI.Chat;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using NeavaAGBF.Content.Items.Others;
using Terraria.Audio;
using Terraria.ID;
using NeavaAGBF.Common.Players;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using NeavaAGBF.WeaponSkills.Wind;
using NeavaAGBF.WeaponSkills.Water;
using NeavaAGBF.WeaponSkills.Earth;
using NeavaAGBF.WeaponSkills.Fire;
using NeavaAGBF.WeaponSkills.Dark;

namespace NeavaAGBF.Common.Items
{
    // NOT BEING CONTINUED DUE TO INEFFECIENT AS FUCK
    internal class WeaponSkillHelprt
    {

        public static List<WeaponSkill> BasicMight(Item entity)
        {
            if (entity.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
            {
                if (globalItem.weaponElement == Element.Earth)
                    return new List<WeaponSkill>
                    {

                        new EarthMight(),
                    };

                else if (globalItem.weaponElement == Element.Water)
                        return new List<WeaponSkill>
                    {

                        new WaterMight(),
                    };

                else if (globalItem.weaponElement == Element.Fire)
                    return new List<WeaponSkill>
                    {

                        new FireMight(),
                    };

                else if (globalItem.weaponElement == Element.Wind)
                    return new List<WeaponSkill>
                    {

                        new WindMight(),
                    };

                else if (globalItem.weaponElement == Element.Light)
                    return new List<WeaponSkill>
                    {

                        new LightMight(),
                    };

                else if (globalItem.weaponElement == Element.Dark)
                    return new List<WeaponSkill>
                    {

                        new DarkMight(),
                    };

            }

            return new List<WeaponSkill>();
        }
    }



    // Basic charge







}
