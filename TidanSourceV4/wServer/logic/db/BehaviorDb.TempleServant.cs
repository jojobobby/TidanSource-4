using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ TempleServant = () => Behav()

         .Init("Stone ball2",
                new State(
                    new Orbit(1, 2.2, target: "Servant of the Temple", radiusVariance: 0.5),
                    new State("Start",
                        new Shoot(20, 2, 0, 0, 0, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 60, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 120, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 180, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 240, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 300, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 0, coolDown: 100, coolDownOffset: 100),
                        new TimedTransition(0, "Start")
                        ),
                         new State("Start2",
                              new Orbit(1, 2.2, target: "Servant of the Temple", radiusVariance: 0.5),
                             new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(20, 2, 0, 0, 0, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 60, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 120, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 180, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 240, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 300, coolDown: 100, coolDownOffset: 100),
                        new Shoot(20, 2, 0, 0, 0, coolDown: 100, coolDownOffset: 100),
                        new TimedTransition(0, "Start2")
                               ),
                           new State("Start3",
                             new Suicide()
                        )
                    )
            )
          .Init("Stone ball1",
                new State(
                    new Orbit(1, 2.2, target: "Servant of the Temple", radiusVariance: 0.5),
                    new State("Start",
                        new Shoot(20, 2, 0, 0, 0),
                        new Shoot(20, 2, 0, 0, 60),
                        new Shoot(20, 2, 0, 0, 120),
                        new Shoot(20, 2, 0, 0, 180),
                        new Shoot(20, 2, 0, 0, 240),
                        new Shoot(20, 2, 0, 0, 300),
                        new Shoot(20, 2, 0, 0, 0),
                        new TimedTransition(0, "Start")
                        ),
                         new State("Start2",
                              new Orbit(1, 2.2, target: "Servant of the Temple", radiusVariance: 0.5),
                             new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(20, 2, 0, 0, 0),
                        new Shoot(20, 2, 0, 0, 60),
                        new Shoot(20, 2, 0, 0, 120),
                        new Shoot(20, 2, 0, 0, 180),
                        new Shoot(20, 2, 0, 0, 240),
                        new Shoot(20, 2, 0, 0, 300),
                        new Shoot(20, 2, 0, 0, 0),
                        new TimedTransition(0, "Start2")
                             ),
                           new State("Start3",
                             new Suicide()
                        )
                    )
            )
                .Init("Servant Protector",
                new State(
                    new Wander(0.5),
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 2, predictive: 0.9, projectileIndex: 0, coolDown: 1500),
                             new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1500)
                )
            )
        .Init("Servant Healer",
                new State(
                    new Wander(0.5),
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 2, predictive: 0.9, projectileIndex: 0, coolDown: 1500),
                             new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1500)
                )
            )
        .Init("Servant of the Temple",
             new State(
                 new ScaleHP(1000),
                 new State("atkme",
                     new HpLessTransition(.99, "Greedings")
                     ),
             new State("Greedings",
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new Taunt("I am the servant of the temple."),
                 new TimedTransition(1800, "Greedings2")
        ),
             new State("Greedings2",
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new Taunt("My master wants you dead."),
                 new TimedTransition(1800, "Greedings3")
        ),
             new State("Greedings3",
                 new ConditionalEffect(ConditionEffectIndex.Invincible),
                 new Taunt("Prepare yourself."),
                 new TimedTransition(1800, "Attacking")
        ),
             new State("Attacking",
                 new ConditionalEffect(ConditionEffectIndex.Armored),
                 new Flash(0xff0000, 0.1, 10),
                 new TimedTransition(1500, "Attacking1")
        ),
             new State("Attacking1",
                 new ConditionalEffect(ConditionEffectIndex.StunImmune),
                  new Shoot(25, projectileIndex: 0, count: 8, shootAngle: 45, coolDown: 1500, coolDownOffset: 1500),
                        new Shoot(25, projectileIndex: 1, count: 3, shootAngle: 10, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 2, count: 3, shootAngle: 10, predictive: 0.2, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 3, count: 2, shootAngle: 10, predictive: 0.4, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 4, count: 5, shootAngle: 10, predictive: 0.6, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000, coolDownOffset: 1000),
                        new HpLessTransition(.56, "Attacking2/21")
                 ),
             new State("Attacking2/21",
new ConditionalEffect(ConditionEffectIndex.Invincible),
new Taunt("You still fail to understand. You cannot win."),
new TimedTransition(2100, "Attacking2/2")
        ),
              new State("Attacking2/2",
                  new ConditionalEffect(ConditionEffectIndex.Armored),
                   new ConditionalEffect(ConditionEffectIndex.StunImmune),
                   new TossObject("Stone ball2", 2, 270, coolDown: 9999999, randomToss: false),
                   new TossObject("Stone ball1", 2, -270, coolDown: 9999999, randomToss: false),
                   new Flash(0xfFF0000, 1, 9000001),
                   new Shoot(50, 5, 10, 1, coolDown: 4750, coolDownOffset: 500),
                   new Shoot(50, 9, 10, 4, coolDown: 300),
                   new Shoot(50, 6, 10, 0, coolDown: 4750, coolDownOffset: 500),
                     new HpLessTransition(.26, "active")
                  ),
              new State("active",
                  new ConditionalEffect(ConditionEffectIndex.Invincible),
                  new Taunt("Hahaha, you must be hurt. Enjoy this pain."),
                  new Order(20, "Stone ball2", "Start3"),
                  new Order(20, "Stone ball1", "Start3"),
                  new TimedTransition(2100, "active2")
                  ),
              new State("active2",
                  new ConditionalEffect(ConditionEffectIndex.Armored),
                  new Grenade(3, 120, 10, coolDown: 2000),
                  new TossObject("Stone ball2", 2, 270, coolDown: 9999999, randomToss: false),
                  new TossObject("Stone ball1", 2, -270, coolDown: 9999999, randomToss: false),
                  new Shoot(25, projectileIndex: 4, count: 5, shootAngle: 10, predictive: 0.6, coolDown: 1000, coolDownOffset: 1000),
                        new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000, coolDownOffset: 1000),
                  new Shoot(25, projectileIndex: 0, count: 3, shootAngle: 10, predictive: 1, coolDown: 1000, coolDownOffset: 1000),
                  new Order(20, "Stone ball2", "Start2"),
                  new Order(20, "Stone ball1", "Start2"),
                  new HpLessTransition(.07, "aSuicide")

               ),
              new State("aSuicide",
                  new Order(20, "Stone ball2", "Start3"),
                  new Order(20, "Stone ball1", "Start3"),
                  new ConditionalEffect(ConditionEffectIndex.Invincible),
                  new Taunt("You'll pay for what you did."),
                   new Flash(0xfFF0000, 1, 9000001),
                  new TimedTransition(2100, "Suicide")
              ),
                    new State("Suicide",
                        new Suicide()
                        )
                ),
               new MostDamagers(30,

                    new ItemLoot("Servant's Scepter", 0.01),
                    new ItemLoot("Lightning Chamber", 0.01),
                    new ItemLoot("Ring of Ancient Slaves", 0.01),
                    new ItemLoot("Unbound Temple Armor", 0.01),
                    new ItemLoot("Seal of the Royal Priest", 0.01),
                    new ItemLoot("Sword of Royal Majesty", 0.01)
                ),
               new MostDamagers(30,
                    new ItemLoot("Potion of Life", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Vitality", 1)
                ),
                new MostDamagers(30,
                    new TierLoot(11, ItemType.Weapon, 0.08),
                    new TierLoot(12, ItemType.Weapon, 0.07),
                    new TierLoot(13, ItemType.Weapon, 0.06),
                    new TierLoot(5, ItemType.Ability, 0.08),
                    new TierLoot(6, ItemType.Ability, 0.06),
                    new TierLoot(12, ItemType.Armor, 0.08),
                    new TierLoot(13, ItemType.Armor, 0.07),
                    new TierLoot(6, ItemType.Ring, 0.07)
                 )

            )





        ;

    }
}
