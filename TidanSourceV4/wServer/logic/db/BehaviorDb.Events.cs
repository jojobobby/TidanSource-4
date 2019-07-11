#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Events = () => Behav()
        #region Skull Shrine

            .Init("Skull Shrine",
                new State(
                    new DropPortalOnDeath("Lair of Shaitan Portal", percent: 75, dropDelaySec: 2),
                    new ScaleHP(1000),
                    new State("Nothing",
                    new Shoot(25, 9, 10, predictive: 1),
                    new Spawn("Red Flaming Skull", 4, coolDown: 5000),
                    new Spawn("Blue Flaming Skull", 5, coolDown: 1000),
                    new Reproduce("Red Flaming Skull", 5, 4, 5000),
                    new Reproduce("Blue Flaming Skull", 5, 5, 1000)
                  )
                    ),
                new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                     new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                     new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new MostDamagers(30,
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new ItemLoot("Orb of Conflict", 0.01),
                    new ItemLoot("Minor Potion of Mana", 0.50),
                    new ItemLoot("Minor Potion of Life", 0.50),
                    new ItemLoot("Burning Core Ring", 0.03),
                    new ItemLoot("Eledor's Perfect Wakizashi", 0.01),
                    new ItemLoot("Equilibrium", 0.01)
                )
            )

            .Init("Red Flaming Skull",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
            .Init("Blue Flaming Skull",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Skull Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
        #endregion

        #region Hermit God

            .Init("Hermit God",
                new State(
                    new ScaleHP(1000),

                    new CopyDamageOnDeath("Hermit God Drop"),
                    new State("invis",
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new InvisiToss("Hermit Minion", 9, 0, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 315, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 15, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 30, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 120, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 210, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 240, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 50, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 100, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 200, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 250, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 300, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 315, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Drop", 5, 0, coolDown: 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Drop", 5, 0, coolDown: 90000001, coolDownOffset: 0),

                        //new Spawn("Hermit God Tentacle", 8, 8, coolDown:9000001),
                        new TimedTransition(1000, "check")
                        ),
                    new State("check",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God Tentacle", 20, "active")
                        ),
                    new State("active",
                        new SetAltTexture(2),
                        new TimedTransition(500, "active2")
                        ),
                    new State("active2",
                        new SetAltTexture(0),
                        new Shoot(25, 3, 10, 0, coolDown: 200),
                        new Wander(.2),
                        new TossObject("Whirlpool", 6, 0, 90000001, 100),
                        new TossObject("Whirlpool", 6, 45, 90000001, 100),
                        new TossObject("Whirlpool", 6, 90, 90000001, 100),
                        new TossObject("Whirlpool", 6, 135, 90000001, 100),
                        new TossObject("Whirlpool", 6, 180, 90000001, 100),
                        new TossObject("Whirlpool", 6, 225, 90000001, 100),
                        new TossObject("Whirlpool", 6, 270, 90000001, 100),
                        new TossObject("Whirlpool", 6, 315, 90000001, 100),
                        new TimedTransition(20000, "rage")
                        ),
                    new State("rage",
                        new SetAltTexture(4),
                        new Order(20, "Whirlpool", "despawn"),
                        new Flash(0xfFF0000, .8, 9000001),
                        new Shoot(25, 8, projectileIndex: 1, coolDown: 2000),
                        new Shoot(25, 20, projectileIndex: 2, coolDown: 3000, coolDownOffset: 5000),
                        new TimedTransition(17000, "invis")
                        )
                    )
            )
            .Init("Whirlpool",
                new State(
                    new State("active",
                        new Shoot(25, 8, projectileIndex: 0, coolDown: 1000),
                        new Orbit(.5, 4, target: "Hermit God", radiusVariance: 0),
                        new EntityNotExistsTransition("Hermit God", 50, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    )
            )
            .Init("Hermit God Tentacle",
                new State(
                    new Prioritize(
                        new Orbit(.5, 5, target: "Hermit God", radiusVariance: 0.5),
                        new Follow(0.85, range: 1, duration: 2000, coolDown: 0)
                        ),
                    new Shoot(4, 8, projectileIndex: 0, coolDown: 1000)
                    )
            )
            .Init("Hermit Minion",
                new State(
                    new Prioritize(
                        new Wander(.5),
                        new Follow(0.85, 3, 1, 2000, 0)
                        ),
                    new Shoot(5, 1, 1, 1, coolDown: 2300),
                    new Shoot(5, 3, 1, 0, coolDown: 1000)
                    )
            )
            .Init("Hermit God Drop",
                new State(//Lair of Shaitan Portal
                          //new DropPortalOnDeath("Lair of Shaitan Portal", percent: 75, dropDelaySec: 2),
                    new DropPortalOnDeath("Ocean Trench Portal", percent: 75, dropDelaySec: 2),
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God", 10, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    ),
                new MostDamagers(30,
                    new OnlyOne(
                        new ItemLoot("Potion of Dexterity", 1),
                        new ItemLoot("Potion of Defense", 0.1),
                        new ItemLoot("Potion of Attack", 1),
                        new ItemLoot("Potion of Vitality", 0.1)
                    )
                ),
                new GoldLoot(10, 120),
                new MostDamagers(30,
                    new ItemLoot("Helm of the Juggernaut", 0.015),
                    new ItemLoot("Potion of Defense", 0.1),
                        new ItemLoot("Potion of Attack", 1)
                )
            )
        #endregion

        #region Mythical Nymph

        .Init("Mythical Nymph",
                new State(

                    new ScaleHP(1000),
                    new StayCloseToSpawn(0.3, range: 7),

                    new State("Wait",
                        new Flash(0xffffff, 2, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new PlayerWithinTransition(200, "Activate")
                    ),
                    new State("Activate",
                        new Taunt("You worthless human, this will be your last!"),
                        new TimedTransition(2500, "RemINVINC")
                    ),
                    new State("RemINVINC",
                        new Flash(0xffffff, 2, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new HpLessTransition(.50, "Spawn"),
                        new TimedTransition(2000, "Shotgun")
                    ),
                    new State("FlashRING",
                        new Flash(0xd40000, 2, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new HpLessTransition(.50, "Spawn"),
                        new TimedTransition(2000, "RingCharge")
                    ),
                    new State("RingCharge",
                        new Follow(1.8, range: 1, duration: 5000, coolDown: 0),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 5),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 220),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 420), //smonk :3
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 620),
                    new HpLessTransition(.50, "Spawn"),
                        new TimedTransition(800, "ChooseRandom")
                    ),
                    new State("Shotgun",
                        new Follow(.7, range: 1, duration: 5000, coolDown: 0),
                        new HpLessTransition(.50, "Spawn"),
                        new Shoot(10, count: 7, predictive: 0.1, shootAngle: 5, coolDown: 500, projectileIndex: 3),
                        new TimedTransition(3700, "ChooseRandom")
                    ),
                    new State("Singular",
                        new Follow(.7, range: 1, duration: 5000, coolDown: 0),
                        new HpLessTransition(.50, "Spawn"),
                        new Shoot(10, count: 10, predictive: 0.1, coolDown: 90, projectileIndex: 0),
                        new TimedTransition(1800, "ChooseRandom")
                    ),
                    new State("PetRing",
                        new HpLessTransition(.50, "Spawn"),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 400, projectileIndex: 4),
                        new TimedTransition(120, "ChooseRandom")
                    ),
                    new State("Spawn",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("I need to rest, Davy, Assist me!"),
                       new ReturnToSpawn(true, 1),
                        new TimedTransition(4000, "Spawn2")
                       ),
                   new State("Spawn2",
                       new SetAltTexture(1),
                       new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TossObject("Ghostly Ship", 5, coolDown: 80000000, angle: 120),
                        new TossObject("Ghostly Ship", 5, coolDown: 80000000, angle: 60),
                        new TimedTransition(8000, "Wait")
                    ),
                    new State("Wait",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Wander(0.5),
                        new EntitiesNotExistsTransition(10000, "JadeDied", "Ghostly Ship")
                        ),
                    new State("ChooseRandom",
                        new Flash(0xffffff, 2, 100),
                        new HpLessTransition(.50, "Spawn"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(1200, "Singular", true),
                        new TimedTransition(1200, "FlashRING", true),
                        new TimedTransition(1200, "Shotgun", true),
                        new TimedTransition(1200, "PetRing", true)
                    ),
                    new State("JadeDied",
                         new Taunt("No, Davy! Enough, this has gone to far!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xffffff, 2, 100),
                        new SetAltTexture(0),
                        new TimedTransition(1500, "ChooseRandomV2")
                    ),
                    new State("ChooseRandomV2",
                        new Flash(0xffffff, 2, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(1200, "SingularV2", true),
                        new TimedTransition(1200, "FlashRINGV2", true),
                        new TimedTransition(1200, "ShotgunV2", true),
                        new TimedTransition(1200, "PetRingV2", true),
                        new TimedTransition(1200, "ShotgunWIDER", true),
                        new TimedTransition(1200, "Swirl", true)
                    ),
                    new State("FlashRINGV2",
                        new Flash(0xd40000, 2, 100),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2000, "RingChargeV2")
                    ),
                    new State("RingChargeV2",
                        new Follow(1.8, range: 1, duration: 5000, coolDown: 0),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 5),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 220),
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 420), //smonk :3
                        new Shoot(10, count: 20, shootAngle: 18, coolDown: 99999, projectileIndex: 1, coolDownOffset: 620),
                        new TimedTransition(800, "ChooseRandomV2")
                    ),
                    new State("ShotgunV2",
                        new Follow(.7, range: 1, duration: 5000, coolDown: 0),
                        new Shoot(10, count: 5, predictive: 0.1, shootAngle: 5, coolDown: 500, projectileIndex: 0),
                        new Shoot(10, count: 1, predictive: 0.1, shootAngle: 5, coolDown: 580, projectileIndex: 3),
                        new TimedTransition(2300, "ChooseRandomV2")
                    ),
                    new State("ShotgunWIDER",
                        new Shoot(10, count: 5, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 0, coolDownOffset: 50),
                        new Shoot(10, count: 1, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 2, coolDownOffset: 50),
                        //
                        new Shoot(10, count: 10, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 0, coolDownOffset: 500),
                        new Shoot(10, count: 1, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 2, coolDownOffset: 500),
                        //
                        new Shoot(10, count: 15, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 0, coolDownOffset: 1000),
                        new Shoot(10, count: 1, predictive: 0.1, shootAngle: 5, coolDown: 99999, projectileIndex: 2, coolDownOffset: 1000),
                        new TimedTransition(1200, "ChooseRandomV2")
                    ),
                    new State("SingularV2",
                        new Follow(.7, range: 1, duration: 5000, coolDown: 0),
                        new Shoot(10, count: 1, predictive: 0.1, coolDown: 90, projectileIndex: 0),
                        new TimedTransition(2500, "ChooseRandomV2")
                    ),
                    new State("PetRingV2",
                        new Shoot(10, count: 20, shootAngle: 18, fixedAngle: 0, coolDown: 9400, projectileIndex: 4),
                        new Shoot(10, count: 20, shootAngle: 18, fixedAngle: 10, coolDown: 9400, projectileIndex: 4, coolDownOffset: 100),
                        new Shoot(10, count: 20, shootAngle: 18, fixedAngle: 20, coolDown: 9400, projectileIndex: 4, coolDownOffset: 300),
                        new TimedTransition(120, "ChooseRandomV2")
                    ),
                    new State("Swirl",
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 0, projectileIndex: 4, coolDownOffset: 50),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 90, projectileIndex: 4, coolDownOffset: 200),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 180, projectileIndex: 4, coolDownOffset: 400),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 270, projectileIndex: 4, coolDownOffset: 600),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 45, projectileIndex: 4, coolDownOffset: 800),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 135, projectileIndex: 4, coolDownOffset: 1000),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 225, projectileIndex: 4, coolDownOffset: 1200),
                        new Shoot(10, count: 2, shootAngle: 20, coolDown: 99999, fixedAngle: 315, projectileIndex: 4, coolDownOffset: 1400),
                        new TimedTransition(1500, "ChooseRandomV2")
                    )
                ),
                new GoldLoot(10, 120),
                new MostDamagers(7,
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Potion of Wisdom", 0.5),
                    new ItemLoot("Horned Titanium Faceguard", 0.01),
                    new ItemLoot("Coral Bleached Shuriken", 0.01)
                )
            )

           .Init("Ghostly Ship",
                new State(
                    new StayCloseToSpawn(0.3, range: 7),

                    new Wander(0.5),
                    new State("searching",
                        new Prioritize(

                            ),
                        new PlayerWithinTransition(10, "creeping"),
                        new TimedTransition(5000, "creeping")
                        ),
                    new State("creeping",
                        new Follow(.02, range: 10),
                        new Shoot(10, count: 2, coolDown: 1800, projectileIndex: 1, coolDownOffset: 1400),
                        new Shoot(10, count: 4, predictive: 0.1, shootAngle: 5, coolDown: 400, projectileIndex: 0)
                            )

                ),
                new MostDamagers(7,
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Deathless Crossbow", 0.01)
                    )
            )









        #endregion

        #region liberian
         .Init("Sleeping liberian",
                new State(

                    new ScaleHP(1000),
                    new State("none",
                        new PlayerWithinTransition(9, "Woke")
                        ),
                    new State("Woke",

                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("What are you doing in my libary!", "Have you come here for knowledge?"),
                        new TimedTransition(2500, "Woke2")
                        ),
                    new State("Woke2",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("I will give you all the knowledge you need."),
                        new TimedTransition(2500, "Woke3")
                        ),
                    new State("Woke3",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("Don't worry I'm not here to hurt you, I just wanted to test you!"),
                        new TimedTransition(2500, "Woke4")
                        ),
                    new State("Woke4",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("Pick a color! Red, Blue, or Green."),
                        new ChatTransition("Red", "Red"),
                        new ChatTransition("Blue", "Blue"),
                        new ChatTransition("Green", "Green"),
                        new ChatTransition("Red", "red"),
                        new ChatTransition("Blue", "blue"),
                        new ChatTransition("Green", "green")
                        ),
                    new State("Red",
                        new Wander(0.3),
                         new Taunt("Red? This will be a bloody death for you!"),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(9, count: 8, projectileIndex: 1, coolDown: 500),
                        new Shoot(18, count: 6, projectileIndex: 0, coolDown: 2000),
                        new Shoot(9, 3, projectileIndex: 2, shootAngle: 10, coolDownOffset: 500, predictive: 0.2)
                        ),

                    new State("Blue",
                        new Wander(0.3),
                         new Taunt("Blue? Prepare to drown in my fear!"),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(9, count: 8, projectileIndex: 1, coolDown: 500),
                        new Shoot(18, count: 6, projectileIndex: 0, coolDown: 2000),
                        new Shoot(9, 3, projectileIndex: 3, shootAngle: 10, coolDownOffset: 500, predictive: 0.2)
                        ),
                    new State("Green",
                        new Wander(0.3),
                         new Taunt("Green? I will bury you in the ground and watch you suffocate!"),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(9, count: 8, projectileIndex: 1, coolDown: 500),
                        new Shoot(18, count: 6, projectileIndex: 0, coolDown: 2000),
                        new Shoot(9, 3, projectileIndex: 4, shootAngle: 10, coolDownOffset: 500, predictive: 0.2)
                               )
                    ),
                new MostDamagers(30,
                        new ItemLoot("Potion of Wisdom", 1),
                        new ItemLoot("Potion of Wisdom", 0.5),
                        new ItemLoot("Potion of Dexterity", 1),
                        new ItemLoot("Libarian's Book", 1),
                        new ItemLoot("Liberian's Robe", 0.01)



                        )
                    )


        #endregion

        #region Tree Shrine


         .Init("Tree Shrine",
                new State(
                    new ScaleHP(1000),
                    new State("Nothing",
                    new Shoot(25, 9, 10, predictive: 1),
                    new Spawn("Tree Servant", 4, coolDown: 5000),
                    new Spawn("Tree Attacker", 5, coolDown: 1000),
                    new Reproduce("Tree Servant", 5, 4, 5000),
                    new Reproduce("Tree Attacker", 5, 5, 1000)
                        )
                ),
                new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                     new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new MostDamagers(30,
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new ItemLoot("Minor Potion of Mana", 0.50),
                    new ItemLoot("Minor Potion of Life", 0.50),
                    new ItemLoot("Wise Shrine's Robe", 0.01),
                    new ItemLoot("Shrine's Demonic Hide", 0.01)
                )
            )

            .Init("Tree Attacker",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
            .Init("Tree Servant",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Tree Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )

        #endregion

        #region Witch Shrine

         .Init("Witch Shrine",
                new State(
                    new ScaleHP(1000),
                    new State("Nothing",
                    new Shoot(25, 9, 10, predictive: 1),
                    new Spawn("Witch Magic Ball2", 4, coolDown: 5000),
                    new Spawn("Witch Magic Ball", 5, coolDown: 1000),
                    new Reproduce("Witch Magic Ball2", 5, 4, 5000),
                    new Reproduce("Witch Magic Ball", 5, 5, 1000)
                        )
                ),
                new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                    ),
                new MostDamagers(7,
                    LootTemplates.DailyToken()
                    ),
                     new MostDamagers(7,
                    LootTemplates.StatIncreasePotionsLoot()
                ),
                new MostDamagers(30,
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new ItemLoot("Punderful Magic Katana", 0.01),
                    new ItemLoot("Minor Potion of Mana", 0.50),
                    new ItemLoot("Minor Potion of Life", 0.50),
                    new ItemLoot("Orb of Dark Magic", 0.015)
                )
            )
         .Init("Witch Magic Ball2",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            )
            .Init("Witch Magic Ball",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Witch Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 750)
                    )
            );





        #endregion








    }
}
