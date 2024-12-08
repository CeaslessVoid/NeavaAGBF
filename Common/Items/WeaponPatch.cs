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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NeavaAGBF.Common.Items
{
    // Prehardmode weapons do not focus too much for now. We can give them generic skills


    // Fire -> Hight crit and crit damage.
    // Water -> Generic high attkck speed and damage
    // Earth -> Tanky
    // Wind -> All rounder. Wants high health

    // Light and Dark are throwaways for overpowered weapons / some modded content

    // Light -> Want high health. Healing? High Health. OP
    // Dark -> High damage, REALLY WANTS LOW HEALTH

    public class WeaponPatchGlobalItem : GlobalItem
    {
        private static readonly Dictionary<int, (Element element, WeaponType weaponType, List<WeaponSkill>? skills, (int maxLevel, int maxUncap, int skillLevelPerCap, int startingCap)? stats, string UncapGroup)> weaponData = new()
    {
        { ItemID.WoodenSword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.WoodYoyo, (Element.Wind, WeaponType.Hand, null, null, null) },
        { ItemID.RichMahoganySword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.CopperShortsword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.TinShortsword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.CopperBroadsword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.TinBroadsword, (Element.Earth, WeaponType.Sword, null, null, null) },
        { ItemID.BorealWoodSword, (Element.Water, WeaponType.Sword, null, null, null) },
        { ItemID.PalmWoodSword, (Element.Water, WeaponType.Sword, null, null, null) },
        { ItemID.PalmWoodBow, (Element.Water, WeaponType.Bow, null, null, null) },
        { ItemID.BladedGlove, (Element.Water, WeaponType.Hand, null, (2, 3, 1, 0), null) },
        { ItemID.AshWoodSword, (Element.Fire, WeaponType.Sword, null, null, null) },
        { ItemID.AshWoodBow, (Element.Fire, WeaponType.Bow, null, null, null) },
        { ItemID.CopperBow, (Element.Fire, WeaponType.Bow, null, null, null) },
        { ItemID.TinBow, (Element.Fire, WeaponType.Bow, null, null, null) },
        { ItemID.WandofSparking, (Element.Fire, WeaponType.Arcane, null, null, null) },
        { ItemID.WoodenBoomerang, (Element.Wind, WeaponType.Hand, null, null, null) },
        { ItemID.WoodenBow, (Element.Wind, WeaponType.Bow, null, null, null) },
        { ItemID.RichMahoganyBow, (Element.Wind, WeaponType.Bow, null, null, null) },
        { ItemID.ShadewoodSword, (Element.Dark, WeaponType.Sword, null, null, null) },
        { ItemID.EbonwoodSword, (Element.Dark, WeaponType.Sword, null, null, null) },
        { ItemID.ShadewoodBow, (Element.Dark, WeaponType.Bow, null, null, null) },
        { ItemID.EbonwoodBow, (Element.Dark, WeaponType.Bow, null, null, null) },
        { ItemID.PearlwoodSword, (Element.Light, WeaponType.Sword, null, null, null) },
        { ItemID.PearlwoodBow, (Element.Light, WeaponType.Bow, null, null, null) },
        { ItemID.CactusSword, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity() }, (2, 3, 1, 0), null) },
        { ItemID.Spear, (Element.Fire, WeaponType.Spear, new List<WeaponSkill> { new FireVerity() }, (2, 3, 1, 0), null) },
        { ItemID.ChainKnife, (Element.Fire, WeaponType.Special, new List<WeaponSkill> { new FireVerity() }, (2, 3, 1, 0), null) },
        { ItemID.LeadShortsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, null, null) },
        { ItemID.IronShortsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, null, null) },
        { ItemID.LeadBroadsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, (2, 3, 1, 0), null) },
        { ItemID.IronBroadsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, (2, 3, 1, 0), null) },
        { ItemID.SilverShortsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis(), new EarthAegis() }, null, null) },
        { ItemID.SilverBroadsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis() }, null, null) },
        { ItemID.TungstenShortsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis(), new WindAegis() }, null, null) },
        { ItemID.TungstenBroadsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis() }, null, null) },
        { ItemID.GoldShortsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WaterVerity() }, (2, 3, 1, 0), null) },
        { ItemID.GoldBroadsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis() }, (2,3,1, 0), null) },
        { ItemID.PlatinumShortsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WindVerity() }, (2, 3, 1, 0), null) },
        { ItemID.PlatinumBroadsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis() }, (2,3,1, 0), null) },
        { ItemID.StylistKilLaKillScissorsIWish, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new NoneArts() }, (2, 3, 1, 0), null) },
        { ItemID.Ruler, (Element.Light, WeaponType.Special, new List<WeaponSkill> { new NoneArts() }, (2, 3, 1, 0), null) },
        { ItemID.Flymeal, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindFortified() }, (2, 3, 1, 0), null) },
        { ItemID.AntlionClaw, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireFortified() }, (2, 3, 1, 0), null) },
        { ItemID.Umbrella, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new WaterTrium() }, (2, 3, 1, 0), null) },
        { ItemID.BreathingReed, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new WaterTrium() }, (2, 3, 1, 0), null) },
        { ItemID.BluePhaseblade, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterMight(), new WaterVerity() }, (3, 3, 1, 0), null) },
        { ItemID.GreenPhaseblade, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindMight(), new WindVerity() }, (3, 3, 1, 0), null) },
        { ItemID.OrangePhaseblade, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthMight(), new EarthVerity() }, (3, 3, 1, 0), null) },
        { ItemID.PurplePhaseblade, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight(), new DarkVerity() }, (3, 3, 1, 0), null) },
        { ItemID.RedPhaseblade, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireMight(), new FireVerity() }, (3, 3, 1, 0), null) },
        { ItemID.YellowPhaseblade, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightMight(), new LightVerity() }, (3, 3, 1, 0), null) },
        { ItemID.WhitePhaseblade, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneBalance() }, (3, 3, 1, 0), null) },
        { ItemID.ZombieArm, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity() }, (2, 3, 1, 0), null) },
        { ItemID.Gladius, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterTrium(), new EarthMight() }, (2, 3, 1, 0), null) },
        { ItemID.BatBat, (Element.Dark, WeaponType.Special, new List<WeaponSkill> { new DarkMight() }, (2,3,1, 0), null) },
        { ItemID.TentacleSpike, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight() }, (2,3,1, 0), null) },
        { ItemID.BoneSword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WindAegis() }, (2,3,1, 0), null) },
        { ItemID.CandyCaneSword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterVerity(), new WaterTrium() }, (3, 3, 1, 0), null) },
        { ItemID.Katana, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindAegis(), new WindStamina()}, (3, 3, 1, 0), null) },
        { ItemID.IceBlade, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterSpearhead(), new WaterMight()}, (6, 3, 1, 3), null) },
        { ItemID.LightsBane, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new DarkBloodshed()}, (3, 3, 1, 0), null) },
        { ItemID.TragicUmbrella, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new WindMight()}, (3, 3, 1, 0), null) },
        { ItemID.Muramasa, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterSpearhead(), new WaterTrium()}, (6, 3, 1, 3), null) },
        { ItemID.DyeTradersScimitar, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity(), new WindMight()}, (6, 3, 1, 3), null) },
        { ItemID.BloodButcherer, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new DarkBloodshed()}, (3, 3, 1, 0), null) },
        { ItemID.Starfury, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneStarTouch(), new WindStamina(), new LightGrace()}, (6, 3, 1, 3), null) },
        { ItemID.EnchantedSword, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneEnchantment(), new LightAegis()}, (6, 3, 1, 3), null) },
        { ItemID.PurpleClubberfish, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new DarkVerity()}, (3, 3, 1, 0), null) },
        { ItemID.BeeKeeper, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina(), new WindVerity()}, (3, 3, 1, 0), null) },
        { ItemID.FalconBlade, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthAegis(), new EarthFortified()}, (3, 2, 1, 0), null) },
        { ItemID.BladeofGrass, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity(), new WindMight(), new EarthFortified()}, (3, 3, 1, 0), null) },
        { ItemID.FieryGreatsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireEnrage(), new FireCelere()}, (3, 3, 1, 0), null) },
        { ItemID.NightsEdge, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new NoneEnchantment(), new NoneBane(),new DarkEnmity()}, (6, 4, 2, 3), "Hero") },
        { ItemID.Rally, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthFortified()}, (2, 3, 1, 0), null) },
        };

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return weaponData.ContainsKey(entity.type);
        }

        public override void SetDefaults(Item entity)
        {

            if (!entity.TryGetGlobalItem(out WeaponSkillsGlobalItem globalItem))
                return;

            if (weaponData.TryGetValue(entity.type, out var data))
            {
                globalItem.weaponElement = data.element;

                globalItem.weaponSkills = data.skills ?? WeaponSkillHelprt.BasicMight(entity);

                if (data.stats.HasValue)
                {
                    globalItem.maxLevel = data.stats.Value.maxLevel;
                    globalItem.maxUncap = data.stats.Value.maxUncap;
                    globalItem.skillLevelPerCap = data.stats.Value.skillLevelPerCap;
                    globalItem.currentUncap = data.stats.Value.startingCap;
                }
                else
                {
                    globalItem.maxLevel = 1;
                    globalItem.maxUncap = 3;
                    globalItem.skillLevelPerCap = 1;
                }

                globalItem.WeaponType = data.weaponType;

                if (!string.IsNullOrEmpty(data.UncapGroup))
                {
                    globalItem.UncapGroup = LoadUncapGroups.GetUncapGroup(data.UncapGroup);
                }

            }

        }
    }
}
