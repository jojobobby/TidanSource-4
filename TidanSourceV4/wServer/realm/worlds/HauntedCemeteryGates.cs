namespace wServer.realm.worlds
{
    public class CrystalCaverns : World
    {
        public CrystalCaverns()
        {
            Name = "Crystal Caverns";
            ClientWorldName = "Crystal Caverns";
            Background = 0;
            Difficulty = 4;
            AllowTeleport = true;
        }

        public override bool NeedsPortalKey => true;

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.HauntedCemeteryGates.jm", MapType.Json);
        }
    }
}
