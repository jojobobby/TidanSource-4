#region

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.logic;
using wServer.networking;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;

#endregion

namespace wServer.realm.entities.player
{
    internal interface IPlayer
    {
        void Damage(int dmg, Entity chr);
        bool IsVisibleToEnemy();


    }

    public static class ComparableExtension
    {
        public static bool InRange<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(from) >= 1 && value.CompareTo(to) <= -1;
        }
    }

    public partial class Player : Character, IContainer, IPlayer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Player));

        private bool dying;

        private Item[] inventory;

        private float hpRegenCounter;
        private float mpRegenCounter;
        private bool resurrecting;
        public bool PvP { get; set; }
        public bool isNotVisible = false;

        private byte[,] tiles;
        private int pingSerial;
        private SetTypeSkin setTypeSkin;

        public int ShotsPerSecond;
        public int ShotsPerSecondTime;

        public Player(RealmManager manager, Client psr)
                   : base(manager, (ushort)psr.Character.ObjectType, psr.Random)
        {
            try
            {
                Client = psr;
                Manager = psr.Manager;
                StatsManager = new StatsManager(this, psr.Random.CurrentSeed);
                Name = psr.Account.Name;
                AccountId = psr.Account.AccountId;
                FameCounter = new FameCounter(this);
                Tokens = psr.Account.FortuneTokens;
                HpPotionPrice = 5;
                MpPotionPrice = 5;

                Level = psr.Character.Level == 0 ? 1 : psr.Character.Level;
                Experience = psr.Character.Exp;
                ExperienceGoal = GetExpGoal(Level);
                Stars = GetStars();
                Texture1 = psr.Character.Tex1;
                Texture2 = psr.Character.Tex2;
                Credits = psr.Account.Credits;
                NameChosen = psr.Account.NameChosen;
                CurrentFame = psr.Account.Stats.Fame;
                Fame = psr.Character.CurrentFame;
                XpBoosted = psr.Character.XpBoosted;
                XpBoostTimeLeft = psr.Character.XpTimer;
                xpFreeTimer = XpBoostTimeLeft != -1.0;
                LootDropBoostTimeLeft = psr.Character.LDTimer;
                lootDropBoostFreeTimer = LootDropBoost;
                LootTierBoostTimeLeft = psr.Character.LTTimer;
                lootTierBoostFreeTimer = LootTierBoost;
                var state =
                    psr.Account.Stats.ClassStates.SingleOrDefault(_ => Utils.FromString(_.ObjectType) == ObjectType);
                FameGoal = GetFameGoal(state?.BestFame ?? 0);
                Glowing = IsUserInLegends();
                Guild = GuildManager.Add(this, psr.Account.Guild);
                HP = psr.Character.HitPoints <= 0 ? psr.Character.MaxHitPoints : psr.Character.HitPoints;
                Mp = psr.Character.MagicPoints;
                ConditionEffects = 0;
                OxygenBar = 100;
                HasBackpack = psr.Character.HasBackpack == 1;
                PlayerSkin = Client.Account.OwnedSkins.Contains(Client.Character.Skin) ? Client.Character.Skin : 0;
                HealthPotions = psr.Character.HealthStackCount < 0 ? 0 : psr.Character.HealthStackCount;
                MagicPotions = psr.Character.MagicStackCount < 0 ? 0 : psr.Character.MagicStackCount;

                Locked = psr.Account.Locked ?? new List<string>();
                Ignored = psr.Account.Ignored ?? new List<string>();
                try
                {
                    Manager.Database.DoActionAsync(db =>
                    {
                        Locked = db.GetLockeds(AccountId);
                        Ignored = db.GetIgnoreds(AccountId);
                        Muted = db.IsMuted(AccountId);
                        DailyQuest = psr.Account.DailyQuest;
                    });
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

                if (HasBackpack)
                {
                    var inv =
                        psr.Character.Equipment.Select(
                            _ =>
                                _ == -1
                                    ? null
                                    : (Manager.GameData.Items.ContainsKey((ushort)_) ? Manager.GameData.Items[(ushort)_] : null))
                            .ToArray();
                    var backpack =
                        psr.Character.Backpack.Select(
                            _ =>
                                _ == -1
                                    ? null
                                    : (Manager.GameData.Items.ContainsKey((ushort)_) ? Manager.GameData.Items[(ushort)_] : null))
                            .ToArray();

                    Inventory = inv.Concat(backpack).ToArray();
                    var xElement = Manager.GameData.ObjectTypeToElement[ObjectType].Element("SlotTypes");
                    if (xElement != null)
                    {
                        var slotTypes =
                            Utils.FromCommaSepString32(
                                xElement.Value);
                        Array.Resize(ref slotTypes, 20);
                        SlotTypes = slotTypes;
                    }
                }
                else
                {
                    Inventory =
                        psr.Character.Equipment.Select(
                            _ =>
                                _ == -1
                                    ? null
                                    : (Manager.GameData.Items.ContainsKey((ushort)_) ? Manager.GameData.Items[(ushort)_] : null))
                            .ToArray();
                    var xElement = Manager.GameData.ObjectTypeToElement[ObjectType].Element("SlotTypes");
                    if (xElement != null)
                        SlotTypes =
                            Utils.FromCommaSepString32(
                                xElement.Value);
                }

                Stats = new[]
                {
                    psr.Character.MaxHitPoints,
                    psr.Character.MaxMagicPoints,
                    psr.Character.Attack,
                    psr.Character.Defense,
                    psr.Character.Speed,
                    psr.Character.HpRegen,
                    psr.Character.MpRegen,
                    psr.Character.Dexterity
                };

                Pet = null;

                for (var i = 0; i < SlotTypes.Length; i++)
                    if (SlotTypes[i] == 0) SlotTypes[i] = 10;

                if (Client.Account.Rank >= 3) return;
                for (var i = 0; i < 4; i++)
                    if (Inventory[i]?.SlotType != SlotTypes[i])
                        Inventory[i] = null;
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        private bool CheckLE1Ability()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.LE1Ability)
                    continue;
                return true;
            }
            return false;
        }

        private bool CheckHalfHPArmored()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HalfHPArmored)
                    continue;
                return true;
            }
            return false;
        }
        //CheckHPBoostMp()

        private bool CheckHPBoostMp()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HPBoostMp)
                    continue;
                return true;
            }
            return false;
        }
        private bool CheckTeleportLowHp()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.TeleportLowHp)
                    continue;
                return true;
            }
            return false;
        }

        private bool CheckHealItem()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HealItem)
                    continue;
                return true;
            }
            return false;
        }

        private bool CheckStunImmune()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.StunImmune)
                    continue;
                return true;
            }
            return false;
        }

        public int HPBERSERKTRUEORFALSE = 0;

        private bool CheckHalfBerserk()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HalfHPBerserk)
                    continue;
                return true;

            }
            return false;

        }

        private bool CheckHpBelowonet3()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HpBelowonet3)
                    continue;
                return true;
            }
            return false;
        }//CheckBonusStatsLGSoft()
       
        private bool CheckHPabovehalf()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HPabovehalf)
                    continue;
                return true;
            }
            return false;
        }
        private bool CheckHealth24()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.Health24)
                    continue;
                return true;
            }
            return false;
        }

        private bool CheckBurstDamage()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.BurstDamage)
                    continue;
                return true;
            }
            return false;
        }
        private bool CheckMPabovehalf()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.MPabovehalf)
                    continue;
                return true;
            }
            return false;
        }


        private bool CheckNearbyDefBoost()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.NearbyDefBoost)
                    continue;
                return true;
            }
            return false;
        }
		private bool CheckHealer()
		{
			for (var i = 0; i < 4; i++)
			{
				var item = Inventory[i];
				if (item == null || !item.Healer)
					continue;
				return true;
			}
			return false;
		}
		private bool CheckObliterator()
		{
			for (var i = 0; i < 4; i++)
			{
				var item = Inventory[i];
				if (item == null || !item.Obliterator)
					continue;
				return true;
			}
			return false;
		}
		private bool CheckHalfMPAImmune()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.HalfMPAImmune)
                    continue;
                return true;
            }
            return false;
        }
        private bool CheckImmune2Dazed()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.Immune2Dazed)
                    continue;
                return true;
            }
            return false;
        }

        private bool CheckImmunePara()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.ImmunePara)
                    continue;
                return true;
            }
            return false;
        }

        ~Player()
        {
            WorldInstance = null;
            Quest = null;
        }



        //Stats
        public string AccountId { get; }

        public int[] Boost { get; private set; }

        public Client Client { get; }

        public int Credits { get; set; }
        public int Tokens { get; set; }
        public int CurrentFame { get; set; }

        public int Experience { get; set; }
        public int ExperienceGoal { get; set; }

        public int Fame { get; set; }

        public FameCounter FameCounter { get; }

        public QuestItem DailyQuest { get; set; }

        public int FameGoal { get; set; }

        public bool Glowing { get; set; }

        public bool itema;

        public bool itemb;

        public bool itemc;

        public bool itemd;

        public bool iteme;

        public bool itemf;

        public bool itemPer;

        public bool itemLE1;

        public bool itemg;
        public bool HasBackpack { get; set; }

        public int HealthPotions { get; set; }

        public List<string> Ignored { get; set; }

        public bool Invited { get; set; }
        public bool Muted { get; set; }

        public int Level { get; set; }

        public List<string> Locked { get; set; }

        public bool LootDropBoost
        {
            get { return LootDropBoostTimeLeft > 0; }
            set { LootDropBoostTimeLeft = value ? LootDropBoostTimeLeft : 0.0f; }
        }
        public float LootDropBoostTimeLeft { get; set; }

        public bool LootTierBoost
        {
            get { return LootTierBoostTimeLeft > 0; }
            set { LootTierBoostTimeLeft = value ? LootTierBoostTimeLeft : 0.0f; }
        }
        public float LootTierBoostTimeLeft { get; set; }

        public bool XpBoosted { get; set; }
        public float XpBoostTimeLeft { get; set; }

        public int MagicPotions { get; set; }

        public ushort HpPotionPrice { get; set; }
        public ushort MpPotionPrice { get; set; }

        public bool HpFirstPurchaseTime { get; set; }
        public bool MpFirstPurchaseTime { get; set; }

        public new RealmManager Manager { get; }

        public int MaxHp { get; set; }

        public int MaxMp { get; set; }

        public int Mp { get; set; }

        public bool NameChosen { get; set; }

        public int OxygenBar { get; set; }

        public Pet Pet { get; set; }

        public int PlayerSkin { get; set; }

        public int Stars { get; set; }

        public int[] Stats { get; }

        public StatsManager StatsManager { get; }

        public int Texture1 { get; set; }

        public int Texture2 { get; set; }

        public Item[] Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public GuildManager Guild { get; set; }

        public int[] SlotTypes { get; set; }

        public void Damage(int dmg, Entity chr)
        {
            try
            {
                if (HasConditionEffect(ConditionEffectIndex.Paused) ||
                    HasConditionEffect(ConditionEffectIndex.Stasis) ||
                    HasConditionEffect(ConditionEffectIndex.Invincible))
                    return;

                dmg = (int)StatsManager.GetDefenseDamage(dmg, false);
                if (!HasConditionEffect(ConditionEffectIndex.Invulnerable))
                    HP -= dmg;
                UpdateCount++;
                Owner.BroadcastPacket(new DamagePacket
                {
                    TargetId = Id,
                    Effects = 0,
                    Damage = (ushort)dmg,
                    Killed = HP <= 0,
                    BulletId = 0,
                    ObjectId = chr.Id
                }, this);
                SaveToCharacter();

                if (CheckHealItem() == true)
                {
                    itemf = true;

                    if (HP == HP)
                    {
                        ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Healing,
                            DurationMS = 1000 * Stats[6] / 25
                        });

                    }
                    if (HP != HP)
                    {
                        ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Healing,
                            DurationMS = 0
                        });
                        itemf = false;
                    }

                }
                else if (itemf == true && CheckHealItem() == false)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Healing,
                        DurationMS = 0
                    });
                    itemf = false;
                }

                if (CheckBurstDamage() == true)
                {
                    itemg = true;

                    if (Mp == Mp)
                    {
                        ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Damaging,
                            DurationMS = 2000
                        });

                    }
                    if (Mp != Mp)
                    {
                        ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Damaging,
                            DurationMS = 0
                        });
                        itemg = false;
                    }

                }
                else if (itemg == true && CheckBurstDamage() == false)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Damaging,
                        DurationMS = 0
                    });
                    itemg = false;
                }






                if (HP <= 0)
                    Death(chr.ObjectDesc.DisplayId, chr.ObjectDesc);

            }
            catch (Exception e)
            {
                log.Error("Error while processing playerDamage: ", e);
            }
        }




        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            base.ExportStats(stats);
            stats[StatsType.AccountId] = AccountId;
            stats[StatsType.Name] = Name;

            stats[StatsType.Experience] = Experience - GetLevelExp(Level);
            stats[StatsType.ExperienceGoal] = ExperienceGoal;
            stats[StatsType.Level] = Level;

            stats[StatsType.CurrentFame] = CurrentFame;
            stats[StatsType.Fame] = Fame;
            stats[StatsType.FameGoal] = FameGoal;
            stats[StatsType.Stars] = Stars;

            stats[StatsType.Guild] = Guild[AccountId].Name;
            stats[StatsType.GuildRank] = Guild[AccountId].Rank;

            stats[StatsType.Credits] = Credits;
            stats[StatsType.Tokens] = Tokens;
            stats[StatsType.NameChosen] = NameChosen ? 1 : 0;
            stats[StatsType.Texture1] = Texture1;
            stats[StatsType.Texture2] = Texture2;

            if (Glowing)
                stats[StatsType.Glowing] = 1;
			if (Client.Account.Rank >= 2)
				stats[StatsType.GLOW_COLOR_STAT] = new Random().Next(0x000000, 0xffffff);
			stats[StatsType.HP] = HP;// prob wont work but
            stats[StatsType.MP] = Mp;

            stats[StatsType.Inventory0] = (int)(Inventory[0]?.ObjectType ?? -1);
            stats[StatsType.Inventory1] = (int)(Inventory[1]?.ObjectType ?? -1);
            stats[StatsType.Inventory2] = (int)(Inventory[2]?.ObjectType ?? -1);
            stats[StatsType.Inventory3] = (int)(Inventory[3]?.ObjectType ?? -1);
            stats[StatsType.Inventory4] = (int)(Inventory[4]?.ObjectType ?? -1);
            stats[StatsType.Inventory5] = (int)(Inventory[5]?.ObjectType ?? -1);
            stats[StatsType.Inventory6] = (int)(Inventory[6]?.ObjectType ?? -1);
            stats[StatsType.Inventory7] = (int)(Inventory[7]?.ObjectType ?? -1);
            stats[StatsType.Inventory8] = (int)(Inventory[8]?.ObjectType ?? -1);
            stats[StatsType.Inventory9] = (int)(Inventory[9]?.ObjectType ?? -1);
            stats[StatsType.Inventory10] = (int)(Inventory[10]?.ObjectType ?? -1);
            stats[StatsType.Inventory11] = (int)(Inventory[11]?.ObjectType ?? -1);

            if (Boost == null) CalcBoost();
            //yeah, thats what i was go
            if (Boost != null)
            {
                stats[StatsType.MaximumHP] = Stats[0] + Boost[0] + LE1Ability();
                stats[StatsType.MaximumMP] = Stats[1] + Boost[1] + LE1Ability();
                stats[StatsType.Attack] = Stats[2] + Boost[2] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability() + TeleportLowHp() ;
                stats[StatsType.Defense] = Stats[3] + NearbyDefBoost() + Boost[3] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.Speed] = Stats[4] + Boost[4] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.Vitality] = Stats[5] + Health24() + Boost[5] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability() + HPBoostMp();
                stats[StatsType.Wisdom] = Stats[6] + MPabovehalf() + Boost[6] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.Dexterity] = Stats[7] + HPabovehalf() + Boost[7] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();

                stats[StatsType.HPBoost] = Boost[0] + LE1Ability();                                                                   // see now i wanted to make it be added into boost[0] at first but i couldn't rap my head aroudn ti
                stats[StatsType.MPBoost] = Boost[1] + LE1Ability();
                stats[StatsType.AttackBonus] = Boost[2] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability() + TeleportLowHp();
                stats[StatsType.DefenseBonus] = Boost[3] + NearbyDefBoost() + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.SpeedBonus] = Boost[4] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.VitalityBonus] = Boost[5] + Health24() + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability() + HPBoostMp();
                stats[StatsType.WisdomBonus] = MPabovehalf() + Boost[6] + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
                stats[StatsType.DexterityBonus] = Boost[7] + HPabovehalf() + Adding1() + Adding2() + Adding3() + Adding4() + Adding5() + Adding6() + LE1Ability();
            }

            stats[StatsType.Size] = setTypeSkin?.Size ?? Size;
            stats[StatsType.Has_Backpack] = HasBackpack.GetHashCode();

            stats[StatsType.Backpack0] = (int)(HasBackpack ? (Inventory[12]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack1] = (int)(HasBackpack ? (Inventory[13]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack2] = (int)(HasBackpack ? (Inventory[14]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack3] = (int)(HasBackpack ? (Inventory[15]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack4] = (int)(HasBackpack ? (Inventory[16]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack5] = (int)(HasBackpack ? (Inventory[17]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack6] = (int)(HasBackpack ? (Inventory[18]?.ObjectType ?? -1) : -1);
            stats[StatsType.Backpack7] = (int)(HasBackpack ? (Inventory[19]?.ObjectType ?? -1) : -1);

            stats[StatsType.Skin] = setTypeSkin?.SkinType ?? PlayerSkin;
            stats[StatsType.HealStackCount] = HealthPotions;
            stats[StatsType.MagicStackCount] = MagicPotions;

            if (Owner != null && Owner.Name == "Ocean Trench")
                stats[StatsType.OxygenBar] = OxygenBar;

            stats[StatsType.XpBoosterActive] = XpBoosted ? 1 : 0;
            stats[StatsType.XpBoosterTime] = (int)XpBoostTimeLeft;
            stats[StatsType.LootDropBoostTimer] = (int)LootDropBoostTimeLeft;
            stats[StatsType.LootTierBoostTimer] = (int)LootTierBoostTimeLeft;
        }

        public void CalcBoost()
        {
            CheckSetTypeSkin();
            if (Boost == null) Boost = new int[12];
            else
                for (var i = 0; i < Boost.Length; i++) Boost[i] = 0;
            for (var i = 0; i < 4; i++)
            {
                if (Inventory.Length < i || Inventory.Length == 0) return;
                if (Inventory[i] == null) continue;
                foreach (var pair in Inventory[i].StatsBoost)
                {
                    if (pair.Key == StatsType.MaximumHP) Boost[0] += pair.Value;
                    if (pair.Key == StatsType.MaximumMP) Boost[1] += pair.Value;
                    if (pair.Key == StatsType.Attack) Boost[2] += pair.Value;
                    if (pair.Key == StatsType.Defense) Boost[3] += pair.Value;
                    if (pair.Key == StatsType.Speed) Boost[4] += pair.Value;
                    if (pair.Key == StatsType.Vitality) Boost[5] += pair.Value;
                    if (pair.Key == StatsType.Wisdom) Boost[6] += pair.Value;
                    if (pair.Key == StatsType.Dexterity) Boost[7] += pair.Value;
                }
            }

            if (setTypeBoosts == null) return;
            for (var i = 0; i < 8; i++)
                Boost[i] += setTypeBoosts[i];
        }

        public bool CompareName(string name)
        {
            var rn = name.ToLower();
            return rn.Split(' ')[0].StartsWith("[") || Name.Split(' ').Length == 1
                ? Name.ToLower().StartsWith(rn)
                : Name.Split(' ')[1].ToLower().StartsWith(rn);
        }

        public void Death(string killer, ObjectDesc desc = null)
        {
            if (dying) return;
            dying = true;
            switch (Owner.Name)
            {
                case "Arena":
                    {
                        Client.SendPacket(new ArenaDeathPacket
                        {
                            RestartPrice = 100
                        });
                        HP = Client.Character.MaxHitPoints;
                        ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Invulnerable,
                            DurationMS = -1
                        });
                        return;
                    }
                case "Nexus":
                    foreach (var i in Manager.Worlds.Values)
                        foreach (var e in i.Players.Values)
                            e.SendInfo(Name + " cannot be killed in nexus!");
                    return;
            }

            if (Client.Stage == ProtocalStage.Disconnected || resurrecting)
                return;
            if (CheckResurrection())
                return;

            if (Client.Character.Dead)
            {
                Client.Disconnect();
                return;
            }
            GenerateGravestone();
            AnnounceDeath(killer);

            if (desc != null)
                killer = desc.DisplayId;

            try
            {
                Manager.Database.DoActionAsync(db =>
                {
                    Client.Character.Dead = true;
                    SaveToCharacter();
                    db.SaveCharacter(Client.Account, Client.Character);
                    db.Death(Manager.GameData, Client.Account, Client.Character, killer);
                });
                if (Owner.Id != -6)
                {
                    Client.SendPacket(new DeathPacket
                    {
                        AccountId = AccountId,
                        CharId = Client.Character.CharacterId,
                        Killer = killer,
                        obf0 = -1,
                        obf1 = -1,
                    });
                    Owner.Timers.Add(new WorldTimer(1000, (w, t) => Client.Disconnect()));
                    Owner.LeaveWorld(this);
                }
                else
                    Client.Disconnect();
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

		private void AnnounceDeath(string killer)
		{
			var playerDesc = Manager.GameData.ObjectDescs[ObjectType];
			var maxed = 0;
			var accid = Manager.FindPlayer(Name);
			var charid = Client.Character.CharacterId;
			if (Stats[0] == playerDesc.MaxHitPoints) maxed++;
			if (Stats[1] == playerDesc.MaxMagicPoints) maxed++;
			if (Stats[2] == playerDesc.MaxAttack) maxed++;
			if (Stats[3] == playerDesc.MaxDefense) maxed++;
			if (Stats[4] == playerDesc.MaxSpeed) maxed++;
			if (Stats[5] == playerDesc.MaxHpRegen) maxed++;
			if (Stats[6] == playerDesc.MaxMpRegen) maxed++;
			if (Stats[7] == playerDesc.MaxDexterity) maxed++;

			var notification = $"{Name} died to {killer} as {maxed}/8 {playerDesc.ObjectId.ToLower()} with {Fame} fame base. (Acc ID: {accid.AccountId}, Char ID:{charid})";

			if (Fame >= 2000 && !Client.Account.Admin)
			{
				foreach (var w in Manager.Worlds.Values)
					foreach (var p in w.Players.Values)
						p.SendError(notification);
				return;
			}
			else
				foreach (var i in Owner.Players.Values)
				{
					{
						i.SendError(notification);
					}
				}
		}

		public void GivePet(PetItem petInfo)
        {
            //if (Name == "ossimc82" || Name == "C453")
            //{
            Pet = new Pet(Manager, petInfo, this);
            Pet.Move(X, Y);
            Owner.EnterWorld(Pet);
            //}
        }
        public void Damage(int dmg, Entity chr, bool noDef, bool toSelf = false, float pvpReduction = 0.20f)
        {
            try
            {
                if (HasConditionEffect(ConditionEffects.Paused) ||
                    HasConditionEffect(ConditionEffects.Stasis) ||
                    HasConditionEffect(ConditionEffects.Invincible))
                    return;

                dmg = (int)StatsManager.GetDefenseDamage(dmg, noDef);
                if (chr is Player)
                    dmg = Math.Max(1, (int)(dmg * pvpReduction));
                if (!HasConditionEffect(ConditionEffects.Invulnerable))
                    HP -= dmg;
                UpdateCount++;
                Owner.BroadcastPacket(new DamagePacket
                {
                    TargetId = Id,
                    Effects = 0,
                    Damage = (ushort)dmg,
                    Killed = HP <= 0,
                    BulletId = 0,
                    ObjectId = chr != null ? chr.Id : -1
                }, toSelf ? null : this);
                SaveToCharacter();

                string killerName = chr is Player
                    ? chr.Name
                    : chr != null ? (chr.ObjectDesc.DisplayId ?? chr.ObjectDesc.ObjectId) : "Unknown";

                if (HP <= 0)
                    Death(killerName);//i feel u
            }
            catch (Exception e)
            {
                log.Error("Error while processing playerDamage: ", e);
            }
        }
        public override bool HitByProjectile(Projectile projectile, RealmTime time)
        {
            if (projectile.ProjectileOwner is Player ||
                HasConditionEffect(ConditionEffectIndex.Paused) ||
                HasConditionEffect(ConditionEffectIndex.Stasis) ||
                HasConditionEffect(ConditionEffectIndex.Invincible))
                return false;

            return base.HitByProjectile(projectile, time);
        }



        public Dictionary<int, string> RankToPrefix = new Dictionary<int, string>()
        {
                  //normal
                  { 0, "" },
                  { 1, "YT" },
                  { 2, "Sp#1" },
                  { 3, "Sp#2" },
                  { 4, "Sp#3" },
                  { 5, "Staff" },
                  { 6, "Admin" },
                  { 7, "Co-Dev" },
                  { 8, "Owner" }
        };



        public override void Init(World owner)
        {
            WorldInstance = owner;
            var rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(0, owner.Map.Width);
                y = rand.Next(0, owner.Map.Height);
            } while (owner.Map[x, y].Region != TileRegion.Spawn);
            Move(x + 0.5f, y + 0.5f);
            tiles = new byte[owner.Map.Width, owner.Map.Height];
            SetNewbiePeriod();
            base.Init(owner);

            if (Client.Character.Pet != null)//kinda forgot where i was ay
                GivePet(Client.Character.Pet);

            if (owner.Id == World.NEXUS_ID || owner.Name == "Vault")
            {
                Client.SendPacket(new Global_NotificationPacket
                {
                    Type = 0,
                    Text = Client.Account.Gifts.Count > 0 ? "giftChestOccupied" : "giftChestEmpty"
                });
            }

            SendAccountList(Locked, AccountListPacket.LOCKED_LIST_ID);
            SendAccountList(Ignored, AccountListPacket.IGNORED_LIST_ID);

            WorldTimer[] accTimer = { null };
            owner.Timers.Add(accTimer[0] = new WorldTimer(5000, (w, t) =>
            {
                Manager.Database.DoActionAsync(db =>
                {
                    if (Client?.Account == null) return;
                    Client.Account = db.GetAccount(AccountId, Manager.GameData);
                    Credits = Client.Account.Credits;
                    CurrentFame = Client.Account.Stats.Fame;
                    Tokens = Client.Account.FortuneTokens;
                    accTimer[0].Reset();
                    Manager.Logic.AddPendingAction(_ => w.Timers.Add(accTimer[0]), PendingPriority.Creation);
                });
            }));

            WorldTimer[] pingTimer = { null };
            owner.Timers.Add(pingTimer[0] = new WorldTimer(PING_PERIOD, (w, t) =>
            {
                Client.SendPacket(new PingPacket { Serial = pingSerial++ });
                pingTimer[0].Reset();
                Manager.Logic.AddPendingAction(_ => w.Timers.Add(pingTimer[0]), PendingPriority.Creation);
            }));
            Manager.Database.DoActionAsync(db =>
            {
                db.UpdateLastSeen(Client.Account.AccountId, Client.Character.CharacterId, owner.Name);
                db.LockAccount(Client.Account);
            });

            if (Client.Account.IsGuestAccount)
            {
                owner.Timers.Add(new WorldTimer(1000, (w, t) => Client.Disconnect()));
                Client.SendPacket(new networking.svrPackets.FailurePacket
                {
                    ErrorId = 8,
                    ErrorDescription = "Registration needed."
                });
                Client.SendPacket(new PasswordPromtPacket
                {
                    CleanPasswordStatus = PasswordPromtPacket.REGISTER
                });
                return;
            }

            if (!Client.Account.VerifiedEmail && Program.Verify)
            {
                Client.SendPacket(new VerifyEmailDialogPacket());
                owner.Timers.Add(new WorldTimer(1000, (w, t) => Client.Disconnect()));
                return;
            }
            CheckSetTypeSkin();
        }

        public void SaveToCharacter()
        {
            var chr = Client.Character;
            chr.Exp = Experience;
            chr.Level = Level;
            chr.Tex1 = Texture1;
            chr.Tex2 = Texture2;
            chr.Pet = Pet?.Info;
            chr.CurrentFame = Fame;
            chr.HitPoints = HP;
            chr.MagicPoints = Mp;
            switch (Inventory.Length)
            {
                case 12:
                    chr.Equipment = Inventory.Select(_ => _?.ObjectType ?? -1).ToArray();
                    break;
                case 20:
                    var equip = Inventory.Select(_ => _?.ObjectType ?? -1).ToArray();
                    var backpack = new int[8];
                    Array.Copy(equip, 12, backpack, 0, 8);
                    Array.Resize(ref equip, 12);
                    chr.Equipment = equip;
                    chr.Backpack = backpack;
                    break;
            }
            chr.MaxHitPoints = Stats[0];
            chr.MaxMagicPoints = Stats[1];
            chr.Attack = Stats[2];
            chr.Defense = Stats[3];
            chr.Speed = Stats[4];
            chr.HpRegen = Stats[5];
            chr.MpRegen = Stats[6];
            chr.Dexterity = Stats[7];
            chr.HealthStackCount = HealthPotions;
            chr.MagicStackCount = MagicPotions;
            chr.HasBackpack = HasBackpack.GetHashCode();
            chr.Skin = PlayerSkin;
            chr.XpBoosted = XpBoosted;
            chr.XpTimer = (int)XpBoostTimeLeft;
            chr.LDTimer = (int)LootDropBoostTimeLeft;
            chr.LTTimer = (int)LootTierBoostTimeLeft;
        }

        public void Teleport(RealmTime time, TeleportPacket packet)
        {
            var obj = Client.Player.Owner.GetEntity(packet.ObjectId);
            try
            {
                if (obj == null) return;
                if (!TPCooledDown())
                {
                    SendError("Player.teleportCoolDown");
                    return;
                }
                if (obj.HasConditionEffect(ConditionEffectIndex.Paused))
                {
                    SendError("server.no_teleport_to_paused");
                    return;
                }
                var player = obj as Player;
                if (player != null && !player.NameChosen)
                {
                    SendError("server.teleport_needs_name");
                    return;
                }
                if (obj.Id == Id)
                {
                    SendError("server.teleport_to_self");
                    return;
                }
                if (!Owner.AllowTeleport)
                {
                    SendError(GetLanguageString("server.no_teleport_in_realm", new KeyValuePair<string, object>("realm", Owner.Name)));
                    return;
                }

             


                SetTPDisabledPeriod();
                Move(obj.X, obj.Y);
                Pet?.Move(obj.X, obj.X);
                FameCounter.Teleport();
                SetNewbiePeriod();
                UpdateCount++;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                SendError("player.cannotTeleportTo");
                return;
            }
            Owner.BroadcastPacket(new GotoPacket
            {
                ObjectId = Id,
                Position = new Position
                {
                    X = X,
                    Y = Y
                }
            }, null);
            if (!isNotVisible)
                Owner.BroadcastPacket(new ShowEffectPacket
                {
                    EffectType = EffectType.Teleport,
                    TargetId = Id,
                    PosA = new Position
                    {
                        X = X,
                        Y = Y
                    },
                    Color = new ARGB(0xFFFFFFFF)
                }, null);
        }


        public override void Tick(RealmTime time)
        {
            try
            {
                if (Manager.Clients.Count(_ => _.Value.Id == Client.Id) == 0)
                {
                    if (Owner != null)
                        Owner.LeaveWorld(this);
                    else
                        WorldInstance.LeaveWorld(this);
                    Manager.Database.DoActionAsync(db => db.UnlockAccount(Client.Account));
                    return;
                }
                if (Client.Stage == ProtocalStage.Disconnected || (!Client.Account.VerifiedEmail && Program.Verify))
                {
                    if (Owner != null)
                        Owner.LeaveWorld(this);
                    else
                        WorldInstance.LeaveWorld(this);
                    Manager.Database.DoActionAsync(db => db.UnlockAccount(Client.Account));
                    return;
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            if (Stats != null && Boost != null)
            {
                MaxHp = Stats[0] + Boost[0];
                MaxMp = Stats[1] + Boost[1];
            }



            if (!KeepAlive(time)) return;

            if (Boost == null) CalcBoost();

            TradeHandler?.Tick(time);
            HandleRegen(time);
            HandleQuest(time);
            HandleEffects(time);
            HandleGround(time);
            HandleBoosts();

            FameCounter.Tick(time);

            //if(pingSerial > 5)
            //    if (!Enumerable.Range(UpdatesSend, 5000).Contains(UpdatesReceived))
            //        Client.Disconnect();

            if (Mp < 0) Mp = 0;

            /* try
                * {
                *     psr.Database.SaveCharacter(psr.Account, psr.Character);
                *     UpdateCount++;
                * }
                * catch (ex)
                * {
                * }
            */

          



            if (CheckHalfHPArmored() == true)
            {
                itemPer = true;

                if (HP <= MaxHp / 4)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Healing,
                        DurationMS = 1000 * Stats[6] / 25
                    });

                }
                if (HP >= MaxHp / 4)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Healing,
                        DurationMS = 0
                    });
                    itemPer = false;
                }

            }
            else if (itemPer == true && CheckHalfHPArmored() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Healing,
                    DurationMS = 0
                });
                itemPer = false;
            }




            try
            {
                if (Owner != null)
                {
                    SendUpdate(time);
                    if (!Owner.IsPassable((int)X, (int)Y) && Client.Account.Rank < 2)
                    {
                        log.Fatal($"Player {Name} No-Cliped at position: {X}, {Y}");

                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            try
            {
                SendNewTick(time);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            if (HP < 0 && !dying)
            {
                Death("Unknown");
                return;
            }




            if (CheckImmune2Dazed() == true)
            {
                iteme = true;

                if (HP >= MaxHp / 2)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.DazedImmune,
                        DurationMS = -1
                    });

                }
                if (HP <= MaxHp / 2)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.DazedImmune,
                        DurationMS = 0
                    });
                    iteme = false;
                }

            }
            else if (iteme == true && CheckImmune2Dazed() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.DazedImmune,
                    DurationMS = 0
                });
                iteme = false;
            }





            if (CheckHalfMPAImmune() == true)
            {
                itemc = true;

                if (Mp <= MaxMp / 2)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.ArmorBreakImmune,
                        DurationMS = -1
                    });

                }
                if (Mp >= MaxMp / 2)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.ArmorBreakImmune,
                        DurationMS = 0
                    });
                    itemc = false;
                }

            }
            else if (itemc == true && CheckHalfMPAImmune() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.ArmorBreakImmune,
                    DurationMS = -1
                });
                itemc = false;
            }


			if (CheckHealer())
			{
			//	if (Inventory[4].ObjectId == "Galatic Spear" || Inventory[5].ObjectId == "Galatic Spear" || Inventory[6].ObjectId == "Galatic Spear" || Inventory[7].ObjectId == "Galatic Spear" || Inventory[8].ObjectId == "Galatic Spear" || Inventory[9].ObjectId == "Galatic Spear" || Inventory[10].ObjectId == "Galatic Spear" || Inventory[11].ObjectId == "Galatic Spear")
			//	{
			//		var chicken = StatsType.Vitality;
			//		chicken = chicken + 10;
			//	}
				if (Random.Next(0, 100) <= 10)
				{
					ApplyConditionEffect(new ConditionEffect
					{
						Effect = ConditionEffectIndex.Healing,
						DurationMS = 2500
					});

				}
			}
			if (CheckObliterator())
			{
				//		if (Inventory[4].ObjectId == "Galatic Axe" || Inventory[5].ObjectId == "Galatic Axe" || Inventory[6].ObjectId == "Galatic Axe" || Inventory[7].ObjectId == "Galatic Axe" || Inventory[8].ObjectId == "Galatic Axe" || Inventory[9].ObjectId == "Galatic Axe" || Inventory[10].ObjectId == "Galatic Axe" || Inventory[11].ObjectId == "Galatic Axe")
				//		{
				//			var chicken = StatsType.Vitality;
				//			chicken = chicken + 10;
				//		}

				if (Random.Next(0, 100) <= 5)
				{
					ApplyConditionEffect(new ConditionEffect
					{
						Effect = ConditionEffectIndex.Damaging,
						DurationMS = 2500
					});
					ApplyConditionEffect(new ConditionEffect
					{
						Effect = ConditionEffectIndex.Berserk,
						DurationMS = 2500
					});
				}
			}


			if (CheckStunImmune() == true)
            {
                itemb = true;

                if (HP == HP)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.StunImmune,
                        DurationMS = -1
                    });

                }
                if (HP != HP)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.StunImmune,
                        DurationMS = 0
                    });
                    itemb = false;
                }

            }
		
				else if (itemb == true && CheckStunImmune() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.StunImmune,
                    DurationMS = 0
                });
                itemb = false;
            }



            if (CheckHalfBerserk() == true)
            {
                itemd = true;

                if (Mp == MaxMp)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Berserk,
                        DurationMS = -1
                    });

                }
                if (Mp != MaxMp)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.Berserk,
                        DurationMS = 0
                    });
                    itemd = false;
                }

            }
            else if (itemd == true && CheckHalfBerserk() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Berserk,
                    DurationMS = 0
                });
                itemd = false;
            }

            if (CheckImmunePara() == true)
            {
                itema = true;

                if (HP >= MaxHp / 3)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.ParalyzeImmune,
                        DurationMS = -1
                    });

                }
                if (HP <= MaxHp / 3)
                {
                    ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = ConditionEffectIndex.ParalyzeImmune,
                        DurationMS = 0
                    });
                    itema = false;
                }

            }
            else if (itema == true && CheckHalfBerserk() == false)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.ParalyzeImmune,
                    DurationMS = 0
                });
                itema = false;
            }



            base.Tick(time);
        }

        public int NearbyDefBoost()
        {
            if (CheckNearbyDefBoost())
            {
                int sp = 0;

                Enemy[] targets = this.GetNearestEntitieIsGroup(2, "Gods").OfType<Enemy>().ToArray();
                foreach (Enemy e in targets)
                {
                    sp += 1;
                }
                return StatsType.Defense + sp;
            }
            else
            {
                return 0;
            }
        }
        /*

              if (CheckTeleportLowHp() && HP == MaxHp)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Damaging,
                    DurationMS = 5000
                });
            }
            else if (!CheckTeleportLowHp() || HP == MaxHp)
            {
                ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Damaging,
                    DurationMS = 0
                });
            }



    */
        //LE1Ability
        public int TeleportLowHp()
        {

            if (CheckTeleportLowHp() && HP == MaxHp)//StatsManager.cs
                return StatsManager.GetATTTKSomething(1);
            else return 0;
        }
        public int HPBoostMp()
        {
          
            
            if (CheckHPBoostMp() && Mp >= Mp /2)
                return StatsManager.GetHpSomething(1);
            else return 0;
        }
        public int LE1Ability()
        {
            if (CheckLE1Ability() && HP <= MaxHp / 2)
                return 10;
            else return 0;
        }
        public int HPabovehalf()
        {
            if (CheckHPabovehalf() && MaxHp == HP)
                return 15;
            else return 0;
        }
        public int Health24()
        {
            if (HP <= MaxHp / 2 && CheckHealth24())
                return 25;
            else return 0;
        }
        public int MPabovehalf()
        {
            if (CheckMPabovehalf() && Mp >= MaxMp / 2)
                return 40;
            else return 0;
        }


        private bool CheckResurrection()
        {
            for (var i = 0; i < 4; i++)
            {
                var item = Inventory[i];
                if (item == null || !item.Resurrects) continue;

                HP = Stats[0] + Stats[0];
                Mp = Stats[1] + Stats[1];
                Inventory[i] = null;
                Owner.BroadcastPacket(new TextPacket
                {
                    BubbleTime = 0,
                    Stars = -1,
                    Name = "",
                    Text = $"{Name}'s {item.ObjectId} breaks and he disappears"
                }, null);
                Client.Reconnect(new ReconnectPacket
                {
                    Host = "",
                    Port = Program.Settings.GetValue<int>("port"),
                    GameId = World.NEXUS_ID,
                    Name = "Nexus",
                    Key = Empty<byte>.Array,
                });

                resurrecting = true;
                return true;
            }
            return false;
        }

        private void GenerateGravestone()
        {
            var maxed = (from i in Manager.GameData.ObjectTypeToElement[ObjectType].Elements("LevelIncrease")
                         let xElement = Manager.GameData.ObjectTypeToElement[ObjectType].Element(i.Value)
                         where xElement
                         != null
                         let limit = int.Parse(xElement.Attribute("max").Value)
                         let idx = StatsManager.StatsNameToIndex(i.Value)
                         where Stats[idx] >= limit
                         select limit).Count();

            ushort objType;
            int? time;
            switch (maxed)
            {
                case 8:
                    objType = 0x0735;
                    time = null;
                    break;

                case 7:
                    objType = 0x0734;
                    time = null;
                    break;

                case 6:
                    objType = 0x072b;
                    time = null;
                    break;

                case 5:
                    objType = 0x072a;
                    time = null;
                    break;

                case 4:
                    objType = 0x0729;
                    time = null;
                    break;

                case 3:
                    objType = 0x0728;
                    time = null;
                    break;

                case 2:
                    objType = 0x0727;
                    time = null;
                    break;

                case 1:
                    objType = 0x0726;
                    time = null;
                    break;

                default:
                    if (Level <= 1)
                    {
                        objType = 0x0723;
                        time = 30 * 1000;
                    }
                    else if (Level < 20)
                    {
                        objType = 0x0724;
                        time = 60 * 1000;
                    }
                    else if (Level >= 20)
                    {
                        objType = 0x7988;
                        time = 60 * 1000;
                    }
                    else
                    {
                        objType = 0x0725;
                        time = 5 * 60 * 1000;
                    }
                    break;
            }
            var obj = new StaticObject(Manager, objType, time, true, time != null, false);
            obj.Move(X, Y);
            obj.Name = Name;
            Owner.EnterWorld(obj);
        }

        private void HandleRegen(RealmTime time)
        {
            if (HP == Stats[0] + Boost[0] || !CanHpRegen())
                hpRegenCounter = 0;
            else
            {
                hpRegenCounter += StatsManager.GetHPRegen() * time.thisTickTimes / 500f;
                var regen = (int)hpRegenCounter;
                if (regen > 0)
                {
                    HP = Math.Min(Stats[0] + Boost[0], HP + regen);
                    hpRegenCounter -= regen;
                    UpdateCount++;
                }
            }

            if (Mp == Stats[1] + Boost[1] || !CanMpRegen())
                mpRegenCounter = 0;
            else
            {
                mpRegenCounter += StatsManager.GetMPRegen() * time.thisTickTimes / 500f;
                var regen = (int)mpRegenCounter;
                if (regen <= 0) return;
                Mp = Math.Min(Stats[1] + Boost[1], Mp + regen);
                mpRegenCounter -= regen;
                UpdateCount++;
            }
        }

        public new void Dispose()
        {
            tiles = null;
            Guild.Remove(this);
        }
    }
}