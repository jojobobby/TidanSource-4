#region

using wServer.logic.behaviors;
using wServer.logic.transitions;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Nexus = () => Behav()
            .Init("Nexus Crier",
            new State(
            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new State("Active",
                    new Taunt("Welcome to the nexus, rest here or just chat with friends."),
                    new TimedTransition(8500, "Return")
                        ),
                new State("Return",
                    new Taunt("https://discord.gg/7YbFyW7 Join the discord to get the most recent patch notes"),
                    new TimedTransition(8500, "Active")

                    )
                )

              );
    }
}