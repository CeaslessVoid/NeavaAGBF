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
        private readonly Dictionary<int, List<UncapRequirement>> _requirements = new();

        public string Name { get; }

        public UncapGroup(string name)
        {
            Name = name;
        }

        public void AddRequirement(int level, IEnumerable<UncapRequirement> requirements)
        {
            _requirements[level] = requirements.ToList();
        }

        public IReadOnlyList<UncapRequirement> GetRequirements(int level)
        {
            if (_requirements.TryGetValue(level, out var reqs))
            {
                return reqs;
            }

            return _requirements[_requirements.Keys.Max()];
        }
    }

    public record UncapRequirement(int ItemID, int Quantity);

    public static class LoadUncapGroups
    {
        private static readonly Dictionary<string, UncapGroup> UncapGroups = new();

        public static void Initialize()
        {
            if (UncapGroups.Any())
            {
                throw new InvalidOperationException("Uncap groups have already been initialized.");
            }

            AddDefaultGroups();
        }

        public static UncapGroup GetUncapGroup(string name)
        {
            if (!UncapGroups.Any())
            {
                return null;
            }

            if (!UncapGroups.TryGetValue(name, out var group))
            {
                return null;
            }

            return group;
        }

        public static void Clear() => UncapGroups.Clear();

        private static void AddDefaultGroups()
        {
            AddUncapGroup("Hero", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.HellstoneBar, 10), new(ItemID.SoulofMight, 10), new(ItemID.PlatinumCoin, 2) } },
                { 2, new() { new(ItemID.HallowedBar, 30), new(ItemID.BrokenHeroSword, 1), new(ItemID.PlanteraTrophy, 1), new(ItemID.Seedler, 1), new(ItemID.PlatinumCoin, 3) } }
            });

            AddUncapGroup("DarkBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 5), new(ItemID.Obsidian, 10), new(ItemID.Deathweed, 25), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofMight, 10), new(ItemID.MoonStone, 1), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("WaterBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.IceSlimeBanner, 1), new(ItemID.Shiverthorn, 25), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofSight, 10), new(ItemID.FrozenTurtleShell, 1), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("WindBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.HarpyBanner, 1), new(ItemID.Feather, 25), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofFright, 10), new(ItemID.SoulofFlight, 100), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("EarthBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.Stinger, 10), new(ItemID.JungleRose, 10), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofMight, 10), new(ItemID.TurtleShell, 1), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("FireBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.Hellstone, 10), new(ItemID.Fireblossom, 25), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofSight, 10), new(ItemID.GuideVoodooDoll, 10), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("LightBasic", new()
            {
                { 1, new() { new(ItemID.HallowedBar, 15), new(ItemID.CrystalShard, 10), new(ItemID.PixieDust, 25), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.HallowedBar, 15), new(ItemID.AvengerEmblem, 1), new(ItemID.SoulofFright, 10), new(ItemID.UnicornHorn, 10), new(ItemID.PlatinumCoin, 1) } }
            });

            AddUncapGroup("Pirate", new()
            {
                { 1, new() { new(ItemID.GoldRing, 1), new(ItemID.LuckyCoin, 1), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.ChlorophyteBar, 10), new(ItemID.FlyingDutchmanTrophy, 1), new(ItemID.PlatinumCoin, 1) } }
            });

            // SORA NEXT UPD???
            AddUncapGroup("SORA", new()
            {
                { 1, new() { new(ItemID.Bacon, 5), new(ItemID.JojaCola, 10), new(ItemID.FroggleBunwich, 2), new(ItemID.GoldCoin, 50) } },
                { 2, new() { new(ItemID.MonsterLasagna, 3), new(ItemID.PumpkinPie, 3), new(ItemID.SeafoodDinner, 3), new(ItemID.GoldenDelight, 3), new(ItemID.PlatinumCoin, 1) } }
            });
        }

        private static void AddUncapGroup(string name, Dictionary<int, List<UncapRequirement>> requirements)
        {
            var group = new UncapGroup(name);
            foreach (var (level, reqs) in requirements)
            {
                group.AddRequirement(level, reqs);
            }

            UncapGroups[name] = group;
        }
    }

}
