#region
using db.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;
using wServer.realm.entities.player;
#endregion

namespace wServer.logic.loot
{
    public interface ILootDef
    {
        string Lootstate { get; set; }

        void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
            Random rand, string lootState, IList<LootDef> lootDefs);
    }
    public class GoldLoot : ILootDef
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LootState));
        public string Lootstate { get; set; }
        private readonly int maxGold;
        private readonly int minGold;

        public GoldLoot(int minGold, int maxGold)
        {
            this.maxGold = maxGold;
            this.minGold = minGold;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
            Random rand, string lootState, IList<LootDef> lootDefs)
        {
            if (playerDat == null)
                return;
            for (int i = 0; i < 3; i++)
                playerDat.Item1.Owner.BroadcastPacket(new ShowEffectPacket()
                {
                    EffectType = EffectType.Flow,
                    Color = new ARGB(0xccac00),
                    TargetId = playerDat.Item1.Id,
                    PosA = new Position() { X = enemy.X, Y = enemy.Y }
                }, null);
            manager.Database.DoActionAsync(db =>
            {
                playerDat.Item1.Credits =
                    playerDat.Item1.Client.Account.Credits =
                        db.UpdateCredit(playerDat.Item1.Client.Account,
                            rand.Next(minGold, maxGold + 1));
            });
            playerDat.Item1.UpdateCount++;
        }
    }
    public class ItemLoot : ILootDef
    {
        private readonly string item;
        private readonly double probability;

        public string Lootstate { get; set; }

        public ItemLoot(string item, double probability)
        {
            this.item = item;
            this.probability = probability;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
            Random rand, string lootState, IList<LootDef> lootDefs)
        {
            Lootstate = lootState;
            if (playerDat != null) return;
            XmlData dat = manager.GameData;
            lootDefs.Add(new LootDef(dat.Items[dat.IdToObjectType[item]], probability, lootState));
        }
    }

    public class LootState : ILootDef
    {
        private readonly ILootDef[] children;

        public string Lootstate { get; set; }

        public LootState(string subState, params ILootDef[] lootDefs)
        {
            children = lootDefs;
            Lootstate = subState;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, string lootState, IList<LootDef> lootDefs)
        {
            foreach (ILootDef i in children)
                i.Populate(manager, enemy, playerDat, rand, Lootstate, lootDefs);
        }
    }

    public enum ItemType
    {
        Weapon,
        Ability,
        Armor,
        Ring,
        Potion
    }

    public enum EggRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public class EggLoot : ILootDef
    {
        private readonly EggRarity rarity;
        private readonly double probability;

        public string Lootstate { get; set; }

        public EggLoot(EggRarity rarity, double probability)
        {
            this.probability = probability;
            this.rarity = rarity;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
            Random rand, string lootState, IList<LootDef> lootDefs)
        {
            Lootstate = lootState;
            if (playerDat != null) return;
            Item[] candidates = manager.GameData.Items
                .Where(item => item.Value.SlotType == 26)
                .Where(item => item.Value.Tier == (int)rarity)
                .Select(item => item.Value)
                .ToArray();
            foreach (Item i in candidates)
                lootDefs.Add(new LootDef(i, probability / candidates.Length, lootState));
        }
    }

    public class TierLoot : ILootDef
    {
        public static readonly int[] WeaponT = { 1, 2, 3, 8, 17, 24 };
        public static readonly int[] AbilityT = { 4, 5, 11, 12, 13, 15, 16, 18, 19, 20, 21, 22, 23, 25, 27, 29, 91 };
        public static readonly int[] ArmorT = { 6, 7, 14 };
        public static readonly int[] RingT = { 9 };
        public static readonly int[] PotionT = { 10 };
        private readonly double probability;

        private readonly byte tier;
        private readonly int[] types;

        public string Lootstate { get; set; }

        public TierLoot(byte tier, ItemType type, double probability)
        {
            this.tier = tier;
            switch (type)
            {
                case ItemType.Weapon:
                    types = WeaponT;
                    break;
                case ItemType.Ability:
                    types = AbilityT;
                    break;
                case ItemType.Armor:
                    types = ArmorT;
                    break;
                case ItemType.Ring:
                    types = RingT;
                    break;
                case ItemType.Potion:
                    types = PotionT;
                    break;
                default:
                    throw new NotSupportedException(type.ToString());
            }
            this.probability = probability;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
            Random rand, string lootState, IList<LootDef> lootDefs)
        {
            Lootstate = lootState;
            if (playerDat != null) return;
            Item[] candidates = manager.GameData.Items
                .Where(item => Array.IndexOf(types, item.Value.SlotType) != -1)
                .Where(item => item.Value.Tier == tier)
                .Select(item => item.Value)
                .ToArray();
            foreach (Item i in candidates)
                lootDefs.Add(new LootDef(i, probability / candidates.Length, lootState));
        }
    }

    public class Threshold : ILootDef
    {
        private readonly ILootDef[] children;
        private readonly double threshold;

        public string Lootstate { get; set; }

        public Threshold(double threshold, params ILootDef[] children)
        {
            this.threshold = threshold;
            this.children = children;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand,
            string lootState, IList<LootDef> lootDefs)
        {
            Lootstate = lootState;
            if (playerDat != null && playerDat.Item2 / enemy.ObjectDesc.MaxHP >= threshold)
            {
                foreach (ILootDef i in children)
                    i.Populate(manager, enemy, null, rand, lootState, lootDefs);
            }
        }
    }

    internal class MostDamagers : ILootDef
    {
        private readonly ILootDef[] loots;
        private readonly int amount;

        public MostDamagers(int amount, params ILootDef[] loots)
        {
            this.amount = amount;
            this.loots = loots;
        }

        public string Lootstate { get; set; }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, string lootState, IList<LootDef> lootDefs)
        {
            var data = enemy.DamageCounter.GetPlayerData();
            var mostDamage = GetMostDamage(data);
            foreach (var loot in mostDamage.Where(pl => pl.Equals(playerDat)).SelectMany(pl => loots))
                loot.Populate(manager, enemy, null, rand, lootState, lootDefs);
        }

        private IEnumerable<Tuple<Player, int>> GetMostDamage(IEnumerable<Tuple<Player, int>> data)
        {
            var damages = data.Select(_ => _.Item2).ToList();
            var len = damages.Count < amount ? damages.Count : amount;
            for (var i = 0; i < len; i++)
            {
                var val = damages.Max();
                yield return data.FirstOrDefault(_ => _.Item2 == val);
                damages.Remove(val);
            }
        }
    }

    public class OnlyOne : ILootDef
    {
        private readonly ILootDef[] loots;

        public OnlyOne(params ILootDef[] loots)
        {
            this.loots = loots;
        }

        public string Lootstate { get; set; }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, string lootState, IList<LootDef> lootDefs)
        {
            loots[rand.Next(0, loots.Length)].Populate(manager, enemy, playerDat, rand, lootState, lootDefs);
        }
    }

    public static class LootTemplates
    {
        public static ILootDef[] DefaultEggLoot(EggRarity maxRarity)
        {
            switch (maxRarity)
            {
                case EggRarity.Common:
                    return new ILootDef[1] { new EggLoot(EggRarity.Common, 0.0) };
                case EggRarity.Uncommon:
                    return new ILootDef[2] { new EggLoot(EggRarity.Common, 0.0), new EggLoot(EggRarity.Uncommon, 0.00) };
                case EggRarity.Rare:
                    return new ILootDef[3] { new EggLoot(EggRarity.Common, 0.0), new EggLoot(EggRarity.Uncommon, 0.00), new EggLoot(EggRarity.Rare, 0.0) };
                case EggRarity.Legendary:
                    return new ILootDef[4] { new EggLoot(EggRarity.Common, 0.0), new EggLoot(EggRarity.Uncommon, 0.00), new EggLoot(EggRarity.Rare, 0.0), new EggLoot(EggRarity.Legendary, 0.0) };
                default:
                    throw new InvalidOperationException("Not a valid Egg Rarity");
            }
        }

        public static ILootDef[] StatIncreasePotionsLoot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new ItemLoot("Potion of Defense", 0.2),
                    new ItemLoot("Potion of Attack", 0.2),
                    new ItemLoot("Potion of Speed", 0.2),
                    new ItemLoot("Potion of Vitality", 0.2),
                    new ItemLoot("Potion of Wisdom", 0.2),
                    new ItemLoot("Potion of Dexterity", 0.2)
                ),
                new OnlyOne(
                    new ItemLoot("Common Daily Quest Statue", 0.1),
                    new ItemLoot("UnCommon Daily Quest Statue", 0.075),
                    new ItemLoot("Rare Daily Quest Statue", 0.050),
                    new ItemLoot("Epic Daily Quest Statue", 0.025)
                )
            };
        }

        public static ILootDef[] DailyToken()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new ItemLoot("Common Daily Quest Statue", 0.1),
                    new ItemLoot("UnCommon Daily Quest Statue", 0.075),
                    new ItemLoot("Rare Daily Quest Statue", 0.050),
                    new ItemLoot("Epic Daily Quest Statue", 0.025)
                )
            };
        }

        public static ILootDef[] Tier1Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(1, ItemType.Weapon, 0.5),
                    new TierLoot(1, ItemType.Ability, 0.5),
                    new TierLoot(1, ItemType.Armor, 0.5),
                    new TierLoot(1, ItemType.Ring, 0.5)
                )
            };
        }

        public static ILootDef[] Tier2Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(2, ItemType.Weapon, 0.5),
                    new TierLoot(1, ItemType.Ability, 0.5),
                    new TierLoot(2, ItemType.Armor, 0.5),
                    new TierLoot(1, ItemType.Ring, 0.5)
                )
            };
        }

        public static ILootDef[] Tier3Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(3, ItemType.Weapon, 0.5),
                    new TierLoot(2, ItemType.Ability, 0.5),
                    new TierLoot(3, ItemType.Armor, 0.5),
                    new TierLoot(2, ItemType.Ring, 0.5)
                )
            };
        }

        public static ILootDef[] Tier4Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(4, ItemType.Weapon, 0.3),
                    new TierLoot(2, ItemType.Ability, 0.3),
                    new TierLoot(4, ItemType.Armor, 0.3),
                    new TierLoot(2, ItemType.Ring, 0.3)
                )
            };
        }

        public static ILootDef[] Tier5Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(5, ItemType.Weapon, 0.3),
                    new TierLoot(2, ItemType.Ability, 0.3),
                    new TierLoot(5, ItemType.Armor, 0.3),
                    new TierLoot(2, ItemType.Ring, 0.3)
                )
            };
        }

        public static ILootDef[] Tier6Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(6, ItemType.Weapon, 0.3),
                    new TierLoot(3, ItemType.Ability, 0.3),
                    new TierLoot(6, ItemType.Armor, 0.3),
                    new TierLoot(3, ItemType.Ring, 0.3)
                )
            };
        }

        public static ILootDef[] Tier7Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(7, ItemType.Weapon, 0.2),
                    new TierLoot(3, ItemType.Ability, 0.2),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(3, ItemType.Ring, 0.2)
                )
            };
        }

        public static ILootDef[] Tier8Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(3, ItemType.Ability, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.2),
                    new TierLoot(3, ItemType.Ring, 0.2)
                )
            };
        }

        public static ILootDef[] Tier9Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(9, ItemType.Weapon, 0.2),
                    new TierLoot(4, ItemType.Ability, 0.2),
                    new TierLoot(9, ItemType.Armor, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.2)
                )
            };
        }

        public static ILootDef[] Tier10Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(10, ItemType.Weapon, 0.15),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(10, ItemType.Armor, 0.15),
                    new TierLoot(4, ItemType.Ring, 0.15)
                )
            };
        }

        public static ILootDef[] Tier11Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(11, ItemType.Weapon, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.15),
                    new TierLoot(5, ItemType.Ring, 0.15)
                )
            };
        }

        public static ILootDef[] Tier12Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(12, ItemType.Weapon, 0.15),
                    new TierLoot(6, ItemType.Ability, 0.15),
                    new TierLoot(12, ItemType.Armor, 0.15),
                    new TierLoot(5, ItemType.Ring, 0.15)
                )
            };
        }

        public static ILootDef[] Tier13Loot()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new TierLoot(12, ItemType.Weapon, 0.1),
                    new TierLoot(6, ItemType.Ability, 0.1),
                    new TierLoot(13, ItemType.Armor, 0.1),
                    new TierLoot(6, ItemType.Ring, 0.1)
                )
            };
        }
    }
}