using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Halls = () => Behav()

        .Init("LH Marble Colossus",
                new State(
                    new ScaleHP(2000),
                    new ConditionalEffect(ConditionEffectIndex.ArmorBreakImmune),
                    new ConditionalEffect(ConditionEffectIndex.StunImmune),
                    new ConditionalEffect(ConditionEffectIndex.ParalyzeImmune),
                    new ConditionalEffect(ConditionEffectIndex.CurseImmune),
                    new ConditionalEffect(ConditionEffectIndex.StasisImmune),
                    new ConditionalEffect(ConditionEffectIndex.DazedImmune),
                    new ConditionalEffect(ConditionEffectIndex.SlowedImmune),
                    new TransformOnDeath("LH Loot Chest", 1, 1, 1),
                    new HpLessTransition(0.12, "deathbegins"),
                    new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new PlayerWithinTransition(7, "start")
                        ),
                    new State(
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("start",
                        new Taunt(1.00, "I'm here to keep this gate locked!"),
                        new TimedTransition(3000, "talk1")
                        ),
                    new State("talk1",
                        new Taunt(1.00, "The Void doesn't want any mortals near this place!"),
                        new TimedTransition(3000, "talk2")
                        ),
                    new State("talk2",
                        new Taunt(1.00, "I will accept my masters plans."),
                        new TimedTransition(3000, "talk3")
                        ),
                    new State("talk3",
                        new Taunt(1.00, "Death is the only answer!"),
                        new TimedTransition(3000, "GetReady")
                        )
                        ),
                    new State("GetReady",
                        new Flash(0xFFF240, 1, 1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2500, "fight2")
                        ),
                    new State("fight2",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(10, count: 4, shootAngle: 4, projectileIndex: 0, coolDown: 1),
                        new Shoot(10, count: 10, projectileIndex: 1, coolDown: 1750),
                        new TimedTransition(5000, "fight3")
                        ),
                    new State("fight3",
                            new Prioritize(
                                 new Follow(0.4, 8, 1),
                                 new Wander(0.5)
                                ),
                    new Taunt(0.50, "My god, minions of Oryx? Your death is near!"),
                        new Shoot(10, count: 15, shootAngle: 6, projectileIndex: 5, coolDown: 3400),
                        new Shoot(10, count: 5, shootAngle: 28, projectileIndex: 2, coolDown: 2600),
                        new TimedTransition(4750, "fight4")
                        ),
                    new State("fight4",
                            new Prioritize(
                                 new StayCloseToSpawn(0.7, 2),
                                 new Wander(0.5)
                                ),
                        new Shoot(10, count: 8, shootAngle: 15, projectileIndex: 4, coolDown: 1750, fixedAngle: 135),
                        new Shoot(10, count: 8, shootAngle: 15, projectileIndex: 4, coolDown: 1750, fixedAngle: 45),
                        new Shoot(10, count: 8, shootAngle: 15, projectileIndex: 4, coolDown: 1750, fixedAngle: 225),
                        new Shoot(10, count: 8, shootAngle: 15, projectileIndex: 4, coolDown: 1750, fixedAngle: 315),
                        new Shoot(10, count: 3, shootAngle: 30, projectileIndex: 3, coolDown: 3400),
                        new TimedTransition(4750, "fight5")
                        ),
                     new State("fight5",
                          new Prioritize(
                            new Follow(0.20, 8, 1),
                            new Wander(0.6)
                            ),
                        new Taunt(1.00, "Mortals think they can do anything!"),
                        new TimedTransition(4500, "heal"),
                        new State("1",
                            new Shoot(10, count: 3, projectileIndex: 3, coolDown: 3400),
                            new TimedTransition(300, "2")
                        ),
                        new State("2",
                            new Shoot(10, count: 6, projectileIndex: 3, coolDown: 3400),
                            new TimedTransition(300, "3")
                        ),
                        new State("3",
                            new Shoot(10, count: 12, projectileIndex: 3, coolDown: 3400),
                            new TimedTransition(300, "4")
                        ),
                         new State("4",
                            new Shoot(10, count: 18, projectileIndex: 5, coolDown: 3400),
                            new Shoot(10, count: 9, projectileIndex: 0, coolDown: 3400),
                            new Shoot(10, count: 7, projectileIndex: 1, coolDown: 3400),
                            new TimedTransition(300, "1")
                        )
                    ),
                     new State("heal",
                        new ReturnToSpawn(once: true, speed: 0.5),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFFFFFF, 1, 1),
                        new SpecificHeal(1, 6500, "Self", coolDown: 2000),
                        new TimedTransition(8000, "spawn")
                        ),
                   new State("spawn",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TimedTransition(2000, "fight6")
                    ),
                   new State("fight6",
                        new Swirl(0.4, 4, 8),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(10, count: 8, shootAngle: 24, projectileIndex: 4, coolDown: 2000),
                        new Shoot(10, count: 6, shootAngle: 16, predictive: 1.5, projectileIndex: 2, coolDown: 1250),
                        new Shoot(10, count: 8, shootAngle: 16, projectileIndex: 0, coolDown: 675),
                        new Grenade(4.5, 221, 6, coolDown: 1000),
                        new TimedTransition(5750, "fight7")
                       ),
                   new State("fight7",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new StayBack(0.2, 2),
                        new Shoot(10, count: 30, shootAngle: 28, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 15, shootAngle: 24, projectileIndex: 3, coolDown: 1000),
                        new TimedTransition(3750, "rush")
                       ),
                   new State("rush",
                        new Taunt(1.00, "This is your fate!", "Beware!", "Death is what you choose."),
                        new Prioritize(
                        new Follow(.4, 8, 1),
                        new Wander(0.85)
                            ),
                        new Shoot(10, count: 12, shootAngle: 20, projectileIndex: 0, coolDown: 1350),
                        new Shoot(10, count: 11, shootAngle: 24, predictive: 1, projectileIndex: 3, coolDown: 1000),
                        new Shoot(10, count: 10, shootAngle: 24, predictive: 2, projectileIndex: 4, coolDown: 2000),
                        new Shoot(10, count: 5, projectileIndex: 2, coolDown: 900),
                        new TimedTransition(6750, "rush2")
                       ),
                    new State("rush2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt(1.00, "Farewell Hero!", "Your death is near."),
                        new Prioritize(
                        new Follow(.4, 8, 1),
                        new Wander(0.75)
                            ),
                        new Shoot(10, count: 20, shootAngle: 1, projectileIndex: 0, coolDown: 2000),
                        new TimedTransition(8000, "rush3")
                       ),
                   new State("rush3",
                        new Taunt(1.00, "Seems as if you've been fighting before.", "You have incredible skill."),
                        new Wander(0.65),
                        new Shoot(10, count: 7, shootAngle: 1, projectileIndex: 1, coolDown: 500),
                        new TimedTransition(2000, "fight8")
                       ),
                  new State("fight8",
                      new Flash(0x00FF00, 1, 8),
                        new ConditionalEffect(ConditionEffectIndex.ArmorBroken),
                        new TimedTransition(12750, "fight2")
                       ),
                   new State("deathbegins",
                       new Taunt(1.00, "My death will forever be in vein", "I must be strong to be honored", "I'll be back next time.", "Must keep a peaceful demeanor."),
                        new ReturnToSpawn(once: true, speed: 0.5),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0x0000FF, 1, 6),
                        new TimedTransition(8000, "Dead")
                        ),
                   new State("Dead",
                       new Shoot(10, count: 10, projectileIndex: 1, coolDown: 9999),
                       new Suicide()
                    )
                 )
            )
        .Init("1Warrior of the Halls1",
                new State("fight",
                     new Follow(0.8, 8, 1),
                     new Shoot(8.4, count: 6, shootAngle: 8, projectileIndex: 0, coolDown: 2000),
                    new Shoot(8.4, count: 8, projectileIndex: 0, coolDown: 4000)
                )
            )
         .Init("1Halls knight1",
                new State("fight",
                     new Wander(0.1),
                     new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450)
                )
            )
         .Init("LH Loot Chest",
            new State(
                new DropPortalOnDeath("Void Entity Portal", 100),
                new ScaleHP(3000),
          new State("timed",
          new ConditionalEffect(ConditionEffectIndex.Invincible),
          new TimedTransition(5000, "loot")
              ),
          new State("loot"
              )),
                 new MostDamagers(30,
                    new ItemLoot("Potion of Life", 0.58),
                    new ItemLoot("Potion of Life", 0.58),
                    new ItemLoot("Potion of Life", 0.58),
                    new ItemLoot("LH LootBox", 0.61),
                    new ItemLoot("Seal of Divine Hope", 0.01),
                    new ItemLoot("Magical Lodestone", 0.01),
                        new ItemLoot("Sword of the Colossus", 0.01),
                            new ItemLoot("Breastplate of New Life", 0.01)
                     ),
                 new MostDamagers(10,
                    LootTemplates.StatIncreasePotionsLoot()
                     ),

                     new MostDamagers(10,
                    LootTemplates.StatIncreasePotionsLoot()


            )

        )


         .Init("Cultist of the Halls",
                new State(
                    new ScaleHP(1000),
                     new DropPortalOnDeath("Dark Halls Portal", 80),
                    new State("default",
                        new PlayerWithinTransition(8, "awoken")
                        ),
                    new State("awoken",
                        new Flash(0xFF0000, 6, 6),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt(1.00, "You had a choice!", "Walk away while you can!", "I will not allow anyone near the halls!"),
                        new TimedTransition(4500, "FollowShootPhase")
                        ),
                    new State("FollowShootPhase",
                        new Follow(0.5, 8, 1),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(10, count: 9, projectileIndex: 1, coolDown: 2500),
                        new Shoot(10, count: 7, projectileIndex: 2, coolDown: 3088),
                        new Shoot(10, count: 2, projectileIndex: 3, coolDown: 1500),
                        new TimedTransition(6750, "FoollowShootPhase2")
                        ),
                    new State("FoollowShootPhase2",
                        new Follow(0.7, 8, 1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(10, count: 12, projectileIndex: 2, coolDown: 2500),
                        new Shoot(10, count: 13, projectileIndex: 5, coolDown: 150),
                        new TimedTransition(6000, "WanderAndRek")
                        ),
                    new State("WanderAndRek",
                        new Wander(0.8),
                        new Flash(0xFF0000, 6, 6),
                        new Shoot(10, count: 24, projectileIndex: 10, coolDown: 4000),
                        new TimedTransition(8000, "TellAndMove")
                        ),
                    new State("TellAndMove",
                        new ReturnToSpawn(once: true, speed: 0.5),
                        new Taunt(1.00, "Must Protect!", "Protect the halls!", "You will never enter!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(4000, "GoBlam")
                        ),
                    new State("GoBlam",
                        new Shoot(10, count: 1, shootAngle: 10, projectileIndex: 12, coolDown: 1, predictive: 0.5),
                        new Shoot(10, count: 2, shootAngle: 20, projectileIndex: 11, coolDown: 1, predictive: 0.5),
                        new Shoot(10, count: 3, shootAngle: 30, projectileIndex: 14, coolDown: 1, predictive: 0.5),
                        new Shoot(10, count: 10, shootAngle: 30, fixedAngle: 0, projectileIndex: 13, coolDown: 1, predictive: 0.5),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new SpecificHeal(1, 200, "Self", coolDown: 1000),
                        new TimedTransition(6000, "FollowShootPhase")
                        )
                       ),
                new GoldLoot(10, 120),
                 new Threshold(0.025,

                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Potion of Life", 0.2),
                    new ItemLoot("Cult Package", 1),
                    new ItemLoot("Robe of the Ancient Cultist", 0.01),
                    //LG
                    new ItemLoot("Staff of Unholy Sacrifice", 0.0094),
                    new ItemLoot("Seal of Divine Hope", 0.0094)
                     ),
                 new MostDamagers(10,
                    LootTemplates.StatIncreasePotionsLoot()
                    )
            )




        ;








    }
}