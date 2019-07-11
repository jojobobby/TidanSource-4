#region


#endregion

namespace wServer.realm.worlds
{
    public class TheVoid : World
    {
        public TheVoid()
        {
            Name = "Void";
            ClientWorldName = "Void";
            Dungeon = true;
            Background = 1;
            AllowTeleport = false;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.Void.jm", MapType.Json);
        }
    }
}