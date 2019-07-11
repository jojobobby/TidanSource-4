using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ EpicSpiderDen = () => Behav()
            .Init("Son of Arachna",
                 new State(
                     new ScaleHP(1000),
                    new DropPortalOnDeath("Glowing Realm Portal", 100, PortalDespawnTimeSec: 360),
                    new State("Start",
                        new EntityNotExistsTransition("Red Son of Arachna Giant Egg Sac", 1000, "Start2"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                     ),
                    new State("Start2",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new EntityNotExistsTransition("Yellow Son of Arachna Giant Egg Sac", 1000, "idle")
                        ),
                     new State("idle",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(12, "WEB!")
                         ),
                     new State("WEB!",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new TossObject("Arachna Web Spoke 7", 6, 0, 100000),
                         new TossObject("Arachna Web Spoke 8", 6, 120, 100000),
                         new TossObject("Arachna Web Spoke 9", 6, 240, 100000),
                         new TossObject("Arachna Web Spoke 1", 10, 0, 100000),
                         new TossObject("Arachna Web Spoke 2", 10, 60, 100000),
                         new TossObject("Arachna Web Spoke 3", 10, 120, 100000),
                         new TossObject("Arachna Web Spoke 4", 10, 180, 100000),
                         new TossObject("Arachna Web Spoke 5", 10, 240, 100000),
                         new TossObject("Arachna Web Spoke 6", 10, 300, 100000),
                         new TimedTransition(2000, "attack")
                         ),
                     new State("attack",
                         new Wander(0.3),
                         new Shoot(3000, count: 12, projectileIndex: 0, fixedAngle: fixedAngle_RingAttack2),
                         new Shoot(10, 1, 0, defaultAngle: 0, angleOffset: 0, projectileIndex: 0, predictive: 1,
                         coolDown: 1000, coolDownOffset: 0),
                         new Shoot(10, 1, 0, defaultAngle: 0, angleOffset: 0, projectileIndex: 1, predictive: 1,
                         coolDown: 2000, coolDownOffset: 0)
                         )
                         ),
                new MostDamagers(30,
                    new ItemLoot("Potion of Mana", 1),
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Doku No Ken", 0.017),
                    new ItemLoot("AssassinST0", 0.007),
                    new ItemLoot("AssassinST1", 0.007),
                    new ItemLoot("AssassinST2", 0.007),
                    new ItemLoot("AssassinST3", 0.007)
                     )
            )

        .Init("Crawling Green Spider",
            new State(
                new State("idle",
                    new Wander(0)
                         )
                     ),
                    new ItemLoot("Healing Ichor", 0.2)
            )

        .Init("Crawling Grey Spider",
            new State(
                new State("idle",
                    new Wander(0)
                         )
                     ),
                    new ItemLoot("Healing Ichor", 0.2)
            )
       .Init("Red Son of Arachna Giant Egg Sac",
            new State(
                new State("idle"
                    )
                ),
                new Threshold(0.32,
                new ItemLoot("Potion of Mana", 0.25),
                new ItemLoot("Healing Ichor", 2),
                new ItemLoot("Doku No Ken", 0.006)

                    )
            )

       .Init("Yellow Son of Arachna Giant Egg Sac",
            new State(
                new State("idle"
                    )
                ),
            new Threshold(0.32,
                new ItemLoot("Potion of Mana", 0.25),
                new ItemLoot("Healing Ichor", 2),
                new ItemLoot("Doku No Ken", 0.006)

                    )
            );
    }
}
