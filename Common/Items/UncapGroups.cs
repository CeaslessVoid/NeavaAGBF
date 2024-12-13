using NeavaAGBF.Common.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NeavaAGBF.Common.Items
{
    public class UncapGroup
    {
        private Dictionary<int, List<UncapRequirement>> requirements = new();

        public string Name { get; }

        public UncapGroup(string name)
        {
            Name = name;
        }

        public void AddRequirement(int level, List<UncapRequirement> requirements)
        {
            this.requirements[level] = requirements;
        }

        public List<UncapRequirement> GetRequirements(int level)
        {
            if (requirements.ContainsKey(level))
                return requirements[level];

            return requirements[requirements.Keys.Max()];
        }
    }

    public class UncapRequirement
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }

        public UncapRequirement(int itemID, int quantity)
        {
            ItemID = itemID;
            Quantity = quantity;
        }
    }

    public class LoadUncapGroups
    {
        private static readonly Dictionary<string, UncapGroup> uncapGroups = new();

        public UncapGroup uncapGroupTest = new UncapGroup("uncapGroupTest");
        public static void Initialize()
        {
            if (uncapGroups.Count > 0)
            {
                throw new InvalidOperationException("Uncap groups have already been initialized.");
            }

            // Example: Adding a test uncap group
            UncapGroup uncapGroupTest = new UncapGroup("uncapGroupTest");
            uncapGroupTest.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.Wood, 5),
                new UncapRequirement(ItemID.StoneBlock, 10)
            });
                uncapGroupTest.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.Wood, 5),
                new UncapRequirement(ItemID.StoneBlock, 10),
                new UncapRequirement(ItemID.Torch, 5)
            });


            uncapGroups.Add(uncapGroupTest.Name, uncapGroupTest);

            UncapGroups.AddUncapGruops(uncapGroups);
        }


        public static UncapGroup GetUncapGroup(string name)
        {
            if (uncapGroups.Count == 0)
            {
                ModContent.GetInstance<NeavaAGBF>().Logger.Warn("Uncap groups have not been initialized. Returning null.");
                return null;
            }

            if (!uncapGroups.TryGetValue(name, out var group))
            {
                ModContent.GetInstance<NeavaAGBF>().Logger.Warn($"Uncap group '{name}' does not exist. Returning null.");
                return null;
            }

            return group; // Return the valid group
        }


        public static void Clear()
        {
            uncapGroups.Clear();
        }

    }

    public class UncapGroups
    { 
    
        public static void AddUncapGruops(Dictionary<string, UncapGroup> uncapDict) 
        {
            // Add uncap groups here


            // Hero Weapons; Knights edge and Excalibur + Booth True Variants. Also has Terrablade
            UncapGroup uncapGroupHero = new UncapGroup("Hero");
            uncapGroupHero.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.HellstoneBar, 10),
                new UncapRequirement(ItemID.SoulofMight, 10),
                new UncapRequirement(ItemID.PlatinumCoin, 2)
            });
            uncapGroupHero.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 30),
                new UncapRequirement(ItemID.BrokenHeroSword, 1),
                new UncapRequirement(ItemID.PlanteraTrophy, 1),
                new UncapRequirement(ItemID.Seedler, 1),
                new UncapRequirement(ItemID.PlatinumCoin, 3)
            });

            uncapDict.Add(uncapGroupHero.Name, uncapGroupHero);

            // Basic Dark

            UncapGroup uncapGroupDark = new UncapGroup("DarkBasic");
            uncapGroupDark.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 5),
                new UncapRequirement(ItemID.Obsidian, 10),
                new UncapRequirement(ItemID.Deathweed, 25),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupDark.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofMight, 10),
                new UncapRequirement(ItemID.MoonStone, 1),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupDark.Name, uncapGroupDark);

            // Basic Water

            UncapGroup uncapGroupWater = new UncapGroup("WaterBasic");
            uncapGroupWater.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.IceSlimeBanner, 1),
                new UncapRequirement(ItemID.Shiverthorn, 25),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupWater.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofSight, 10),
                new UncapRequirement(ItemID.FrozenTurtleShell, 1),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupWater.Name, uncapGroupWater);

            // Basic Wind

            UncapGroup uncapGroupWind = new UncapGroup("WindBasic");
            uncapGroupWind.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.HarpyBanner, 1),
                new UncapRequirement(ItemID.Feather, 25),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupWind.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofFright, 10),
                new UncapRequirement(ItemID.SoulofFlight, 100),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupWind.Name, uncapGroupWind);

            // Basic Earth

            UncapGroup uncapGroupEarth = new UncapGroup("EarthBasic");
            uncapGroupEarth.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.Stinger, 10),
                new UncapRequirement(ItemID.JungleRose, 10),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupEarth.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofMight, 10),
                new UncapRequirement(ItemID.TurtleShell, 1),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupEarth.Name, uncapGroupEarth);

            // Basic Fire

            UncapGroup uncapGroupFire = new UncapGroup("FireBasic");
            uncapGroupFire.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.Hellstone, 10),
                new UncapRequirement(ItemID.Fireblossom, 25),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupFire.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofSight, 10),
                new UncapRequirement(ItemID.GuideVoodooDoll, 10),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupFire.Name, uncapGroupFire);

            // Basic Light

            UncapGroup uncapGroupLight = new UncapGroup("LightBasic");
            uncapGroupLight.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.CrystalShard, 10),
                new UncapRequirement(ItemID.PixieDust, 25),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupLight.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.HallowedBar, 15),
                new UncapRequirement(ItemID.AvengerEmblem, 1),
                new UncapRequirement(ItemID.SoulofFright, 10),
                new UncapRequirement(ItemID.UnicornHorn, 10),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupLight.Name, uncapGroupLight);

            // Pirate

            UncapGroup uncapGroupPirate = new UncapGroup("Pirate");
            uncapGroupPirate.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.GoldRing, 1),
                new UncapRequirement(ItemID.LuckyCoin, 1),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupPirate.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.ChlorophyteBar, 10),
                new UncapRequirement(ItemID.FlyingDutchmanTrophy, 1),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupPirate.Name, uncapGroupPirate);

            // Food Related (SORA NEXT UPDATE???)

            UncapGroup uncapGroupSORA = new UncapGroup("SORA");
            uncapGroupSORA.AddRequirement(1, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.Bacon, 5),
                new UncapRequirement(ItemID.JojaCola, 10),
                new UncapRequirement(ItemID.FroggleBunwich, 2),
                new UncapRequirement(ItemID.GoldCoin, 50)
            });
            uncapGroupSORA.AddRequirement(2, new List<UncapRequirement>
            {
                new UncapRequirement(ItemID.MonsterLasagna, 3),
                new UncapRequirement(ItemID.PumpkinPie, 3),
                new UncapRequirement(ItemID.SeafoodDinner, 3),
                new UncapRequirement(ItemID.GoldenDelight, 3),
                new UncapRequirement(ItemID.PlatinumCoin, 1)
            });

            uncapDict.Add(uncapGroupSORA.Name, uncapGroupSORA);

        }
    
    }

}
