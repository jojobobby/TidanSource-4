﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;
using wServer.realm.entities;
using wServer.realm.entities.player;

#endregion

namespace wServer.realm.commands
{

    internal class LeftToMax : Command
    {
        public LeftToMax() : base("lefttomax") { }
        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            int Hp = player.ObjectDesc.MaxHitPoints - player.Stats[0];
            int Mp = player.ObjectDesc.MaxMagicPoints - player.Stats[1];
            int Atk = player.ObjectDesc.MaxAttack - player.Stats[2];
            int Def = player.ObjectDesc.MaxDefense - player.Stats[3];
            int Spd = player.ObjectDesc.MaxSpeed - player.Stats[4];
            int Vit = player.ObjectDesc.MaxHpRegen - player.Stats[5];
            int Wis = player.ObjectDesc.MaxMpRegen - player.Stats[6];
            int Dex = player.ObjectDesc.MaxDexterity - player.Stats[7];
            player.SendInfo(Hp + " HP");
            player.SendInfo(Mp + " MP");
            player.SendInfo(Atk + " ATK");
            player.SendInfo(Def + " DEF");
            player.SendInfo(Spd + " SPD");
            player.SendInfo(Vit + " VIT");
            player.SendInfo(Wis + " WIS");
            player.SendInfo(Dex + " DEX");
            return true;
        }
    }

    internal class GlandCommand : Command
    {
        public GlandCommand()
            : base("glands", 0)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (args.Length == 1000 || args.Length == 1000)
            {
                player.SendHelp("Usage: /glands to tp to glands");
            }
            else
            {
                if (player.Owner.Dungeon == true)
                {
                    player.SendError("Only usable in realm.");
                    return false;
                }

                int x, y;
                try
                {
                    x = int.Parse("1000");
                    y = int.Parse("1000");
                }
                catch
                {
                    player.SendError("Invalid coordinates!");
                    return false;
                }
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Invulnerable,
                    DurationMS = 3000
                });
                player.Move(x + 0.5f, y + 0.5f);
                if (player.Pet != null)
                    player.Pet.Move(x + 0.5f, y + 0.5f);
                player.UpdateCount++;
                player.Owner.BroadcastPacket(new GotoPacket
                {
                    ObjectId = player.Id,
                    Position = new Position
                    {
                        X = player.X,
                        Y = player.Y
                    }
                }, null);
            }
            return true;
        }
    }


    internal class SuicideCommand : Command
    {
        public SuicideCommand()
            : base("nuce")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.HP = 0;
            player.Death("./nuce command"); //what is will say has killed you
            return true;
        }
    }
    internal class TutorialCommand : Command
    {
        public TutorialCommand()
            : base("tutorial")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.Client.Reconnect(new ReconnectPacket
            {
                Host = "",
                Port = Program.Settings.GetValue<int>("port"),
                GameId = World.TUT_ID,
                Name = "Tutorial",
                Key = Empty<byte>.Array,
            });
            return true;
        }
    }
    internal class TradeCommand : Command
    {
        public TradeCommand()
            : base("trade")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (String.IsNullOrWhiteSpace(args[0]))
            {
                player.SendInfo("Usage: /trade <player name>");
                return false;
            }
            player.RequestTrade(time, new RequestTradePacket
            {
                Name = args[0]
            });
            return true;
        }
    }


    internal class WhoCommand : Command
    {
        public WhoCommand()
            : base("who")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            StringBuilder sb = new StringBuilder("Players online: ");
            Player[] copy = player.Owner.Players.Values.ToArray();
            for (int i = 0; i < copy.Length; i++)
            {
                if (i != 0) sb.Append(", ");
                sb.Append(copy[i].Name);
            }

            player.SendInfo(sb.ToString());
            return true;
        }
    }

    internal class ServerCommand : Command
    {
        public ServerCommand()
            : base("server")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            player.SendInfo(player.Owner.Name);
            return true;
        }
    }

    internal class PauseCommand : Command
    {
        public PauseCommand()
            : base("pause")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.HasConditionEffect(ConditionEffectIndex.Paused))
            {
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Paused,
                    DurationMS = 0
                });
                player.SendInfo("Game resumed.");
            }
            else
            {
                foreach (Enemy i in player.Owner.EnemiesCollision.HitTest(player.X, player.Y, 8).OfType<Enemy>())
                {
                    if (i.ObjectDesc.Enemy)
                    {
                        player.SendInfo("Not safe to pause.");
                        return false;
                    }
                }
                player.ApplyConditionEffect(new ConditionEffect
                {
                    Effect = ConditionEffectIndex.Paused,
                    DurationMS = -1
                });
                player.SendInfo("Game paused.");
            }
            return true;
        }
    }



    internal class teleportCommand : Command
    {
        public teleportCommand()
            : base("teleport")
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            try
            {
                if (String.Equals(player.Name.ToLower(), args[0].ToLower()))
                {
                    player.SendInfo("You are already at yourself, and always will be!");
                    return false;
                }

                foreach (KeyValuePair<int, Player> i in player.Owner.Players)
                {
                    if (i.Value.Name.ToLower() == args[0].ToLower().Trim())
                    {
                        player.Teleport(time, new TeleportPacket
                        {
                            ObjectId = i.Value.Id
                        });
                        player.ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Invincible,
                            DurationMS = 1500
                        });
                        player.ApplyConditionEffect(new ConditionEffect
                        {
                            Effect = ConditionEffectIndex.Stunned,

                            DurationMS = 1500
                        });
                        return true;
                    }
                }
                player.SendInfo(string.Format("Cannot teleport, {0} not found!", args[0].Trim()));
            }
            catch
            {
                player.SendHelp("Usage: /teleport <player name>");
            }
            return false;
        }
    }

    class TellCommand : Command
    {
        public TellCommand() : base("tell") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (!player.NameChosen)
            {
                player.SendError("Choose a name!");
                return false;
            }
            if (args.Length < 2)
            {
                player.SendError("Usage: /tell <player name> <text>");
                return false;
            }

            string playername = args[0].Trim();
            string msg = string.Join(" ", args, 1, args.Length - 1);

            if (String.Equals(player.Name.ToLower(), playername.ToLower()))
            {
                player.SendInfo("Quit telling yourself!");
                return false;
            }

            foreach (var i in player.Manager.Clients.Values)
            {
                if (i.Account.NameChosen && i.Account.Name.EqualsIgnoreCase(playername))
                {
                    player.Client.SendPacket(new TextPacket() //echo to self
                    {
                        ObjectId = player.Id,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = ""
                    });

                    i.SendPacket(new TextPacket() //echo to /tell player
                    {
                        ObjectId = i.Player.Owner.Id == player.Owner.Id ? player.Id : -1,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = ""
                    });
                    return true;
                }
            }
            player.SendError(string.Format("{0} not found.", playername));
            return false;
        }
    }

    class MsgCommand : Command
    {
        public MsgCommand() : base("Msg") { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (!player.NameChosen)
            {
                player.SendError("Choose a name!");
                return false;
            }
            if (args.Length < 2)
            {
                player.SendError("Usage: /Msg <player name> <text>");
                return false;
            }

            string playername = args[0].Trim();
            string msg = string.Join(" ", args, 1, args.Length - 1);

            if (String.Equals(player.Name.ToLower(), playername.ToLower()))
            {
                player.SendInfo("Quit telling yourself!");
                return false;
            }

            foreach (var i in player.Manager.Clients.Values)
            {
                if (i.Account.NameChosen && i.Account.Name.EqualsIgnoreCase(playername))
                {
                    player.Client.SendPacket(new TextPacket() //echo to self
                    {
                        ObjectId = player.Id,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = ""
                    });

                    i.SendPacket(new TextPacket() //echo to /tell player
                    {
                        ObjectId = i.Player.Owner.Id == player.Owner.Id ? player.Id : -1,
                        BubbleTime = 10,
                        Stars = player.Stars,
                        Name = player.Name,
                        Recipient = i.Account.Name,
                        Text = msg.ToSafeText(),
                        CleanText = ""
                    });
                    return true;
                }
            }
            player.SendError(string.Format("{0} not found.", playername));
            return false;
        }
    }

    internal class TqCommand : Command
    {
        public TqCommand()
            : base("tq", 1)
        {
        }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (player.Quest == null)
            {
                player.SendError("Player does not have a quest!");
                return false;
            }

            if (player.Owner.Dungeon == true)
            {
                player.SendError("Only usable in realm.");
                return false;
            }

            player.Move(player.Quest.X + 0.5f, player.Quest.Y + 0.5f);
            if (player.Pet != null)
                player.Pet.Move(player.Quest.X + 0.5f, player.Quest.Y + 0.5f);
            player.UpdateCount++;

            player.ApplyConditionEffect(new ConditionEffect
            {
                DurationMS = 2000,
                Effect = ConditionEffectIndex.Invulnerable
            });

            player.ApplyConditionEffect(new ConditionEffect
            {
                DurationMS = 2000,
                Effect = ConditionEffectIndex.Stunned
            });

            player.Owner.BroadcastPacket(new GotoPacket
            {
                ObjectId = player.Id,
                Position = new Position
                {
                    X = player.Quest.X,
                    Y = player.Quest.Y
                }
            }, null);
            player.SendInfo("Success!");
            return true;
        }
    }
}