using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Net;

namespace CSConnection
{
    public static class CSApi
    {
        public static List<ReaderModule> Readers = new List<ReaderModule>();

        /// <summary>
        /// Sends bytes to players
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="args">An array with objects to be written in bytes</param>
        public static void Send(string key, params object[] args) =>
            Send(key, GetBytes(args));

        /// <summary>
        /// Sends bytes to players
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="stream">Stream with already written bytes</param>
        public static void Send(string key, MemoryStream stream) =>
            Send(key, stream.ToArray());

        /// <summary>
        /// Sends bytes to players
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="data">Bytes</param>
        /// <param name="ignoreClient"></param>
        public static void Send(string key, byte[] data, int ignoreClient = -1)
        {
            NetPacket packet = new NetPacket(SCModule.Id, ASCIIEncoding.Unicode.GetByteCount(key) + data.Length);

            packet.Writer.Write(key);
            packet.Writer.Write(data);

            NetManager.Instance.Broadcast(packet, ignoreClient);
        }

        /// <summary>
        /// Sends bytes to player
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="args">An array with objects to be written in bytes</param>
        public static void SendToClient(string key, int player, params object[] args) =>
            SendToClient(key, player, GetBytes(args));

        /// <summary>
        /// Sends bytes to player
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="stream">Stream with already written bytes</param>
        public static void SendToClient(string key, int player, MemoryStream stream) =>
            SendToClient(key, player, stream.ToArray());

        /// <summary>
        /// Sends bytes to player
        /// </summary>
        /// <param name="key">ReaderModule key</param>
        /// <param name="data">Bytes</param>
        public static void SendToClient(string key, int player, byte[] data)
        {
            NetPacket packet = new NetPacket(SCModule.Id, ASCIIEncoding.Unicode.GetByteCount(key)+data.Length);

            packet.Writer.Write(key);
            packet.Writer.Write(data);

            NetManager.Instance.SendToClient(packet, player);
        }

        public static byte[] GetBytes(params object[] args)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] == null)
                            continue;

                        if (args[i] is bool)
                            writer.Write((bool)args[i]);

                        if (args[i] is byte)
                            writer.Write((byte)args[i]);
                        if (args[i] is sbyte)
                            writer.Write((sbyte)args[i]);
                        if (args[i] is byte[])
                            writer.Write((byte[])args[i]);

                        if (args[i] is char)
                            writer.Write((char)args[i]);
                        if (args[i] is char[])
                            writer.Write((char[])args[i]);

                        if (args[i] is double)
                            writer.Write((double)args[i]);
                        if (args[i] is decimal)
                            writer.Write((decimal)args[i]);

                        if (args[i] is short)
                            writer.Write((short)args[i]);
                        if (args[i] is ushort)
                            writer.Write((ushort)args[i]);

                        if (args[i] is int)
                            writer.Write((int)args[i]);
                        if (args[i] is uint)
                            writer.Write((uint)args[i]);

                        if (args[i] is long)
                            writer.Write((long)args[i]);
                        if (args[i] is ulong)
                            writer.Write((ulong)args[i]);

                        if (args[i] is float)
                            writer.Write((float)args[i]);

                        if (args[i] is string)
                            writer.Write((string)args[i]);
                    }
                }
                return stream.ToArray();
            }
        }
        internal static void Deserialize(BinaryReader reader, int userId)
        {
            string key = reader.ReadString();
            var modules = Readers.FindAll(p => p.Name.ToLower() == key.ToLower());
            
            if (modules.Count != 0)
            {
                modules.ForEach(m => m.Run(new ReaderArgs(key, reader, userId)));
            }
        }
    }
}
