﻿using NeavaAGBF.Common.Items;
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

        }
    
    }

}
