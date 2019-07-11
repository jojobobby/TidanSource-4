using db;
using System.Collections.Generic;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;


namespace wServer.networking.handlers
{
    internal class TinkerQuestHandler : PacketHandlerBase<TinkerQuestPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.TINKERQUEST; }
        }

        protected override void HandlePacket(Client client, TinkerQuestPacket packet)
        {
            using (Database db = new Database())
            {
                if (packet.Object.ObjectType == client.Player.Inventory[packet.Object.SlotId].ObjectType &&
                    (int)client.Player.Inventory[packet.Object.SlotId].ObjectType == Utils.FromString(client.Player.DailyQuest.Goal))
                {
                    client.SendPacket(new QuestRedeemResponsePacket
                    {
                        Success = true,
                        Message = client.Player.GetLanguageString("server.quest_complete")
                    });
                    client.Player.Inventory[packet.Object.SlotId] = null;

                    GiveRewards(db, client.Player.DailyQuest.Tier - 1);
                    var cmd = db.CreateQuery();
                    int tier = client.Player.DailyQuest.Tier == DailyQuestConstants.QuestsPerDay ? -1 : (client.Player.DailyQuest.Tier + 1);
                    cmd.CommandText = "UPDATE dailyquests SET tier=@tier WHERE accId=@accId;";
                    cmd.Parameters.AddWithValue("@accId", client.Account.AccountId);
                    cmd.Parameters.AddWithValue("@tier", tier);
                    int v = cmd.ExecuteNonQuery();
                    client.Player.DailyQuest = db.GetDailyQuest(client.Account.AccountId, Manager.GameData);
                    client.Player.UpdateCount++;
                    client.Player.SaveToCharacter();
                }
            }
        }



        private void GiveRewards(Database db, int index)
        {
            List<int> gifts = new List<int>();


            switch (DailyQuestConstants.Rewards[index])
            {
                case "FortuneToken:1":
                    Client.Player.Tokens = db.UpdateFortuneToken(Client.Account, +1);
                    break;
                case "FortuneToken:2":
                    Client.Player.Tokens = db.UpdateFortuneToken(Client.Account, +2);
                    break;
                case "FortuneToken:3":
                    Client.Player.Tokens = db.UpdateFortuneToken(Client.Account, +3);
                    break;
                case "Item:1":
                    gifts.Add(3276);
                    db.AddGifts(Client.Account, gifts);
                    break;
                case "Item:2":
                    gifts.Add(3290);
                    db.AddGifts(Client.Account, gifts);
                    break;
            }


        }
    }
}
