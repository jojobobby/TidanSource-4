#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class Grassland : World
    {
        public Grassland()
        {
            Name = "The Grassland";
            ClientWorldName = "The Grassland";
            Dungeon = true;
            Background = 1;
            AllowTeleport = true;
        }


        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.GrassLand.jm", MapType.Json);
        }

        public override World GetInstance(Client psr)
        {
            return Manager.AddWorld(new Grassland());
        }
    }
}