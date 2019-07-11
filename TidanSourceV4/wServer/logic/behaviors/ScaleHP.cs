using System;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.behaviors
{
    internal class ScaleHP : Behavior
    {
        //State storage: none

        private readonly int amount;

        public ScaleHP(int amountperplayer)
        {
            amount = amountperplayer;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            if (state != null || host.Owner == null) return;

            state = true;

            (host as Enemy).HP += amount * Math.Max(host.Owner.Players.Count - 1, 0);
            (host as Enemy).MaximumHP += amount * Math.Max(host.Owner.Players.Count - 1, 0);
        }
    }

}