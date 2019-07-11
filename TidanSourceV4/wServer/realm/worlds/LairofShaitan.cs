#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class LairofShaitan : World
    {
        public LairofShaitan()
        {
            Name = "Lair of Shaitan";
            ClientWorldName = "Lair of Shaitan";
            Background = 0;
            AllowTeleport = true;
            Dungeon = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.shaitansmap2.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new LairofShaitan());
        }
    }
}