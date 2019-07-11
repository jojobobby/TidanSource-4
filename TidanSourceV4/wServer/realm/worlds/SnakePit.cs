﻿using wServer.networking;

namespace wServer.realm.worlds
{
    public class SnakePit : World
    {
        public SnakePit()
        {
            Name = "Snake Pit";
            ClientWorldName = "dungeons.Snake_Pit";
            Dungeon = true;
            Background = 0;
            AllowTeleport = true;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.SnakePit.jm", MapType.Json);
        }

        public override World GetInstance(Client client)
        {
            return Manager.AddWorld(new SnakePit());
        }
    }
}
