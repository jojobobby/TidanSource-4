using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        _ Shaits = () => Behav()
            .Init("md1 Head of Shaitan",
                new State(
                    new ScaleHP(4500),
                    new State("TrueStart",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new TimedTransition(3300, "Start")
                        ),
                new State("Start",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new HpLessTransition(0.06, "Suicide"),
                    new InvisiToss("md1 Right Hand of Shaitan", 4, 180, 999999),
                    new InvisiToss("md1 Left Hand of Shaitan", 4, 360, 999999),
                    new Taunt("Oryx has made a mistake letting you pass through here!"),
                    new Flash(0xffffffff, 2, 100),
                    new TimedTransition(3300, "Start3.2")
                    ),
                new State("Start.1",
                    new MoveTo(15, 7, 1, false, true, false),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new HpLessTransition(0.06, "Suicide"),
                    new Flash(0xffffffff, 2, 100),
                    new Taunt("You're starting to get me mad!", "You're going to regret that!", "You're getting annoying!", "This is enough!", "You dare to challenge me!"),
                    new TimedTransition(3300, "Start2")
                    ),
                 new State("Start2",
                     new InvisiToss("md1 Right Hand of Shaitan", 4, 180, 999999),
                    new InvisiToss("md1 Left Hand of Shaitan", 4, 360, 999999),
                     new HpLessTransition(0.06, "Suicide"),
                     new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(10, count: 5, predictive: 0.2, coolDown: 1000, shootAngle: 10.5),
                new TimedTransition(2200, "Start3.2")
                      ),
                  new State("Start3.2",
                     new HpLessTransition(0.06, "Suicide"),
                     new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(10, count: 5, predictive: 0.2, coolDown: 1000, shootAngle: 10.5),
                new EntityNotExistsTransition2("md1 Right Hand of Shaitan", "md1 Left Hand of Shaitan", 20, "Start3")
                  //    ),
              //   new State("Start3",
                 //    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                   //  new Shoot(10, count: 5, predictive: 0.2, coolDown: 1000, shootAngle: 10.5),
                //     new EntityNotExistsTransition2("md1 Right Hand of Shaitan", "md1 Right Hand of Shaitan", 20, "Start3")
                      ),
                 new State("Start3",
                    new MoveTo(23, 8, 1, false, true, false),
                    new HpLessTransition(0.06, "Suicide"),
                    new Shoot(10, count: 5, predictive: 0.2, coolDown: 700, shootAngle: 10.5),//md placer1
                    new TimedTransition(3200, "Start4")
                ),
                  new State("Start4",
                    new MoveTo(15, 8, 1, false, true, false),
                    new Shoot(10, count: 5, predictive: 0.2, coolDown: 700, shootAngle: 10.5),//md placer2
                    new HpLessTransition(0.06, "Suicide"),
                    new TimedTransition(3200, "Start5")
                ),
                   new State("Start5",
                 new MoveTo(8, 8, 1, false, true, false),
                    new Shoot(10, count: 5, predictive: 0.2, coolDown: 700, shootAngle: 10.5),//md placer3
                    new HpLessTransition(0.06, "Suicide"),
                    new TimedTransition(3200, "Start.1")
                 ),
                   new State("Suicide",
                       new Taunt("Oryx shall know of your wrong doings!"),
                       new ReturnToSpawn(true, 2),
                       new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                       new TimedTransition(1900, "Suicide2")
                        ),
                   new State("Suicide2",
                       new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                       new TossObject("md1 Loot Balloon Shaitan", 6, 90, 999999),
                       new TimedTransition(1000, "Suicide3")
                       ),
                   new State("Suicide3",
                       new Suicide()
                       )
                    )
            )


           .Init("md1 Loot Balloon Shaitan",
                new State(
                    new State("1"
                )
            ),
                    new MostDamagers(30,
                    new TierLoot(4, ItemType.Ring, 0.2),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(5, ItemType.Ring, 0.05),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.03),
                    new TierLoot(11, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.02),
                    new TierLoot(12, ItemType.Armor, 0.01),
                    new TierLoot(12, ItemType.Weapon, 0.01),
                    new TierLoot(6, ItemType.Ring, 0.01),//Amulet of Cursed Death
                    new ItemLoot("Skull of Endless Torment", 0.01),
                    new ItemLoot("Amulet of Cursed Death", 0.0075),
                    new ItemLoot("Potion of Life", 0.75),
                    new ItemLoot("Potion of Attack", 0.75),
                    new ItemLoot("Potion of Attack", 1)
                )
            )


            .Init("md1 Right Hand of Shaitan",
                new State(
                    new State("spawned",
                        new Shoot(10, count: 4, coolDown: 1000, projectileIndex: 0, shootAngle: 10.5),
                        new TimedTransition(2000,"1")
                        ),
                    new State("1",
                           new StayCloseToSpawn(0.03, range: 7),
                           new EntityNotExistsTransition("md1 Left Hand of Shaitan", 100, "rush"),
                             new Shoot(10, count: 4, coolDown: 1000, projectileIndex: 0, shootAngle: 10.5)
                    ),
                new State("rush",
                    new Follow(1.5, acquireRange: 7, range: 0.5),
                    new Shoot(10, count: 8, predictive: 0.2, projectileIndex: 2, coolDown: 650)
                )
            )
            )
            .Init("md1 Left Hand of Shaitan",
                new State(
                       new State("spawned",
                        new Shoot(10, count: 4, coolDown: 1000, projectileIndex: 0, shootAngle: 10.5),
                        new TimedTransition(2000, "1")
                        ),
                    new State("1",
                             new StayCloseToSpawn(0.03, range: 7),
                             new EntityNotExistsTransition("md1 Right Hand of Shaitan", 100, "rush"),
                             new Shoot(10, count: 4, coolDown: 1000, projectileIndex: 0, shootAngle: 10.5)
                    ),
                new State("rush",
                    new Follow(1.5, acquireRange: 7, range: 0.5),
                    new Shoot(10, count: 8, predictive: 0.2, projectileIndex: 2, coolDown: 650)
                )
            )
            )
            .Init("md1 Right Hand spawner",
                new State(
                    new State("nothing",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                    ),
                new State("1235",
                   new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                  new Spawn("md1 Right Hand of Shaitan", 1, 0.5, 999999)
                      
                )
                    )

            )
             .Init("md1 Left Hand spawner",
                new State(
                    new State("nothing",
                new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                    ),
                new State("1235",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Spawn("md1 Left Hand of Shaitan", 1, 0.5, 999999)
                    
                )
                    )

            );//You better not say Tidan made this cause he surely did... yw for my sloppy work
    }
}
