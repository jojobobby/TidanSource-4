using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Omen = () => Behav()
         .Init("The Haunted Omen",
                new State(
                    new ScaleHP(500),
                    new TransformOnDeath("OM Loot Chest", 1, 1, 1),
                    new HpLessTransition(0.12, "DyingPhase"),
                    new State("idle",
                         new HpLessTransition(0.99, "Awakening")
                        ),
                    new State("Awakening",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFFFFFF, 2, 3),
                        new TimedTransition(4768, "backandforth")
                        ),
                  new State("backandforth",
                      new Shoot(10, count: 8, shootAngle: 28, projectileIndex: 6, coolDown: 3451),
                      new Shoot(10, count: 5, shootAngle: 28, projectileIndex: 5, coolDown: 2600),
                      new Shoot(10, count: 3, shootAngle: 28, projectileIndex: 4, coolDown: 2982),
                      new TimedTransition(8875, "spawnshades"),
                       new State("2",
                        new Taunt(1.00, "There is no mercy here.", "Prepare to meet your doom!"),
                        new Prioritize(
                             new Follow(0.34, 8, 1),
                             new StayBack(0.3, 5)
                            ),
                        new Shoot(10, count: 12, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 6, shootAngle: 60, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 2, projectileIndex: 1, coolDown: 1500),
                        new TimedTransition(3250, "1")
                        ),
                     new State("1",
                        new Grenade(5, 100, range: 6, coolDown: 2000),
                        new ReturnToSpawn(once: false, speed: 0.4),
                        new Shoot(10, count: 8, shootAngle: 28, projectileIndex: 6, coolDown: 2600),
                        new Shoot(10, count: 2, shootAngle: 36, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 8, shootAngle: 60, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 2, projectileIndex: 1, coolDown: 1500),
                        new TimedTransition(3250, "2")
                         )
                        ),
                    new State("spawnshades",
                        new TimedTransition(500, "shadesandrek")
                        ),
                  new State("shadesandrek",
                                            new Shoot(10, count: 8, shootAngle: 28, projectileIndex: 6, coolDown: 3451),
                      new Shoot(10, count: 5, shootAngle: 28, projectileIndex: 5, coolDown: 2600),
                      new Shoot(10, count: 3, shootAngle: 28, projectileIndex: 4, coolDown: 2982),
                      new TimedTransition(12875, "returntospawn"),
                    new State("1",
                        new SetAltTexture(0),
                        new Taunt(0.80, "I shall rip you apart.", "Your life has come to a end."),
                        new Prioritize(
                             new Follow(0.34, 8, 1),
                             new StayBack(0.3, 5)
                            ),
                        new Shoot(10, count: 12, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 6, shootAngle: 60, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 2, projectileIndex: 1, coolDown: 1000),
                        new Shoot(10, count: 2, projectileIndex: 3, predictive: 2, coolDown: 2500),
                        new TimedTransition(3250, "2")
                        ),
                     new State("2",
                        new ReturnToSpawn(once: false, speed: 0.4),
                        new Shoot(10, count: 2, shootAngle: 36, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 8, shootAngle: 60, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 2, projectileIndex: 1, coolDown: 1500),
                        new Shoot(10, count: 2, projectileIndex: 3, predictive: 2, coolDown: 1700),
                        new Grenade(5, 100, range: 6, coolDown: 2000),
                        new TimedTransition(3250, "1")
                         )
                        ),
                    new State("returntospawn",
                        new ReturnToSpawn(once: true, speed: 0.5),
                        new Shoot(10, count: 4, shootAngle: 36, projectileIndex: 3, coolDown: 3000),
                        new TimedTransition(2000, "invisibleattack")
                        ),
                      new State("invisibleattack",
                       new Shoot(10, count: 8, shootAngle: 28, projectileIndex: 4, coolDown: 2600),
                       new Wander(0.1),
                       new ConditionalEffect(ConditionEffectIndex.Invincible),
                       new Shoot(10, count: 2, shootAngle: 36, projectileIndex: 0, coolDown: 3000),
                        new Shoot(10, count: 8, shootAngle: 60, projectileIndex: 2, coolDown: 2000),
                        new Shoot(10, count: 2, projectileIndex: 1, coolDown: 1500),
                        new Shoot(10, count: 2, projectileIndex: 3, predictive: 2, coolDown: 1700),
                        new TimedTransition(7000, "backandforth")
                        ),
                   new State(
                      new ConditionalEffect(ConditionEffectIndex.Invincible),
                      new State("DyingPhase",
                        new ReturnToSpawn(once: true, speed: 0.3),
                        new Flash(0xFFFFFF, 2, 3),
                        new Taunt(1.00, "What a wonderful hero you are!", "I will see you soon, mortal."),
                        new TimedTransition(4750, "restindark")
                        ),
                       new State("restindark",
                        new Suicide()
                        )
                      )
                    )
            )
            .Init("OM Loot Chest",
            new State(
          new State("timed",
          new ConditionalEffect(ConditionEffectIndex.Invincible),
          new TimedTransition(5000, "loot")
              ),
          new State("loot"
              )),
                 new MostDamagers(30,
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Omen LootBox", 1),
                    new ItemLoot("Premise of Possibilities", 0.01),
                    new ItemLoot("Candle of Metallic Flames", 0.01),
                    new ItemLoot("Potential Seeking Crystal", 0.01),
                    //LG
                    new ItemLoot("Blood Vial Necklace", 0.0094)
                     ),
                 new MostDamagers(10,
                    LootTemplates.StatIncreasePotionsLoot()
                     ),

                     new MostDamagers(10,
                    LootTemplates.StatIncreasePotionsLoot()


            )

        )
            ;
    }
}