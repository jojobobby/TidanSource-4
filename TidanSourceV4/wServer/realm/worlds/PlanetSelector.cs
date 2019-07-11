#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class PlanetSelector : World
    {
        public PlanetSelector()
        {
            Name = "Planet Selector";
            ClientWorldName = "Planet Selector";
            Background = 1;
            AllowTeleport = true;
            IsLimbo = false;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.PlanetSelector.jm", MapType.Json);
        }

        public override World GetInstance(Client psr) => Manager.AddWorld(new PlanetSelector());
    }
}