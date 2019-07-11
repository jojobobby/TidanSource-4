using wServer.logic.behaviors;
using wServer.logic.behaviors.Drakes;
using wServer.logic.transitions;


namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Specialized = () => Behav()
            .Init("Spirit Prism Bomb",
                new State(
                    new State("Idle",
                        new TimedTransition(1000, "Explode")
                    ),
                    new State("Explode",
                        new Prioritize(
                            new StayCloseToSpawn(3, 3)
                        ),
                        new State("Explode 1",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new ChangeSize(100, 0),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new TimedTransition(100, "Explode 2")
                        ),
                        new State("Explode 2",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new TimedTransition(100, "Explode 3")
                        ),
                        new State("Explode 3",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new TimedTransition(100, "Explode 4")
                        ),
                        new State("Explode 4",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new TimedTransition(100, "Explode 5")
                        ),
                        new State("Explode 5",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new TimedTransition(100, "Explode 6")
                        ),
                        new State("Explode 6",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(1, false, 40, 90, false, 0xFF9933),
                            new Decay(0)
                        )
                    )
                )
            )


            .Init("Big Firecracker",
                new State(
                    new State("Explode",
                        new Prioritize(
                            new StayCloseToSpawn(3, 3)
                        ),
                        new State("Explode 1",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(250, "Explode 2")
                        ),
                        new State("Explode 2",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 3")
                        ),
                        new State("Explode 3",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 4")
                        ),
                        new State("Explode 4",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 5")
                        ),
                        new State("Explode 5",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 6")
                        ),
                        new State("Explode 6",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 7")
                        ),
                        new State("Explode 7",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 8")
                        ),
                        new State("Explode 8",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 9")
                        ),
                        new State("Explode 9",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 10")
                        ),
                        new State("Explode 10",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(2, 4), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new Decay(0)
                        )
                    )
                )
            )
         .Init("Candle of Metallic FlamesT7",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 550, 950, true, 0x7a7a7a),
                    new TimedTransition(5000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 550, 950, true, 0x7a7a7a),
                    new TimedTransition(5000, "Idle")
                        )
                    )
            )
            .Init("Baleful Beacon UT",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new GreenDrakeAttack(),
                        new Aoe(10, false, 250, 300, true, 0xFF9933),
                    new TimedTransition(2000, "Idle2")
                        ),
                    new State("Idle2",
                        new GreenDrakeAttack(),
                        new Aoe(10, false, 250, 300, true, 0xFF9933),
                    new TimedTransition(2000, "Idle")
                        )
                    )
            )
               .Init("luring spiritsT7",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 450, 530, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 450, 530, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
        .Init("luring spiritsT6",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 390, 450, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 390, 450, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("luring spiritsT5",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 310, 380, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 310, 380, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("luring spiritsT4",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 240, 300, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 240, 300, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("luring spiritsT3",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 190, 240, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 190, 240, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("luring spiritsT2",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 120, 180, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 120, 180, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("luring spiritsT1",
                new State(
                    new DrakeFollow(),
                    new State("Idle",
                        new Aoe(10, false, 90, 130, true, 0xFF9933),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Aoe(10, false, 90, 130, true, 0xFF9933),
                    new TimedTransition(3000, "Idle")
                        )
                    )
            )
         .Init("LegSoulTHing",
                new State(
                    new State("Idle",
                        new Aoe(6, false, 600, 650, true, 0x3c7c5c),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
         .Init("LegSoulTHing21",
                new State(
                    new State("Idle",
                        new Aoe(6, false, 400, 500, true, 0x3c7c5c),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
         .Init("Tier6A",
                new State(
                    new State("Idle",
                        new Aoe(6, false, 650, 700, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
        .Init("Tier5A",
                new State(
                    new State("Idle",
                        new Aoe(5.5, false, 600, 650, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
        .Init("Tier4A",
                new State(
                    new State("Idle",
                        new Aoe(5, false, 500, 550, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
         .Init("Tier3A",
                new State(
                    new State("Idle",
                        new Aoe(4.5, false, 450, 500, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
        .Init("Tier2A",
                new State(
                    new State("Idle",
                        new Aoe(4, false, 400, 450, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
          .Init("Tier1A",
                new State(
                    new State("Idle",
                        new Aoe(3.5, false, 350, 400, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
        .Init("Tier0A",
                new State(
                    new State("Idle",
                        new Aoe(3, false, 300, 350, true, 0xff0000),
                    new TimedTransition(3000, "Idle2")
                        ),
                    new State("Idle2",
                        new Suicide()
                        )
                    )
            )
            .Init("Lil Firecracker",
                new State(
                    new State("Explode",
                        new Prioritize(
                            new StayCloseToSpawn(3, 3)
                        ),
                        new State("Explode 1",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 2")
                        ),
                        new State("Explode 2",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 3")
                        ),
                        new State("Explode 3",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 4")
                        ),
                        new State("Explode 4",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 5")
                        ),
                        new State("Explode 5",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 6")
                        ),
                        new State("Explode 6",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 7")
                        ),
                        new State("Explode 7",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 8")
                        ),
                        new State("Explode 8",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 9")
                        ),
                        new State("Explode 9",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new TimedTransition(100, "Explode 10")
                        ),
                        new State("Explode 10",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(Random.Next(1, 2), false, 100, 100, true, (uint)Random.Next(0x0000000, 0xFFFFFF)),
                            new Decay(0)
                        )
                    )
                )
            )
            .Init("Rock Candy Grenade",
                new State(
                    new State("Explode",
                        new Prioritize(
                            new StayCloseToSpawn(3, 3)
                        ),
                        new State("Explode 1",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 2")
                        ),
                        new State("Explode 2",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 3")
                        ),
                        new State("Explode 3",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 4")
                        ),
                        new State("Explode 4",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 5")
                        ),
                        new State("Explode 5",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 6")
                        ),
                        new State("Explode 6",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 7")
                        ),
                        new State("Explode 7",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 8")
                        ),
                        new State("Explode 8",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 9")
                        ),
                        new State("Explode 9",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new TimedTransition(100, "Explode 10")
                        ),
                        new State("Explode 10",
                            new JumpToRandomOffset(-2, 2, -2, 2),
                            new PlaySound(),
                            new Aoe(2, false, 100, 200, true, 0xFF6633),
                            new Decay(0)
                        )
                    )
                )
            )
            .Init("Zombie Trickster",
                new State(
                    new Wander(1)
                )
            )
            .Init("Realm Portal Opener",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible, true)
                )
            );
    }
}
