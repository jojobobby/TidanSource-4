#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class LostHalls : World
    {
        public LostHalls()
        {
            Name = "Dark Halls";
            ClientWorldName = "Dark Halls";
            Dungeon = true;
            Background = 1;
            AllowTeleport = false;
        }


        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.LostHalls.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new LostHalls());
        }
    }
}