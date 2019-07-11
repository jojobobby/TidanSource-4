namespace wServer.realm.worlds
{
    public class Beachzone : World
    {
        public Beachzone()
        {
            Name = "Ultimate Arena";
            ClientWorldName = "Ultimate Arena";
            Background = 0;
            Difficulty = 0;
            ShowDisplays = true;
            AllowTeleport = false;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.beachzone.jm", MapType.Json);
        }
    }
}
