﻿namespace ClassicConnect.Network.Player
{
    public class Despawn : ClassicPacket
    {
        public override byte PacketID => 0x0c;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerId = (sbyte)stream.ReadByte();
            connection.PlayerList.PlayerDespawn(playerId);
        }
    }
}
