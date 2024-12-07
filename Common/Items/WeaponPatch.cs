using NeavaAGBF.WeaponSkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using NeavaAGBF.WeaponSkills.Fire;
using NeavaAGBF.WeaponSkills.Water;
using NeavaAGBF.WeaponSkills.Earth;
using NeavaAGBF.WeaponSkills.None;
using NeavaAGBF.WeaponSkills.Dark;

namespace NeavaAGBF.Common.Items
{
    // Prehardmode weapons do not focus too much for now. We can give them generic skills


    // Fire -> Hight crit and crit damage.
    // Water -> Generic high attkck speed and damage
    // Earth -> Tanky
    // Wind -> All rounder. Wants high health

    // Light and Dark are throwaways for overpowered weapons / some modded content

    // Light -> Want high health. Healing?
    // Dark -> High damage, REALLY WANTS LOW HEALTH

    // Wooden Weapons (All have might) + some Ore Weapons

    public class WeaponPatchGlobalItem : GlobalItem
    {

        private static readonly Dictionary<int, Element> weaponElementMapping = new Dictionary<int, Element>
        {
            { ItemID.WoodenSword, Element.Earth },
            { ItemID.WoodYoyo, Element.Wind },
            { ItemID.RichMahoganySword, Element.Earth },
            { ItemID.CopperShortsword, Element.Earth },
            { ItemID.TinShortsword, Element.Earth },
            { ItemID.CopperBroadsword, Element.Earth },
            { ItemID.TinBroadsword, Element.Earth },
            { ItemID.BorealWoodSword, Element.Water },
            { ItemID.PalmWoodSword, Element.Water },
            { ItemID.PalmWoodBow, Element.Water },
            { ItemID.BladedGlove, Element.Water },
            { ItemID.AshWoodSword, Element.Fire },
            { ItemID.AshWoodBow, Element.Fire },
            { ItemID.CopperBow, Element.Fire },
            { ItemID.TinBow, Element.Fire },
            { ItemID.WandofSparking, Element.Fire },
            { ItemID.WoodenBoomerang, Element.Wind },
            { ItemID.WoodenBow, Element.Wind },
            { ItemID.RichMahoganyBow, Element.Wind },
            { ItemID.ShadewoodSword, Element.Dark },
            { ItemID.EbonwoodSword, Element.Dark },
            { ItemID.ShadewoodBow, Element.Dark },
            { ItemID.EbonwoodBow, Element.Dark },
            { ItemID.PearlwoodSword, Element.Light },
            { ItemID.PearlwoodBow, Element.Light },
            


            { ItemID.CactusSword, Element.Wind },
            { ItemID.Spear, Element.Fire },
            { ItemID.ChainKnife, Element.Fire },
            { ItemID.LeadShortsword, Element.Water },
            { ItemID.LeadBroadsword, Element.Water },
            { ItemID.IronShortsword, Element.Water },
            { ItemID.IronBroadsword, Element.Water },
            { ItemID.SilverShortsword, Element.Fire },
            { ItemID.SilverBroadsword, Element.Fire },
            { ItemID.TungstenShortsword, Element.Fire },
            { ItemID.TungstenBroadsword, Element.Fire },
            { ItemID.GoldShortsword, Element.Earth },
            { ItemID.GoldBroadsword, Element.Earth },
            { ItemID.PlatinumShortsword, Element.Earth },
            { ItemID.PlatinumBroadsword, Element.Earth }
        };

        private static readonly Dictionary<int, List<WeaponSkill>> WeaponSkillMappings = new()
        {
            { ItemID.CactusSword, new List<WeaponSkill> {new WindVerity() } },
            { ItemID.Spear, new List<WeaponSkill> { new FireVerity() } },
            { ItemID.ChainKnife, new List<WeaponSkill> { new FireVerity() } },
            { ItemID.LeadShortsword, new List<WeaponSkill> { new WaterAegis() } },
            { ItemID.LeadBroadsword, new List<WeaponSkill> { new WaterAegis() } },
            { ItemID.SilverShortsword, new List<WeaponSkill> { new FireAegis(), new EarthAegis() } },
            { ItemID.SilverBroadsword, new List<WeaponSkill> { new FireAegis() } },
            { ItemID.TungstenShortsword, new List<WeaponSkill> { new FireAegis(), new WindAegis() } },
            { ItemID.TungstenBroadsword, new List<WeaponSkill> { new FireAegis() } },
            { ItemID.GoldShortsword, new List<WeaponSkill> { new EarthAegis(), new WaterVerity() } },
            { ItemID.GoldBroadsword, new List<WeaponSkill> { new EarthAegis() } },
            { ItemID.PlatinumShortsword, new List<WeaponSkill> { new EarthAegis(), new WindVerity() } },
            { ItemID.PlatinumBroadsword, new List<WeaponSkill> { new EarthAegis() } }
        };

        private static readonly Dictionary<int, WeaponType> weaponTypeMapping = new Dictionary<int, WeaponType>
        {
            { ItemID.WoodenSword, WeaponType.Sword },
            { ItemID.BorealWoodSword, WeaponType.Sword },
            { ItemID.PalmWoodSword, WeaponType.Sword },
            { ItemID.ShadewoodSword, WeaponType.Sword },
            { ItemID.EbonwoodSword, WeaponType.Sword },
            { ItemID.PearlwoodSword, WeaponType.Sword },
            { ItemID.AshWoodSword, WeaponType.Sword },
            { ItemID.CopperShortsword, WeaponType.Sword },
            { ItemID.TinShortsword, WeaponType.Sword },
            { ItemID.CopperBroadsword, WeaponType.Sword },
            { ItemID.TinBroadsword, WeaponType.Sword },
            { ItemID.RichMahoganySword, WeaponType.Sword },
            { ItemID.WoodYoyo, WeaponType.Hand },
            { ItemID.WoodenBoomerang, WeaponType.Hand },
            { ItemID.WoodenBow, WeaponType.Bow },
            { ItemID.PalmWoodBow, WeaponType.Bow },
            { ItemID.EbonwoodBow, WeaponType.Bow },
            { ItemID.ShadewoodBow, WeaponType.Bow },
            { ItemID.AshWoodBow, WeaponType.Bow },
            { ItemID.PearlwoodBow, WeaponType.Bow },
            { ItemID.WandofSparking, WeaponType.Arcane },




            { ItemID.CactusSword, WeaponType.Sword },
            { ItemID.Spear, WeaponType.Spear },
            { ItemID.ChainKnife, WeaponType.Special },
            { ItemID.LeadShortsword, WeaponType.Sword },
            { ItemID.LeadBroadsword, WeaponType.Sword },
            { ItemID.SilverShortsword, WeaponType.Sword },
            { ItemID.SilverBroadsword, WeaponType.Sword },
            { ItemID.TungstenShortsword, WeaponType.Sword },
            { ItemID.TungstenBroadsword, WeaponType.Sword },
            { ItemID.GoldShortsword, WeaponType.Sword },
            { ItemID.GoldBroadsword, WeaponType.Sword },
            { ItemID.PlatinumShortsword, WeaponType.Sword },
            { ItemID.PlatinumBroadsword, WeaponType.Sword }
        };

        private static readonly Dictionary<int, (int maxLevel, int maxUncap, int skillLevelPerCap)> itemStatsDict = new Dictionary<int, (int, int, int)>
        {
            { ItemID.BladedGlove, (2, 2, 1) },
            { ItemID.CactusSword, (2, 1, 1) },
            { ItemID.LeadBroadsword, (2, 1, 1) },
            { ItemID.IronBroadsword, (2, 1, 1) },
            { ItemID.GoldBroadsword, (2, 2, 1) },
            { ItemID.PlatinumBroadsword, (2, 2, 1) },
            { ItemID.Spear, (2, 1, 1) },
            { ItemID.ChainKnife, (2, 1, 1) },
            { ItemID.BladedGlove, (2, 2, 1) }
        };

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return weaponElementMapping.ContainsKey(entity.type);
        }

        public override void SetDefaults(Item entity)
        {

            if (!entity.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
                return;

            if (weaponElementMapping.TryGetValue(entity.type, out Element element))
            {
                globalItem.weaponElement = element;
            }

            if (WeaponSkillMappings.TryGetValue(entity.type, out List<WeaponSkill> skills))
            {
                globalItem.weaponSkills = skills;
            }
            else
            {
                globalItem.weaponSkills = WeaponSkillHelprt.BasicMight(entity);
            }

            if (itemStatsDict.ContainsKey(entity.type))
            {
                var stats = itemStatsDict[entity.type];
                globalItem.maxLevel = stats.maxLevel;
                globalItem.maxUncap = stats.maxUncap;
                globalItem.skillLevelPerCap = stats.skillLevelPerCap;
            }
            else
            {
                globalItem.maxLevel = 1;
                globalItem.maxUncap = 1;
                globalItem.skillLevelPerCap = 1;
            }

            if (entity.type == ItemID.BladedGlove)
            {
                globalItem.maxUncap = 2;
                globalItem.maxLevel = 2;
            }

            if (weaponTypeMapping.TryGetValue(entity.type, out WeaponType weaponType))
            {
                globalItem.WeaponType = weaponType;
            }


            //    // Verity (Crit)

            //     if (entity.type == ItemID.CactusSword || entity.type == ItemID.Spear || entity.type == ItemID.ChainKnife)
            //    {
            //        if (entity.type == ItemID.CactusSword)
            //        {
            //            globalItem.weaponElement = Element.Wind;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WindVerity()
            //            };
            //        }

            //        else if (entity.type == ItemID.Spear || entity.type == ItemID.ChainKnife)
            //        {
            //            globalItem.weaponElement = Element.Fire;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new FireVerity()
            //            };
            //        }

            //        globalItem.maxUncap = 1;
            //        globalItem.maxLevel = 2;

            //        if (entity.type == ItemID.CactusSword)
            //            globalItem.WeaponType = WeaponType.Sword;
            //        else if (entity.type == ItemID.Spear)
            //            globalItem.WeaponType = WeaponType.Spear;
            //        else if (entity.type == ItemID.ChainKnife)
            //            globalItem.WeaponType = WeaponType.Special;

            //    }

            //    // Aegis

            //    else if (
            //        entity.type == ItemID.LeadShortsword || entity.type == ItemID.LeadBroadsword
            //        || entity.type == ItemID.SilverShortsword || entity.type == ItemID.SilverBroadsword
            //        || entity.type == ItemID.IronShortsword || entity.type == ItemID.IronBroadsword
            //        || entity.type == ItemID.TungstenShortsword || entity.type == ItemID.TungstenBroadsword
            //        || entity.type == ItemID.GoldShortsword || entity.type == ItemID.GoldBroadsword
            //        || entity.type == ItemID.PlatinumShortsword || entity.type == ItemID.PlatinumBroadsword
            //        )
            //    {
            //        if (entity.type == ItemID.LeadShortsword || entity.type == ItemID.LeadBroadsword || entity.type == ItemID.IronShortsword || entity.type == ItemID.IronBroadsword)
            //        {
            //            globalItem.weaponElement = Element.Water;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WaterAegis()
            //            };
            //        }

            //        else if (entity.type == ItemID.SilverShortsword || entity.type == ItemID.SilverBroadsword || entity.type == ItemID.TungstenShortsword || entity.type == ItemID.TungstenBroadsword)
            //        {
            //            globalItem.weaponElement = Element.Fire;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new FireAegis()
            //            };
            //        }

            //        else if (entity.type == ItemID.GoldShortsword || entity.type == ItemID.GoldBroadsword
            //        || entity.type == ItemID.PlatinumShortsword || entity.type == ItemID.PlatinumBroadsword)
            //        {
            //            globalItem.weaponElement = Element.Earth;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new EarthAegis()
            //            };
            //        }

            //        globalItem.maxUncap = 1;
            //        globalItem.maxLevel = 2;

            //        if (
            //        entity.type == ItemID.LeadShortsword || entity.type == ItemID.LeadBroadsword
            //        || entity.type == ItemID.SilverShortsword || entity.type == ItemID.SilverBroadsword
            //        || entity.type == ItemID.IronShortsword || entity.type == ItemID.IronBroadsword
            //        || entity.type == ItemID.TungstenShortsword || entity.type == ItemID.TungstenBroadsword
            //        || entity.type == ItemID.GoldShortsword || entity.type == ItemID.GoldBroadsword
            //        || entity.type == ItemID.PlatinumShortsword || entity.type == ItemID.PlatinumBroadsword
            //        )
            //            globalItem.WeaponType = WeaponType.Sword;

            //        //else if (entity.type == ItemID.Spear)
            //        //    globalItem.WeaponType = WeaponType.Spear;
            //        //else if (entity.type == ItemID.ChainKnife)
            //        //    globalItem.WeaponType = WeaponType.Special;

            //    }






            //    // Fortified

            //    else if (
            //        entity.type == ItemID.Flymeal || entity.type == ItemID.AntlionClaw
            //        )
            //    {
            //        if (entity.type == ItemID.Flymeal)
            //        {
            //            globalItem.weaponElement = Element.Wind;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WindFortified()
            //            };
            //        }

            //        if (entity.type == ItemID.AntlionClaw)
            //        {
            //            globalItem.weaponElement = Element.Fire;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new FireFortified()
            //            };
            //        }

            //        globalItem.maxUncap = 1;
            //        globalItem.maxLevel = 2;

            //        if (
            //        entity.type == ItemID.Flymeal || entity.type == ItemID.AntlionClaw
            //        )
            //            globalItem.WeaponType = WeaponType.Hand;

            //    }

            //    // All element damage

            //    else if (
            //        entity.type == ItemID.StylistKilLaKillScissorsIWish || entity.type == ItemID.Ruler
            //        )
            //    {
            //        if (entity.type == ItemID.Ruler)
            //        {
            //            globalItem.weaponElement = Element.Light;
            //        }

            //        if (entity.type == ItemID.StylistKilLaKillScissorsIWish)
            //        {
            //            globalItem.weaponElement = Element.Water;

            //        }

            //        globalItem.weaponSkills = new List<WeaponSkill>
            //        {
            //            new NoneArts()
            //        };

            //        globalItem.maxUncap = 2;
            //        globalItem.maxLevel = 2;

            //        globalItem.WeaponType = WeaponType.Special;

            //    }

            //    // Trium

            //    else if (
            //        entity.type == ItemID.Umbrella || entity.type == ItemID.BreathingReed
            //        )
            //    {
            //        globalItem.weaponElement = Element.Water;

            //        globalItem.weaponSkills = new List<WeaponSkill>
            //        {
            //            new WaterTrium()
            //        };

            //        globalItem.maxUncap = 2;
            //        globalItem.maxLevel = 2;

            //        globalItem.WeaponType = WeaponType.Special;

            //    }

            //    else if (
            //        entity.type == ItemID.BluePhaseblade || entity.type == ItemID.GreenPhaseblade || entity.type == ItemID.OrangePhaseblade || entity.type == ItemID.PurplePhaseblade || entity.type == ItemID.RedPhaseblade || entity.type == ItemID.WhitePhaseblade || entity.type == ItemID.YellowPhaseblade
            //        )
            //    {
            //        if (entity.type == ItemID.BluePhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Water;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WaterMight(),
            //                new WaterVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.GreenPhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Wind;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WindMight(),
            //                new WindVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.OrangePhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Earth;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new EarthMight(),
            //                new EarthVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.RedPhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Fire;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new FireMight(),
            //                new FireVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.PurplePhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Dark;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new DarkMight(),
            //                new DarkVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.YellowPhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Light;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new LightMight(),
            //                new LightVerity()
            //            };
            //        }
            //        else if (entity.type == ItemID.WhitePhaseblade)
            //        {
            //            globalItem.weaponElement = Element.Light;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new NoneBalance(),
            //            };
            //        }



            //        globalItem.maxUncap = 2;
            //        globalItem.maxLevel = 3;

            //        globalItem.skillLevelPerCap = 1;

            //        globalItem.WeaponType = WeaponType.Sword;

            //    }

            //    else if (
            //        entity.type == ItemID.Gladius || entity.type == ItemID.BoneSword || entity.type == ItemID.BatBat || entity.type == ItemID.TentacleSpike
            //        )
            //    {

            //        if (entity.type == ItemID.Gladius)
            //        { 
            //            globalItem.weaponElement = Element.Wind;
            //            globalItem.weaponSkills = new List<WeaponSkill>
            //            {
            //                new WaterTrium()
            //            };
            //        }

            //        else if (entity.type == ItemID.BatBat || entity.type == ItemID.TentacleSpike)
            //            globalItem.weaponElement = Element.Dark;

            //        else if (entity.type == ItemID.BoneSword)
            //            globalItem.weaponElement = Element.Earth;



            //        globalItem.maxUncap = 2;
            //        globalItem.maxLevel = 2;

            //        globalItem.WeaponType = WeaponType.Sword;

            //        if (entity.type == ItemID.BatBat)
            //            globalItem.WeaponType = WeaponType.Special;

            //    }

        }
    }
}
