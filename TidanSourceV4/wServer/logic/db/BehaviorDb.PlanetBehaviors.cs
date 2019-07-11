using wServer.logic.behaviors;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ PlanetBehaviors = () => Behav()

        #region Nexus Misc

            .Init("Planet Crier",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Player Within",
                        new PlayerWithinTransition(5, "Speech")
                        ),
                        new State("Speech1",
                            new Taunt("Well, This rocket isn't done yet but once finished you'll beable to adventure far into the universe."),
                        new SetAltTexture(3)
                        ),
                        new State("Speech",
                        new SetAltTexture(3),
                        new Taunt("Hey adventurer, if you find rocket fuel you can power this rocket! the portal will be open for 30 seconds before it closes.", "This rocket allows you to explore multiple planets with all sorts of creatures, use rocket fuel to open it for 30 seconds."),
                        new TimedTransition(8000, "Return")
                             ),
                        new State("Activated",
                        new SetAltTexture(3),
                        new Taunt("The rocket has been fully powered up, go explore before the time runs out!.", "The rocket is ready to take off, hurry and go explore!"),
                        new TimedTransition(8000, "Activated")
                        ),
                        new State("Return",
                            new TimedTransition(10000, "Speech")

                            )
                )
            )
        .Init("Planet Space Ship",
                new State(
                    new Wander(0.3),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Player Within",
                        new Order(50, "Planet Crier", "Speech"),
                        new InvisiToss("Planet Portal Spawner", 0, 0, 90000000, coolDownOffset: 0)
                        ),
                    new State("portal",
                        new Taunt("Space Ship (Activated)"),
                        new Order(50, "Planet Portal Spawner", "spawn"),
                        new Order(50, "Planet Crier", "Activated"),
                        new TimedTransition(1799999, "Player Within")



                            )
                )
            )
         .Init("Planet Portal Spawner",
                new State(
                    new DropPortalOnDeath("LightSpeed Selector", percent: 100, dropDelaySec: 0, XAdjustment: 0, YAdjustment: 0, PortalDespawnTimeSec: 1800),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("nothing"
                        ),
                    new State("spawn",
                        new Suicide()
                            )
                )
            )

        .Init("Planet Portal Spawned",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("nothing",
                         new Order(50, "Planet Space Ship", "portal"),
                          new TimedTransition(9999, "spawn")
                        ),
                    new State("spawn",
                        new Suicide()
                            )
                )
            )
        #endregion
        #region Moon

        .Init("Moon Crier",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("Player Within",
                        new Order(5000, "Invisible Event Spawner [moon]", "EventS1"),
                        new PlayerWithinTransition(5, "Player Within2")
                        ),
                    new State("Player Within2",
                     new PlayerWithinTransition(5, "Speech")
                        ),
                        new State("Speech",
                        new SetAltTexture(3),
                        new Taunt("Whew, I thought I'd be stuck on this cold planet all by myself. Welcome to the moon!", "You must have been able to power the rocket! This place is what we call the moon!."),
                        new TimedTransition(10000, "Player Within2")


                            )
                )
            )

        #endregion
        #region EventSpawner [Moon]

           .Init("Invisible Event Spawner 1 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 1 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                          new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner 2 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 2 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                           new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner 3 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 3 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                          new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner 4 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 4 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                         new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner 5 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 5 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                           new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner 6 [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner 6 [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                         new NoPlayerWithinTransition(1, "Nothing")
                        ),
                    new State("Nothing",
                        new Wander(0.3)
                        ),
                    new State("start",
                        new TimedTransition(1500, "Ghost King", randomized: true)
                        //new TimedTransition(1500, "EventReplace2", randomized: true),
                        //new TimedTransition(1500, "EventReplace3", randomized: true)
                        ),
                     new State("Ghost King",
                           new Taunt(true,
                             "Ghost King has been informed of visitors!",
                             "The Ghost King shall make quick work of these Visitors!"
                             ),
                        new TossObject("Ghost King", 4, 0, 9999999),
                        new TimedTransition(1500, "Nothing")


                )
            )
            )
           .Init("Invisible Event Spawner [moon]",
                new State(
                    new TransformOnDeath("Invisible Event Spawner [moon]"),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                    new State("EventS177",
                        new TossObject("Invisible Event Spawner 1 [moon]", 250, 9999999),
                        new TossObject("Invisible Event Spawner 2 [moon]", 250, 9999999),
                        new TossObject("Invisible Event Spawner 3 [moon]", 250, 9999999),
                        new TossObject("Invisible Event Spawner 4 [moon]", 250, 9999999),
                        new TossObject("Invisible Event Spawner 5 [moon]", 250, 9999999),
                        new TossObject("Invisible Event Spawner 6 [moon]", 250, 9999999),
                         new NoPlayerWithinTransition(1, "EventS1")
                        ),
                    new State("EventS1",
                        new Order(500, "Invisible Event Spawner 1 [moon]", "start"),
                        new TimedTransition(15000, "EventS1(Complete)")
                        ),
                    new State("EventS1(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 500, "EventS2")
                        ),
                    new State("EventS2",
                        new Order(500, "Invisible Event Spawner 2 [moon]", "start"),
                        new TimedTransition(15000, "EventS2(Complete)")
                        ),
                    new State("EventS2(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 500, "EventS3")
                        ),
                   new State("EventS3",
                        new Order(500, "Invisible Event Spawner 3 [moon]", "start"),
                        new TimedTransition(15000, "EventS3(Complete)")
                        ),
                    new State("EventS3(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 500, "EventS4")
                        ),
                   new State("EventS4",
                        new Order(500, "Invisible Event Spawner 4 [moon]", "start"),
                        new TimedTransition(15000, "EventS4(Complete)")
                        ),
                    new State("EventS4(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 500, "EventS5")
                        ),
                   new State("EventS5",
                        new Order(500, "Invisible Event Spawner 5 [moon]", "start"),
                        new TimedTransition(15000, "EventS5(Complete)")
                        ),
                    new State("EventS5(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 500, "EventS6")
                        ),
                   new State("EventS6",
                        new Order(500, "Invisible Event Spawner 6 [moon]", "start"),
                        new TimedTransition(15000, "EventS6(Complete)")
                        ),
                    new State("EventS6(Complete)",
                        new EntityNotExistsTransition2("Ghost King", "Ghost King", 5000, "EventS1")
                        )



                )
            );



        #endregion;




    }
}