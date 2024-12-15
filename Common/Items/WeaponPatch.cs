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
        private static readonly Dictionary<int, (
            Element element, 
            WeaponType weaponType, 
            List<WeaponSkill>? skills, 
            (int maxLevel, int maxUncap, int skillLevelPerCap, int startingCap)? stats,
            string UncapGroup, 
            (float chargeGain, int chargeAttackDamage, string charge)? chargeStats)> weaponData = new()
    {
        { ItemID.WoodenSword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.WoodYoyo, (Element.Wind, WeaponType.Hand, null, null, null, null) },
        { ItemID.RichMahoganySword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.CopperShortsword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.TinShortsword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.CopperBroadsword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.TinBroadsword, (Element.Earth, WeaponType.Sword, null, null, null, null) },
        { ItemID.BorealWoodSword, (Element.Water, WeaponType.Sword, null, null, null, null) },
        { ItemID.PalmWoodSword, (Element.Water, WeaponType.Sword, null, null, null, null) },
        { ItemID.PalmWoodBow, (Element.Water, WeaponType.Bow, null, null, null, null) },
        { ItemID.BladedGlove, (Element.Water, WeaponType.Hand, null, (2, 3, 1, 0), null, null) },
        { ItemID.AshWoodSword, (Element.Fire, WeaponType.Sword, null, null, null, null) },
        { ItemID.BorealWoodBow, (Element.Water, WeaponType.Bow, null, null, null, null) },
        { ItemID.AshWoodBow, (Element.Fire, WeaponType.Bow, null, null, null, null) },
        { ItemID.CopperBow, (Element.Fire, WeaponType.Bow, null, null, null, null) },
        { ItemID.TinBow, (Element.Fire, WeaponType.Bow, null, null, null, null) },
        { ItemID.WandofSparking, (Element.Fire, WeaponType.Arcane, null, null, null, null) },
        { ItemID.WoodenBoomerang, (Element.Wind, WeaponType.Hand, null, null, null, null) },
        { ItemID.WoodenBow, (Element.Wind, WeaponType.Bow, null, null, null, null) },
        { ItemID.RichMahoganyBow, (Element.Wind, WeaponType.Bow, null, null, null, null) },
        { ItemID.ShadewoodSword, (Element.Dark, WeaponType.Sword, null, null, null, null) },
        { ItemID.EbonwoodSword, (Element.Dark, WeaponType.Sword, null, null, null, null) },
        { ItemID.ShadewoodBow, (Element.Dark, WeaponType.Bow, null, null, null, null) },
        { ItemID.EbonwoodBow, (Element.Dark, WeaponType.Bow, null, null, null, null) },
        { ItemID.PearlwoodSword, (Element.Light, WeaponType.Sword, null, null, null, null) },
        { ItemID.PearlwoodBow, (Element.Light, WeaponType.Bow, null, null, null, null) },
        { ItemID.CactusSword, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.Spear, (Element.Fire, WeaponType.Spear, new List<WeaponSkill> { new FireVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.ChainKnife, (Element.Fire, WeaponType.Special, new List<WeaponSkill> { new FireVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.LeadShortsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, null, null, null) },
        { ItemID.IronShortsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, null, null, null) },
        { ItemID.LeadBroadsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, (2, 3, 1, 0), null, null) },
        { ItemID.IronBroadsword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis() }, (2, 3, 1, 0), null, null) },
        { ItemID.SilverShortsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis(), new EarthAegis() }, null, null, null) },
        { ItemID.SilverBroadsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis() }, null, null, null) },
        { ItemID.TungstenShortsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis(), new WindAegis() }, null, null, null) },
        { ItemID.TungstenBroadsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireAegis() }, null, null, null) },
        { ItemID.GoldShortsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WaterVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.GoldBroadsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis() }, (2,3,1, 0), null, null) },
        { ItemID.PlatinumShortsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WindVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.PlatinumBroadsword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis() }, (2,3,1, 0), null, null) },
        { ItemID.StylistKilLaKillScissorsIWish, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new NoneArts() }, (2, 3, 1, 0), null, null) },
        { ItemID.Ruler, (Element.Light, WeaponType.Special, new List<WeaponSkill> { new NoneArts() }, (2, 3, 1, 0), null, null) },
        { ItemID.Flymeal, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindFortified() }, (2, 3, 1, 0), null, null) },
        { ItemID.AntlionClaw, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireFortified() }, (2, 3, 1, 0), null, null) },
        { ItemID.Umbrella, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new WaterTrium() }, (2, 3, 1, 0), null, null) },
        { ItemID.BreathingReed, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new WaterTrium() }, (2, 3, 1, 0), null, null) },
        { ItemID.BluePhaseblade, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterMight(), new WaterVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.GreenPhaseblade, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindMight(), new WindVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.OrangePhaseblade, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthMight(), new EarthVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.PurplePhaseblade, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight(), new DarkVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.RedPhaseblade, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireMight(), new FireVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.YellowPhaseblade, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightMight(), new LightVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.WhitePhaseblade, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneBalance() }, (3, 3, 1, 0), null, null) },
        { ItemID.ZombieArm, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity() }, (2, 3, 1, 0), null, null) },
        { ItemID.Gladius, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterTrium(), new EarthMight() }, (2, 3, 1, 0), null, null) },
        { ItemID.BatBat, (Element.Dark, WeaponType.Special, new List<WeaponSkill> { new DarkMight() }, (2,3,1, 0), null, null) },
        { ItemID.TentacleSpike, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMajesty() }, (2,3,1, 0), null, null) },
        { ItemID.BoneSword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis(), new WindAegis() }, (2,3,1, 0), null, null) },
        { ItemID.CandyCaneSword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterVerity(), new WaterTrium() }, (3, 3, 1, 0), null, null) },
        { ItemID.Katana, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina(), new WindMajesty()}, (3, 3, 1, 0), null, null) },
        { ItemID.IceBlade, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterSpearhead(), new WaterMight()}, (6, 3, 1, 3), null, null) },
        { ItemID.LightsBane, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new DarkBloodshed()}, (3, 3, 1, 0), null, null) },
        { ItemID.TragicUmbrella, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new WindMajesty()}, (3, 3, 1, 0), null, null) },
        { ItemID.Muramasa, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterSpearhead(), new WaterTrium()}, (6, 3, 1, 3), null, null) },
        { ItemID.DyeTradersScimitar, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity(), new WindMight()}, (6, 3, 1, 3), null, null) },
        { ItemID.BloodButcherer, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkEnmity(), new DarkBloodshed()}, (3, 3, 1, 0), null, null) },
        { ItemID.Starfury, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneStarTouch(), new WindStamina(), new LightGrace()}, (6, 3, 1, 3), null, null) },
        { ItemID.EnchantedSword, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneEnchantment(), new LightMajesty()}, (6, 3, 1, 3), null, null) },
        { ItemID.PurpleClubberfish, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new EarthMajesty(), new DarkVerity()}, (3, 3, 1, 0), null, null) },
        { ItemID.BeeKeeper, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina(), new WindVerity()}, (3, 3, 1, 0), null, null) },
        { ItemID.FalconBlade, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthMajesty(), new EarthFortified()}, (3, 2, 1, 0), null, null) },
        { ItemID.BladeofGrass, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindVerity(), new EarthFortified()}, (3, 3, 1, 0), null, null) },
        { ItemID.FieryGreatsword, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireEnrage(), new FireCelere()}, (3, 3, 1, 0), null, null) },
        { ItemID.NightsEdge, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new NoneEnchantment(), new NoneBane(),new DarkEnmity() }, (6, 5, 2, 3), "Hero", null) },
        { ItemID.Rally, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthFortified()}, (2, 3, 1, 0), null, null) },
        { ItemID.CrimsonYoyo, (Element.Dark, WeaponType.Hand, new List<WeaponSkill> { new DarkMight(), new DarkVerity()}, (2, 3, 1, 0), null, null) },
        { ItemID.CorruptYoyo, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new DarkMight(), new DarkVerity()}, (2, 3, 1, 0), null, null) },
        { ItemID.JungleYoyo, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthFortified(), new WindAegis()}, (2, 3, 1, 0), null, null) },
        { ItemID.Code1, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthMajesty(), new FireAegis()}, (2, 3, 1, 0), null, null) },
        { ItemID.Valor, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthFortified(), new EarthMajesty()}, (2, 3, 1, 0), null, null) },
        { ItemID.Cascade, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireAegis(), new FireVerity()}, (2, 3, 1, 0), null, null) },
        { ItemID.HiveFive, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthFortified(), new EarthVerity()}, (3, 3, 1, 0), null, null) },
        { ItemID.Trident, (Element.Water, WeaponType.Spear, new List<WeaponSkill> { new WaterSpearhead()}, (1, 3, 1, 0), null, null) },
        { ItemID.ThunderSpear, (Element.Water, WeaponType.Spear, new List<WeaponSkill> { new WaterTrium(), new WaterVerity()}, (2, 3, 1, 0), null, null) },
        { ItemID.TheRottedFork, (Element.Dark, WeaponType.Spear, new List<WeaponSkill> { new DarkVerity(), new DarkMajesty()}, (3, 3, 1, 0), null, null) },
        { ItemID.DarkLance, (Element.Dark, WeaponType.Spear, new List<WeaponSkill> { new DarkVerity(), new DarkBloodshed() }, (6, 3, 1, 3), null, null) },
        { ItemID.Swordfish, (Element.Water, WeaponType.Spear, new List<WeaponSkill> { new WaterSpearhead(), new WaterTrium() }, (6, 3, 1, 3), null, null) },
        { ItemID.EnchantedBoomerang, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindFortified(), new WindMajesty() }, (6, 3, 1, 3), null, null) },
        { ItemID.FruitcakeChakram, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindFortified(), new WindMajesty() }, (6, 3, 1, 3), null, null) },
        { ItemID.BloodyMachete, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireCelere(), new WindVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.Shroomerang, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindMight(), new WindAegis() }, (3, 3, 1, 0), null, null) },
        { ItemID.IceBoomerang, (Element.Water, WeaponType.Hand, new List<WeaponSkill> { new WaterTrium(), new WindMajesty() }, (3, 3, 1, 0), null, null) },
        { ItemID.Trimarang, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new NoneEnchantment(), new WindMajesty(), new WindVerity() }, (6, 3, 1, 3), null, null) },
        { ItemID.ThornChakram, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new WindMajesty(), new WindAegis() }, (3, 3, 1, 0), null, null) },
        { ItemID.CombatWrench, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new NoneArts(), new WindMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.Flamarang, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireFortified(), new FireCelere() }, (3, 3, 1, 0), null, null) },
        { ItemID.Mace, (Element.Wind, WeaponType.Special, new List<WeaponSkill> { new WindStamina() }, (2, 3, 1, 0), null, null) },
        { ItemID.FlamingMace, (Element.Fire, WeaponType.Special, new List<WeaponSkill> { new WindStamina(), new FireMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.BallOHurt, (Element.Dark, WeaponType.Special, new List<WeaponSkill> { new DarkVerity(), new DarkMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.TheMeatball, (Element.Dark, WeaponType.Special, new List<WeaponSkill> { new DarkVerity(), new DarkMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.BlueMoon, (Element.Water, WeaponType.Special, new List<WeaponSkill> { new NoneWaterMoon(), new WaterAegis() }, (6, 3, 1, 3), null, null) },
        { ItemID.Sunfury, (Element.Fire, WeaponType.Special, new List<WeaponSkill> { new NoneFireSun(), new FireAegis() }, (6, 3, 1, 3), null, null) },
        { ItemID.Terragrim, (Element.Wind, WeaponType.Special, new List<WeaponSkill> { new NoneArts() }, (6, 3, 1, 3), null, null) },
        { ItemID.LeadBow, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave() }, (2, 3, 1, 0), null, null) },
        { ItemID.IronBow, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave() }, (2, 3, 1, 0), null, null) },
        { ItemID.SilverBow, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave() }, (2, 3, 1, 0), null, null) },
        { ItemID.TungstenBow, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave() }, (2, 3, 1, 0), null, null) },
        { ItemID.GoldAxe, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new EarthMajesty() }, (2, 3, 1, 0), null, null) },
        { ItemID.PlatinumBow, (Element.Earth, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new EarthMajesty() }, (2, 3, 1, 0), null, null) },
        { ItemID.DemonBow, (Element.Dark, WeaponType.Bow, new List<WeaponSkill> { new DarkMight(), new DarkVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.TendonBow, (Element.Dark, WeaponType.Bow, new List<WeaponSkill> { new DarkMight(), new DarkVerity() }, (2, 3, 1, 0), null, null) },
        { ItemID.BloodRainBow, (Element.Dark, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new DarkEnmity(), new DarkVerity() }, (3, 3, 1, 0), null, null) },
        { ItemID.BeesKnees, (Element.Wind, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new WindMajesty()}, (3, 3, 1, 0), null, null) },
        { ItemID.MoltenFury, (Element.Fire, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new FireEnrage()}, (3, 3, 1, 0), null, null) },
        { ItemID.HellwingBow, (Element.Fire, WeaponType.Bow, new List<WeaponSkill> { new NoneAmmoSave(), new FireCelere(), new FireMajesty() }, (6, 3, 1, 3), null, null) },
        { ItemID.RedRyder, (Element.Fire, WeaponType.Gun, new List<WeaponSkill> { new FireMystery() }, (2, 3, 1, 0), null, null) },
        { ItemID.FlintlockPistol, (Element.Wind, WeaponType.Gun, new List<WeaponSkill> { new WindMystery() }, (2, 3, 1, 0), null, null) },
        { ItemID.Musket, (Element.Earth, WeaponType.Gun, new List<WeaponSkill> { new EarthMystery(), new EarthMajesty() }, (6, 3, 1, 3), null, null) },
        { ItemID.TheUndertaker, (Element.Dark, WeaponType.Gun, new List<WeaponSkill> { new DarkMystery(), new DarkVerity() }, (6, 3, 1, 3), null, null) },
        { ItemID.Sandgun, (Element.Earth, WeaponType.Gun, new List<WeaponSkill> { new EarthMystery(), new EarthFortified() }, (3, 3, 1, 0), null, null) },
        { ItemID.Revolver, (Element.Wind, WeaponType.Gun, new List<WeaponSkill> { new WindMystery(), new WindMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.Minishark, (Element.Water, WeaponType.Gun, new List<WeaponSkill> { new WaterMystery(), new WaterSpearhead() }, (6, 3, 1, 3), null, null) },
        { ItemID.Boomstick, (Element.Fire, WeaponType.Gun, new List<WeaponSkill> { new FireMystery() }, (2, 3, 1, 0), null, null) },
        { ItemID.QuadBarrelShotgun, (Element.Wind, WeaponType.Gun, new List<WeaponSkill> { new WindMystery(), new EarthMajesty() }, (3, 3, 1, 0), null, null) },
        { ItemID.Handgun, (Element.Fire, WeaponType.Gun, new List<WeaponSkill> { new FireMight(), new WindMight() }, (3, 3, 1, 0), null, null) },
        { ItemID.PhoenixBlaster, (Element.Fire, WeaponType.Gun, new List<WeaponSkill> { new FireMystery(), new FireEnrage() }, (3, 3, 1, 0), null, null) },
        { ItemID.PewMaticHorn, (Element.Fire, WeaponType.Gun, new List<WeaponSkill> { new WindMystery(), new WindStamina() }, (3, 3, 1, 0), null, null) },
        { ItemID.FlareGun, (Element.Fire, WeaponType.Gun, null, null, null, null) },
        { ItemID.AleThrowingGlove, (Element.Dark, WeaponType.Gun, null, null, null, null) },
        { ItemID.Blowpipe, (Element.Wind, WeaponType.Gun, null, null, null, null) },
        { ItemID.Blowgun, (Element.Wind, WeaponType.Gun, null, (2,3,1,0), null, null) },
        { ItemID.SnowballCannon, (Element.Water, WeaponType.Gun, new List<WeaponSkill> { new WaterMystery(), new WaterMight() }, (3,3,1,0), null, null) },
        { ItemID.PainterPaintballGun, (Element.Water, WeaponType.Gun, new List<WeaponSkill> { new WaterMystery(), new WaterAegis() }, (3,3,1,0), null, null) },
        { ItemID.Harpoon, (Element.Earth, WeaponType.Gun, new List<WeaponSkill> { new EarthMajesty(), new EarthFortified() }, (3,3,1,0), null, null) },
        { ItemID.StarCannon, (Element.Light, WeaponType.Gun, new List<WeaponSkill> { new StarCannonMain(), new LightMystery(), new LightMajesty() }, (6,3,1,3), null, (0.01f, 3, "Test")) },
        { ItemID.WandofFrosting, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WaterMight(), new WaterVerity() }, (2,3,1,0), null, null) },
        { ItemID.ThunderStaff, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WaterDeathstrike(), new WaterVerity() }, (6,3,1,3), null, null) },
        { ItemID.AmethystStaff, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new DarkDeathstrike() }, (2,3,1,0), null, null) },
        { ItemID.TopazStaff, (Element.Earth, WeaponType.Arcane, new List<WeaponSkill> { new EarthDeathstrike() }, (2,3,1,0), null, null) },
        { ItemID.SapphireStaff, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WaterDeathstrike() }, (2,3,1,0), null, null) },
        { ItemID.RubyStaff, (Element.Fire, WeaponType.Arcane, new List<WeaponSkill> { new FireDeathstrike() }, (2,3,1,0), null, null) },
        { ItemID.DiamondStaff, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WindDeathstrike(), new WaterDeathstrike() }, (2,3,1,0), null, null) },
        { ItemID.AmberStaff, (Element.Light, WeaponType.Arcane, new List<WeaponSkill> { new LightDeathstrike(), new LightMajesty() }, (3,3,1,0), null, null) },
        { ItemID.Vilethorn, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new DarkDeathstrike(), new WindMajesty() }, (3,3,1,0), null, null) },
        { ItemID.WeatherPain, (Element.Wind, WeaponType.Arcane, new List<WeaponSkill> { new WindDeathstrike(), new WindFortified() }, (3,3,1,0), null, null) },
        { ItemID.MagicMissile, (Element.Wind, WeaponType.Arcane, new List<WeaponSkill> { new WindDeathstrike(), new WindMajesty(), new WindStamina() }, (6,3,1,3), null, null) },
        { ItemID.AquaScepter, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WaterDeathstrike(), new WaterSpearhead() }, (6,3,1,3), null, null) },
        { ItemID.FlowerofFire, (Element.Fire, WeaponType.Arcane, new List<WeaponSkill> { new FireDeathstrike(), new FireEnrage() }, (6,3,1,3), null, null) },
        { ItemID.Flamelash, (Element.Fire, WeaponType.Arcane, new List<WeaponSkill> { new FireDeathstrike(), new FireCelere() }, (6,3,1,3), null, null) },
        { ItemID.SpaceGun, (Element.Fire, WeaponType.Arcane, new List<WeaponSkill> { new FireDeathstrike(), new FireMystery() }, (3,3,1,0), null, null) },
        { ItemID.BeeGun, (Element.Wind, WeaponType.Arcane, new List<WeaponSkill> { new WindDeathstrike(), new WindMajesty() }, (3,3,1,0), null, null) },
        { ItemID.WaterBolt, (Element.Water, WeaponType.Arcane, new List<WeaponSkill> { new WaterDeathstrike(), new WaterSpearhead() }, (6,3,1,3), null, null) },
        { ItemID.BookofSkulls, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new DarkDeathstrike(), new DarkVerity() }, (3,3,1,0), null, null) },
        { ItemID.DemonScythe, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new DarkDeathstrike(), new DarkEnmity() }, (3,3,1,0), null, null) },
        { ItemID.CrimsonRod, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new DarkDeathstrike(), new DarkBloodshed() }, (6,3,1,3), null, null) },
        { ItemID.BabyBirdStaff, (Element.Wind, WeaponType.Arcane, null, (6,3,1,3), null, null) },
        { ItemID.SlimeStaff, (Element.Wind, WeaponType.Arcane, null, (2,3,1,3), null, null) },
        { ItemID.FlinxStaff, (Element.Wind, WeaponType.Arcane, new List<WeaponSkill> { new WindVerity() }, (3,3,1,0), null, null) },
        { ItemID.AbigailsFlower, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new NoneSummonCharge(), new DarkBloodshed() }, (6,3,1,3), null, null) },
        { ItemID.HornetStaff, (Element.Earth, WeaponType.Arcane, new List<WeaponSkill> { new NoneSummonCharge(), new EarthMajesty() }, (3,3,1,0), null, null) },
        { ItemID.VampireFrogStaff, (Element.Dark, WeaponType.Arcane, new List<WeaponSkill> { new NoneSummonCharge(), new DarkMajesty() }, (3,3,1,0), null, null) },
        { ItemID.ImpStaff, (Element.Fire, WeaponType.Arcane, new List<WeaponSkill> { new NoneSummonCharge(), new FireFortified() }, (3,3,1,0), null, null) },
        { ItemID.BlandWhip, (Element.Earth, WeaponType.Whip, new List<WeaponSkill> { new EarthAegis() }, (2,3,1,0), null, (1,1,null)) },
        { ItemID.ThornWhip, (Element.Earth, WeaponType.Whip, new List<WeaponSkill> { new WindEssence() }, (3,3,1,0), null, (1,1,null)) },
        { ItemID.BoneWhip, (Element.Earth, WeaponType.Whip, new List<WeaponSkill> { new DarkEssence(), new DarkMajesty() }, (3,3,1,0), null, (1,1,null)) },

        // Hardmode Items

        { ItemID.TaxCollectorsStickOfDoom, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkAegis2(), new DarkMajesty() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.SlapHand, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight2() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.CobaltSword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis2() }, (5,5,2,0), "WaterBasic", null) },
        { ItemID.PalladiumSword, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterAegis2() }, (5,5,2,0), "WaterBasic", null) },

        { ItemID.BluePhasesaber, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterMight2(), new WaterVerity2() }, (5, 5, 2, 0), "WaterBasic", null) },
        { ItemID.GreenPhasesaber, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindMight2(), new WindVerity2() }, (5, 5, 2, 0), "WindBasic", null) },
        { ItemID.OrangePhasesaber, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthMight2(), new EarthVerity2() }, (5, 5, 2, 0), "EarthBasic", null) },
        { ItemID.PurplePhasesaber, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight2(), new DarkVerity2() }, (5, 5, 2, 0), "DarkBasic", null) },
        { ItemID.RedPhasesaber, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireMight2(), new FireVerity2() }, (5, 5, 2, 0), "FireBasic", null) },
        { ItemID.YellowPhasesaber, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightMight2(), new LightVerity2() }, (5, 5, 2, 0), "LightBasic", null) },
        { ItemID.WhitePhasesaber, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneBalance2() }, (5, 5, 2, 0), "LightBasic", null) },

        { ItemID.IceSickle, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterSpearhead(), new WaterDemolishment() }, (5, 5, 2, 0), "WaterBasic", null) },
        { 3823, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireMight2(), new FireAegis2() }, (5, 5, 2, 0), "FireBasic", null) }, // Brand of inferno

        { ItemID.MythrilSword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis2(), new EarthMight2() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.OrichalcumSword, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthAegis2(), new EarthMight2() }, (5,5,2,0), "EarthBasic", null) },

        { ItemID.BreakerBlade, (Element.Special, WeaponType.Sword, new List<WeaponSkill> { new SpecialElementWeapon(), new BreakerGuard() }, (5,3,2,0), null, null) },
        { ItemID.Cutlass, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina2(), new WindAegis2() }, (5,5,2,0), "Pirate", null) },
        { ItemID.Frostbrand, (Element.Water, WeaponType.Sword, new List<WeaponSkill> { new WaterFandango(), new WaterAegis2() }, (5,5,2,0), "WaterBasic", null) },

        { ItemID.AdamantiteSword, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightMight2(), new LightAegis2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.TitaniumSword, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightMight2(), new LightAegis2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.BeamSword, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightDemolishment(), new LightMajesy2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.FetidBaghnakhs, (Element.Dark, WeaponType.Hand, new List<WeaponSkill> { new DarkMight2(), new DarkEnmity2() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.Bladetongue, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkMight2(), new DarkEnmity2() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.HamBat, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new NoneHamBat(), new DarkMight2() }, (5,5,2,0), "DarkBasic", null) },

        { ItemID.Excalibur, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new ChosenBlade(), new LightMajesy2() }, (5,4,2,0), "Hero", null) },
        { ItemID.TrueExcalibur, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new ChosenBlade2(), new ChosenBlade(), new LightMajesy2() }, (11,5,2,3), "Hero", null) },
        { ItemID.ChlorophyteSaber, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthVerity2(), new EarthMajesty2() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.ChlorophyteClaymore, (Element.Earth, WeaponType.Sword, new List<WeaponSkill> { new EarthCrux(), new EarthMajesty2() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.DeathSickle, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkSpearhead(), new DarkDemolishment() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.PsychoKnife, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new DarkBloodshed(), new DarkEnrage2() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.Keybrand, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina2(), new WindDemolishment() }, (5,5,2,0), "WindBasic", null) },
        { ItemID.TheHorsemansBlade, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireMajesty(), new FireCelere2() }, (5,5,2,0), "FireBasic", null) },
        { ItemID.ChristmasTreeSword, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindCrux(), new WindStamina2() }, (5,5,2,0), "WindBasic", null) },

        { ItemID.TrueNightsEdge, (Element.Dark, WeaponType.Sword, new List<WeaponSkill> { new NoneEnchantment2(), new NoneBane(),new DarkTyranny() }, (11,5,2,3), "Hero", null) },
        { ItemID.Seedler, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new WindStamina2(), new WindMajesty() }, (5,5,2,0), "FireBasic", null) },
        { 3827, (Element.Fire, WeaponType.Sword, new List<WeaponSkill> { new FireEnrage2(), new FireMight2() }, (5,5,2,0), "WindBasic", null) }, // Flying dragon
        { ItemID.PiercingStarlight, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new LightGrace2(), new LightMajesy2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.TerraBlade, (Element.Wind, WeaponType.Sword, new List<WeaponSkill> { new TerraMight(), new ChosenBlade2(), new NoneBane() }, (0,5,0,3), "Hero", null) },

        { ItemID.InfluxWaver, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneFlux(), new LightMight2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.StarWrath, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new NoneStarTouch(), new WindEnrage2(), new WindStamina2()  }, (5,5,2,0), "LightBasic", null) },
        { ItemID.Meowmere, (Element.Light, WeaponType.Sword, new List<WeaponSkill> { new MeowMur(), new LightGrace2() }, (5,5,2,0), "LightBasic", null) },

        // Holy fuck

        { ItemID.FormatC, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new FireAegis2(), new FireVerity2() }, (5,5,2,0), "FireBasic", null) },
        { ItemID.Gradient, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthAegis2(), new EarthVerity2() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.Chik, (Element.Light, WeaponType.Hand, new List<WeaponSkill> { new LightAegis2(), new LightVerity2() }, (5,5,2,0), "LightBasic", null) },
        { ItemID.HelFire, (Element.Fire, WeaponType.Hand, new List<WeaponSkill> { new NoneHelFire(), new FireDemolishment() }, (5,5,2,0), "FireBasic", null) },
        { ItemID.Amarok, (Element.Water, WeaponType.Hand, new List<WeaponSkill> { new NoneAmarok(), new WaterCelere2() }, (5,5,2,0), "WaterBasic", null) },
        { ItemID.Yelets, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new NoneYelets(), new EarthBladeshield() }, (5,5,2,0), "EarthBasic", null) },

        { ItemID.ValkyrieYoyo, (Element.Earth, WeaponType.Hand, new List<WeaponSkill> { new EarthMajesty2(), new EarthBladeshield() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.RedsYoyo, (Element.Dark, WeaponType.Hand, new List<WeaponSkill> { new DarkDemolishment(), new DarkEnmity2() }, (5,5,2,0), "DarkBasic", null) },
        { ItemID.Kraken, (Element.Water, WeaponType.Hand, new List<WeaponSkill> { new WaterEnrage2(), new WaterMajesty2() }, (5,5,2,0), "WaterBasic", null) },
        { ItemID.TheEyeOfCthulhu, (Element.Dark, WeaponType.Hand, new List<WeaponSkill> { new DarkEnmity2(), new DarkTyranny() }, (5,5,2,0), "WaterBasic", null) },
        { ItemID.Terrarian, (Element.Wind, WeaponType.Hand, new List<WeaponSkill> { new NoneTerrarian(), new WindStamina2() }, (5,5,2,0), "WaterBasic", null) },

        { ItemID.CobaltNaginata, (Element.Fire, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new FireSpearhead() }, (5,5,2,0), "FireBasic", null) },
        { ItemID.PalladiumPike, (Element.Fire, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new FireSpearhead() }, (5,5,2,0), "FireBasic", null) },
        { ItemID.MythrilHalberd, (Element.Wind, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new WindSpearhead() }, (5,5,2,0), "WindBasic", null) },
        { ItemID.OrichalcumHalberd, (Element.Wind, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new WindSpearhead() }, (5,5,2,0), "WindBasic", null) },
        { ItemID.AdamantiteGlaive, (Element.Earth, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new EarthSpearhead() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.TitaniumTrident, (Element.Earth, WeaponType.Spear, new List<WeaponSkill> { new NoneExploit1(), new EarthSpearhead() }, (5,5,2,0), "EarthBasic", null) },
        { ItemID.Gungnir, (Element.Light, WeaponType.Spear, new List<WeaponSkill> { new GungnirPassive(), new LightGrace2() }, (5,5,2,0), "Hero", null) },

        { 3836, (Element.Fire, WeaponType.Spear, new List<WeaponSkill> { new NoneHpPerFire(), new FireMajesty2() }, (5,5,2,0), "FireBasic", null) }, // Ghastly Glaive
        { ItemID.ChlorophytePartisan, (Element.Wind, WeaponType.Spear, new List<WeaponSkill> { new NoneToxicosis(), new WindStamina2() }, (5,5,2,0), "WindBasic", null) },
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


                // When Charge Attacks are implemented

                if (data.chargeStats.HasValue)
                {
                    globalItem.chargeAttackDamage = data.chargeStats.Value.chargeAttackDamage;
                    globalItem.chargeGain = data.chargeStats.Value.chargeGain;

                    if (!string.IsNullOrEmpty(data.chargeStats.Value.charge) &&
                        ChargeAttacks.chargeAttackDict.TryGetValue(data.chargeStats.Value.charge, out var chargeData))
                    {
                        globalItem.chargeAttack = (Player player, float multi) => chargeData.Action(player, entity, multi);
                        globalItem.chargeName = chargeData.Name;
                        globalItem.chargeAttackString = chargeData.Description;
                    }
                    else
                    {

                    }
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
