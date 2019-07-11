#region

using db.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace wServer.realm.entities
{
    internal class MerchantLists
    {
        public static int[] AccessoryClothList;
        public static int[] AccessoryDyeList;
        public static int[] ClothingClothList;
        public static int[] ClothingDyeList;

        public static Dictionary<int, Tuple<int, CurrencyType>> prices = new Dictionary<int, Tuple<int, CurrencyType>>
        {
            {0xb41, new Tuple<int, CurrencyType>(0, CurrencyType.Gold)},
            {0xbab, new Tuple<int, CurrencyType>(0, CurrencyType.Gold)},
            {0xbad, new Tuple<int, CurrencyType>(0, CurrencyType.Gold)},

            //WEAPONS
            {0xaf6, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Wand Of Recompense T12
            {0xa87, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Wand Of Ancient Warning T11
            {0xa86, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Wand Of Shadow T10
            {0xa85, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Wand Of Deep Sorcery T9
            {0xa07, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Wand Of Death T8
            {0xb02, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Bow Of Covert Havens T12
            {0xa8d, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Bow Of Innocent Blood T11
            {0xa8c, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Bow Of Fey Magic T10
            {0xa8b, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Verdant Bow T9
            {0xa1e, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Golden Bow T8
            {0xb08, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Staff of the Cosmic Whole T12
            {0xaa2, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Staff of Astral Knowledge T11
            {0xaa1, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Staff of Diabolic Secrets T10
            {0xaa0, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Staff Of Necrotic Arcana T9
            {0xa9f, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Staff of Horror T8
            {0xb0b, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Sword of Acclaim T12
            {0xa47, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Skysplitter Sword T11
            {0xa84, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Archon Sword T10
            {0xa83, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Dragonsoul Sword T9
            {0xa82, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Ravenheart Sword T8
            {0xaff, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Dagger Of Foul Malevolence T12
            {0xa8a, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Agateclaw Dagger T11
            {0xa89, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Emeraldshard Dagger T10
            {0xa88, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ragetalon Dagger T9
            {0xa19, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Fire Dagger T8
            {0xc50, new Tuple<int, CurrencyType>(850, CurrencyType.Gold)}, //Masamune T12
            {0xc4f, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Muramasa T11
            {0xc4e, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Ichimonji T10
            {0xc4d, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Jewel Eye Katana T9
            {0xc4c, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Demon Edge T8

            //Rings
            {0xabf, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ring of Paramount Attack T4
            {0xac0, new Tuple<int, CurrencyType>(120, CurrencyType.Gold)}, //Ring of Paramount Defense T4
            {0xac1, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ring Of Paramount Speed T4
            {0xac2, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ring Of Paramount Vitality T4
            {0xac3, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ring Of Paramount Wisdom T4
            {0xac4, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ring Of Paramount Dexterity T4
            {0xac5, new Tuple<int, CurrencyType>(120, CurrencyType.Gold)}, //Ring Of Paramount Health T4
            {0xac6, new Tuple<int, CurrencyType>(120, CurrencyType.Gold)}, //Ring Of Paramount Magic T4
            {0xac7, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Ring Of Exalted Attack T5
            {0xac8, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Ring Of Exalted Defense T5
            {0xac9, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Ring Of Exalted Speed T5
            {0xaca, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Ring Of Exalted Vitality T5
            {0xacb, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Ring Of Exalted Wisdom T5
            {0xacc, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Ring Of Exalted Dexterity T5
            {0xacd, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Ring Of Exalted Health T5
            {0xace, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Ring Of Exalted Magic T5
            //ARMORS
            {0xb05, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Robe of the Grand Sorcerer
            {0xa96, new Tuple<int, CurrencyType>(425, CurrencyType.Gold)}, //Robe of the Elder Warlock T12
            {0xa95, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Robe of the Moon Wizard T11
            {0xa94, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Robe of the Shadow Magus T10
            {0xa60, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Robe of the Master T9
            {0xafc, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Acropolis Armor T13
            {0xa93, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Abyssal Armor T12
            {0xa92, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Vengeance Armor T11
            {0xa91, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Desolation Armor T10
            {0xa13, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Dragonscale Armor T9
            {0xaf9, new Tuple<int, CurrencyType>(900, CurrencyType.Gold)}, //Hydra Skin Armor T13
            {0xa90, new Tuple<int, CurrencyType>(450, CurrencyType.Gold)}, //Griffon Hide Armor T12
            {0xa8f, new Tuple<int, CurrencyType>(250, CurrencyType.Gold)}, //Hippogriff Hide Armor t11
            {0xa8e, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Roc Leather Armor T10
            {0xad3, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Drake Hide Armor T9
     
            //ABILITIES
            {0xb25, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Tome Of Holy Guidance T6
            {0xa5b, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Tome of Divine Favor T5
            {0x7265, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //T6 candle
            {0x7263, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //T5 candle
            {0x7111, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //T6 waki
            {0x7110, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //T5 waki
            {0xb22, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Colossus Shield T6
            {0xa0c, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Mithril Shield T5
            {0xb24, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Elemental Detonation Spell T6
            {0xa30, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Magic Nova Spell T5
            {0xb26, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Seal of the Blessed Champion T6
            {0xa55, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Seal of the Holy Warrior T5
            {0xb27, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Cloak of Ghostly Concealment T6
            {0xae1, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Cloak of Endless Twilight T5
            {0xb28, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Quiver of Elvish Mastery T6
            {0xa65, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Golden Quiver T5
            {0xb29, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Helm of the Great General T6
            {0xa6b, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Golden Helm T5
            {0xb2a, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Baneserpent Poison T6
            {0xaa8, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //NightWing Venom T5
            {0xb2b, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Bloodsucker Skull T6
            {0xaaf, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Lifedrinker Skull T5
            {0xb2c, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Giantcatcher Trap T6
            {0xab6, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Dragonstalker Trap T5
            {0xb2d, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Planefetter Orb T6
            {0xa46, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Banishment Orb T5
            {0xb23, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Prism of Apparitions T6
            {0xb20, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Prism of Phantoms T5
            {0xb33, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Scepter of Storms T6
            {0xb32, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Scepter of Skybolts T5
            {0xc59, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //Doom Circle T6
            {0xc58, new Tuple<int, CurrencyType>(175, CurrencyType.Gold)}, //Ice Star T5

            //PET FOOD
            {0xccc, new Tuple<int, CurrencyType>(5000, CurrencyType.Gold)}, //Ambrosia 
            {0xccb, new Tuple<int, CurrencyType>(60, CurrencyType.Gold)}, //Fries
            {0xcca, new Tuple<int, CurrencyType>(360, CurrencyType.Gold)}, //Grapes Of Wrath
            {0xcc9, new Tuple<int, CurrencyType>(20, CurrencyType.Gold)}, //Soft Drink
            {0xcc8, new Tuple<int, CurrencyType>(420, CurrencyType.Gold)}, //Superburger
            {0xcc7, new Tuple<int, CurrencyType>(720, CurrencyType.Gold)}, //Double Cheese Burger Deluxe
            {0xcc6, new Tuple<int, CurrencyType>(120, CurrencyType.Gold)}, //Great Taco
            {0xcc5, new Tuple<int, CurrencyType>(180, CurrencyType.Gold)}, //Power Pizza
            {0xcc4, new Tuple<int, CurrencyType>(240, CurrencyType.Gold)}, //Chocolate Ice Cream Cookie

            //EGGS
            {0xc86, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon feline egg
            {0xc87, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare feline egg
            {0xc8a, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon canine egg
            {0xc8b, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare canine egg
            {0xc8e, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon avian egg
            {0xc8f, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare avian egg
            {0xc92, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon exotic egg
            {0xc93, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare exotic egg
            {0xc96, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon farm egg
            {0xc97, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare farm egg
            {0xc9a, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon woodland egg
            {0xc9b, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare woodland egg
            {0xc9e, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon reptile egg
            {0xc9f, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare reptile egg
            {0xca2, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon insect egg
            {0xca3, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare insect egg
            {0xca6, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon pinguin egg
            {0xca7, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare pinguin egg
            {0xcaa, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon aquatic egg
            {0xcab, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare aquatic egg
            {0xcae, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon spooky egg
            {0xcaf, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare spooky egg
            {0xcb2, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon humanoid egg
            {0xcb3, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare humanoid egg
            {0xcb6, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon ???? egg
            {0xcb7, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare ???? egg
            {0xcba, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon automaton egg
            {0xcbb, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare automaton egg
            {0xcbe, new Tuple<int, CurrencyType>(800, CurrencyType.Gold)}, //uncommon mystery egg
            {0xcbf, new Tuple<int, CurrencyType>(1200, CurrencyType.Gold)}, //rare mystery egg
            {0xcc0, new Tuple<int, CurrencyType>(3600, CurrencyType.Gold)}, //Legendary mystery egg
            {0xc6c, new Tuple<int, CurrencyType>(400, CurrencyType.Gold)}, //backpack

            //KEYS
            {0x2290, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Bella's Key - just temporary for testing
            {0x2294, new Tuple<int, CurrencyType>(150, CurrencyType.Gold)},
            {0x701, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Undead Lair Key
            {0x7222, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Epic Servant key
           {0x7068, new Tuple<int, CurrencyType>(90, CurrencyType.Gold)}, //Epic Jungle key
            {0x70a, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Abyss of Demons Key
            {0x70b, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Snake Pit Key
            {0x710, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Tomb of the Ancients Key
            {0x71f, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Sprite World Key
            {0xc11, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Ocean Trench Key
            {0xc23, new Tuple<int, CurrencyType>(51, CurrencyType.Gold)}, //Manor Key
            {0xc2e, new Tuple<int, CurrencyType>(150, CurrencyType.Gold)}, //Davy's Key
            {0x7362, new Tuple<int, CurrencyType>(150, CurrencyType.Gold)}, //Grassland Key
            {0x7407, new Tuple<int, CurrencyType>(150, CurrencyType.Gold)}, //Omen Key
            {0x7359, new Tuple<int, CurrencyType>(200, CurrencyType.Gold)}, //Halls Key
            {0xc2f, new Tuple<int, CurrencyType>(100, CurrencyType.Gold)}, //Lab Key
            {0xcce, new Tuple<int, CurrencyType>(90, CurrencyType.Gold)}, //Deadwater Docks Key
            {0xccf, new Tuple<int, CurrencyType>(90, CurrencyType.Gold)}, //Woodland Labyrinth Key
            {0xcda, new Tuple<int, CurrencyType>(90, CurrencyType.Gold)}, //The Crawling Depths Key
        };

        public static int[] store10List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store11List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store12List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store13List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store14List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store15List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store16List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store17List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store18List = { 0xb41, 0xbab, 0xbad, 0xbac };
        public static int[] store19List = { 0xb41, 0xbab, 0xbad, 0xbac };

        public static int[] store1List =
        {
            0x2294, 0x7068, 0x7407, 0x7362, 0x7359, 0x7222, 0xcda, 0xccf, 0xcce, 0xc2f, 0xc2e, 0xc23, 0xc19, 0xc11, 0x71f, 0x710,
            0x70b, 0x70a, 0x705, 0x701, 0x2290
        };

        public static int[] store20List = { 0xb41, 0xbab, 0xbad, 0xbac };

        //keys need to add etcetc
        public static int[] store2List =
        {

        };

        //pet eggs
        public static int[] store3List =
        {
            0x7409, 0x7416, 0x7413, 0x7407, 0x7404, 0x7402, 0x7401, 0x7400, 0x7392, 0x244, 0x7382, 0x7381, 0x024c, 0x7369, 0x7368, 0x7390, 0x7379
        };

        //pet food
        public static int[] store4List =
        {//here
            0x7263, 0x7265, 0x7110, 0x7111, 0xb25, 0xa5b, 0xb22, 0xa0c, 0xb24, 0xa30, 0xb26, 0xa55, 0xb27, 0xae1, 0xb28,
            0xa65, 0xb29, 0xa6b, 0xb2a, 0xaa8, 0xb2b, 0xaaf, 0xb2c, 0xab6, 0xb2d, 0xa46, 0xb23, 0xb20, 0xb33, 0xb32,
            0xc59, 0xc58
        };

        //abilities
        public static int[] store5List =
        {
            0xb05, 0xa96, 0xa95, 0xa94, 0xa60, 0xafc, 0xa93, 0xa92, 0xa91, 0xa13, 0xaf9,
            0xa90, 0xa8f, 0xa8e, 0xad3
        };

        //armors
        public static int[] store6List =
        {
            0xaf6, 0xa87, 0xa86, 0xa85, 0xa07, 0xb02, 0xa8d, 0xa8c, 0xa8b, 0xa1e, 0xb08,
            0xaa2, 0xaa1, 0xaa0, 0xa9f
        };

        //Wands&staves&bows
        public static int[] store7List =
        {
            0xb0b, 0xa47, 0xa84, 0xa83, 0xa82, 0xaff, 0xa8a, 0xa89, 0xa88, 0xa19, 0xc50,
            0xc4f, 0xc4e, 0xc4d, 0xc4c
        };

        //Swords&daggers&samurai shit
        public static int[] store8List =
        {
            0xabf, 0xac0, 0xac1, 0xac2, 0xac3, 0xac4, 0xac5, 0xac6, 0xac7, 0xac8, 0xac9,
            0xaca, 0xacb, 0xacc, 0xacd, 0xace
        };

        // rings
        public static int[] store9List = { 0xb41, 0xbab, 0xbad, 0xbac, 0x716c, 0x2299, 0x2360, 0x21a9, 0x108e, 0xc03, 0x229e, 0xcbde };

        private static readonly ILog log = LogManager.GetLogger(typeof(MerchantLists));
        public static void InitMerchatLists(XmlData data)
        {
            log.Info("Loading merchant lists...");
            List<int> accessoryDyeList = new List<int>();
            List<int> clothingDyeList = new List<int>();
            List<int> accessoryClothList = new List<int>();
            List<int> clothingClothList = new List<int>();

            foreach (KeyValuePair<ushort, Item> item in data.Items.Where(_ => noShopCloths.All(i => i != _.Value.ObjectId)))
            {
                if (item.Value.Texture1 != 0 && item.Value.ObjectId.Contains("Clothing") && item.Value.Class == "Dye")
                {
                    prices.Add(item.Value.ObjectType, new Tuple<int, CurrencyType>(51, CurrencyType.Gold));
                    clothingDyeList.Add(item.Value.ObjectType);
                }

                if (item.Value.Texture2 != 0 && item.Value.ObjectId.Contains("Accessory") && item.Value.Class == "Dye")
                {
                    prices.Add(item.Value.ObjectType, new Tuple<int, CurrencyType>(51, CurrencyType.Gold));
                    accessoryDyeList.Add(item.Value.ObjectType);
                }

                if (item.Value.Texture1 != 0 && item.Value.ObjectId.Contains("Cloth") &&
                    item.Value.ObjectId.Contains("Large"))
                {
                    prices.Add(item.Value.ObjectType, new Tuple<int, CurrencyType>(160, CurrencyType.Gold));
                    clothingClothList.Add(item.Value.ObjectType);
                }

                if (item.Value.Texture2 != 0 && item.Value.ObjectId.Contains("Cloth") &&
                    item.Value.ObjectId.Contains("Small"))
                {
                    prices.Add(item.Value.ObjectType, new Tuple<int, CurrencyType>(160, CurrencyType.Gold));
                    accessoryClothList.Add(item.Value.ObjectType);
                }
            }

            ClothingDyeList = clothingDyeList.ToArray();
            ClothingClothList = clothingClothList.ToArray();
            AccessoryClothList = accessoryClothList.ToArray();
            AccessoryDyeList = accessoryDyeList.ToArray();
            log.Info("Merchat lists added.");
        }

        private static readonly string[] noShopCloths =
        {
            "Large Ivory Dragon Scale Cloth", "Small Ivory Dragon Scale Cloth",
            "Large Green Dragon Scale Cloth", "Small Green Dragon Scale Cloth",
            "Large Midnight Dragon Scale Cloth", "Small Midnight Dragon Scale Cloth",
            "Large Blue Dragon Scale Cloth", "Small Blue Dragon Scale Cloth",
            "Large Red Dragon Scale Cloth", "Small Red Dragon Scale Cloth",
            "Large Jester Argyle Cloth", "Small Jester Argyle Cloth",
            "Large Alchemist Cloth", "Small Alchemist Cloth",
            "Large Mosaic Cloth", "Small Mosaic Cloth",
            "Large Spooky Cloth", "Small Spooky Cloth",
            "Large Flame Cloth", "Small Flame Cloth",
            "Large Heavy Chainmail Cloth", "Small Heavy Chainmail Cloth",
        };
    }
}