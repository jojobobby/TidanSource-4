#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class TheOmen : World
    {
        public TheOmen()
        {
            Name = "The Omen";
            ClientWorldName = "The Omen";
            Dungeon = true;
            Background = 1;
            AllowTeleport = true;
        }


        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.The_Omen.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new TheOmen());
        }
    }
}