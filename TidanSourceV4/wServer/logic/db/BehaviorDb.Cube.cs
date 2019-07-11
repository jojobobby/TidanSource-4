﻿using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        _ Cube = () => Behav()
            .Init("Cube God",
                new State(
                    new ScaleHP(1000),
                    new StayCloseToSpawn(0.3, range: 7),
                             new Shoot(10, count: 9, predictive: 0.9, shootAngle: 6.5, coolDown: 1000),
                             new Spawn("Cube Overseer", maxChildren: 2, initialSpawn: 2, coolDown: 100000),
                             new Spawn("Cube Defender", maxChildren: 1, initialSpawn: 2, coolDown: 100000),
                             new Spawn("Cube Blaster", maxChildren: 1, initialSpawn: 2, coolDown: 100000)
                ),
                new GoldLoot(10, 120),
                new MostDamagers(30,
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new ItemLoot("Dirk of Cronus", 0.01),
                    new ItemLoot("Royal Glutinous Hide", 0.01),
                    new ItemLoot("Ring of Pure Hastelloy", 0.01),
                    new ItemLoot("Potion of Dexterity", 0.5),
                    new ItemLoot("Necklace of Magic", 0.009),
                    new ItemLoot("Potion of Attack", 0.5)
                )
            )
            .Init("Cube Overseer",
                new State(
                    new EntityNotExistsTransition("Cube God", 100, "Suicide"),
                    new Orbit(.5, 3, target: "Cube God", acquireRange: 15, speedVariance: 0, radiusVariance: 0),
                    new State("1",
                    new StayCloseToSpawn(0.3, range: 7),
                    new Shoot(10, count: 4, predictive: 0.9, projectileIndex: 0, coolDown: 1250)
                    ),
                new State("Suicide",
                    new Suicide()
                )
            )
            )
            .Init("Cube Defender",
                new State(
                    new EntityNotExistsTransition("Cube God", 100, "Suicide"),
                    new Orbit(.6, 6, target: "Cube God", acquireRange: 15, speedVariance: 0, radiusVariance: 0),
                    new State("1",
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 1, coolDown: 1000, predictive: 0.9, projectileIndex: 0)
                    ),
                new State("Suicide",
                    new Suicide()
                )
            )
            )
            .Init("Cube Blaster",
                new State(
                    new EntityNotExistsTransition("Cube God", 100, "Suicide"),
                    new Orbit(.7, 9, target: "Cube God", acquireRange: 15, speedVariance: 0, radiusVariance: 0),
                    new State("1",
                             new StayCloseToSpawn(0.03, range: 7),
                             new Follow(0.4, acquireRange: 9, range: 2),
                             new Shoot(10, count: 2, predictive: 0.9, projectileIndex: 0, coolDown: 1500),
                             new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1500)
                    ),
                new State("Suicide",
                    new Suicide()
                )
                    )
            );
    }
}
