#region


#endregion

namespace wServer.realm.worlds
{
    public class Library : World
    {
        public Library()
        {
            Name = "Library";
            ClientWorldName = "Library";
            Dungeon = true;
            Background = 0;
            AllowTeleport = false;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.Library.jm", MapType.Json);
        }
    }
}