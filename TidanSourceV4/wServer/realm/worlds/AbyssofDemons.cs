#region

using wServer.networking;

#endregion

namespace wServer.realm.worlds
{
    public class AbyssofDemons : World
    {
        public AbyssofDemons()
        {
            Name = "Abyss of Demons";
            ClientWorldName = "{dungeons.Abyss_of_Demons}";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        public override bool NeedsPortalKey => true;

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.Abyss.jm", MapType.Json);
        }
        public override World GetInstance(Client psr) => Manager.AddWorld(new AbyssofDemons());
    }
}