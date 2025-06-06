﻿using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Movement
{
    public class Teleport : Command
    {
        public override string Name => "teleport";
        public override int RankRequired => 50;

        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length == 0)
            {
                client.LocalPlayer.SetPosition(executor.X, executor.Y, executor.Z);
                client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
                // client.SendMessage($"&eTeleported to &a{executor.Name}&e!");
                return true;
            }
            if (arguments.Length < 3)
            {
                ClassicPlayer target = client.PlayerList.SearchPlayer(arguments[0]);
                if (target == null) return false;

                client.LocalPlayer.SetPositionRotation(target.X, target.Y, target.Z, target.Yaw, target.Pitch);
                client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
                // client.SendMessage($"&eTeleported to &a{target.Name}&e!");
                return true;
            }
            short[] position = new short[3];

            for (int i = 0; i < 3; i++)
                if (!short.TryParse(arguments[i], out position[i]))
                    return false;

            client.LocalPlayer.SetBlockPosition(position[0], position[1], position[2]);
            client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
            //  client.SendMessage($"&eTeleported to &a{position[0]} {position[1]} {position[2]}&e!");
            return true;
        }
    }
}
