using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace BetterFishing.Util
{
    public partial class PacketID
    {
        public const byte ANGLER_QUEST = 1;
        public const byte ANGLER_TIME_REQUEST = 2;
        public const byte ANGLER_TIME_ANSWER = 3;
    }

    public abstract class PacketHandler
    {
        private const int BYTE_SIZE = sizeof(byte);

        public static ModPacket Create(byte PacketID, int capacity)
        {
            ModPacket packet = BetterFishing.Instance.GetPacket(capacity + BYTE_SIZE);
            packet.Write(PacketID);
            return packet;
        }

        private static readonly Dictionary<byte, RecievePacket> _listners = new();

        public static void SetListener(byte PacketID, RecievePacket receiver)
        {
            _listners.Add(PacketID, receiver);
        }

        public static void RemoveListener(byte PacketID)
        {
            _listners.Remove(PacketID);
        }

        public static void Invoke(BinaryReader reader, int whoAmI)
        {
            byte PacketID = reader.ReadByte();

            RecievePacket receiver = _listners.GetValueOrDefault(PacketID, null);

            if (receiver == null)
                return;

            receiver.Invoke(reader, whoAmI == 255 ? PacketType.SERVER_TO_CLIENT : PacketType.CLIENT_TO_SERVER, whoAmI);
        }

        public delegate void RecievePacket(BinaryReader reader, PacketType type, int senderIndex);

        private readonly byte id;

        public PacketHandler(byte id)
        {
            this.id = id;
            SetListener(id, HandlePacket);
        }

        public virtual ModPacket CreatePacket(int capacity)
        {
            return Create(id, capacity);
        }

        public abstract void HandlePacket(BinaryReader reader, PacketType type, int senderIndex);
    }

    public enum PacketType
    {
        CLIENT_TO_SERVER,
        SERVER_TO_CLIENT
    }
}
