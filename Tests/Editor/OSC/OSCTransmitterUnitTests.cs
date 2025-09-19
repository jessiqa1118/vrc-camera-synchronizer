using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NUnit.Framework;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class OSCTransmitterUnitTests
    {
        private static string ReadOscString(byte[] data, ref int offset)
        {
            int start = offset;
            while (offset < data.Length && data[offset] != 0) offset++;
            var s = Encoding.UTF8.GetString(data, start, offset - start);
            // Skip null
            offset++;
            // Pad to 4-byte boundary
            while ((offset % 4) != 0) offset++;
            return s;
        }

        private static int ReadInt32BE(byte[] data, ref int offset)
        {
            var v = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(new[] { data[offset + 3], data[offset + 2], data[offset + 1], data[offset + 0] }, 0));
            offset += 4;
            return v;
        }

        private static float ReadFloat32BE(byte[] data, ref int offset)
        {
            var bytes = new byte[] { data[offset + 3], data[offset + 2], data[offset + 1], data[offset + 0] };
            offset += 4;
            return BitConverter.ToSingle(bytes, 0);
        }

        private static byte[] ReadBlob(byte[] data, ref int offset)
        {
            int length = ReadInt32BE(data, ref offset);
            var buf = new byte[length];
            Buffer.BlockCopy(data, offset, buf, 0, length);
            offset += length;
            while ((offset % 4) != 0) offset++;
            return buf;
        }

        private static byte[] SendAndReceivePacket(Message message)
        {
            using var udp = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
            udp.Client.ReceiveTimeout = 2000;
            int port = ((IPEndPoint)udp.Client.LocalEndPoint).Port;

            using var tx = new OSCTransmitter("127.0.0.1", port);
            tx.Send(message);

            var remote = new IPEndPoint(IPAddress.Any, 0);
            var bytes = udp.Receive(ref remote);
            return bytes;
        }

        [Test]
        public void Send_NoArgs_EmitsAddressAndEmptyTypeTag()
        {
            var packet = SendAndReceivePacket(new Message(new Address("/ping"), Array.Empty<Argument>()));
            int off = 0;
            var address = ReadOscString(packet, ref off);
            var tags = ReadOscString(packet, ref off);

            Assert.AreEqual("/ping", address);
            Assert.AreEqual(",", tags);
            Assert.AreEqual(off, packet.Length); // no payload
        }

        [Test]
        public void Send_IntFloatString_EmitsProperTypeTagsAndPayloads()
        {
            var msg = new Message(new Address("/test"), new[]
            {
                new Argument(42),
                new Argument(0.5f),
                new Argument("ok")
            });
            var packet = SendAndReceivePacket(msg);

            int off = 0;
            Assert.AreEqual("/test", ReadOscString(packet, ref off));
            Assert.AreEqual(",ifs", ReadOscString(packet, ref off));
            Assert.AreEqual(42, ReadInt32BE(packet, ref off));
            Assert.AreEqual(0.5f, ReadFloat32BE(packet, ref off), 1e-6f);
            Assert.AreEqual("ok", ReadOscString(packet, ref off));
            Assert.AreEqual(off, packet.Length);
        }

        [Test]
        public void Send_Bool_UsesTypeTagOnly_NoPayload()
        {
            var packetT = SendAndReceivePacket(new Message(new Address("/flag"), new[] { new Argument(true) }));
            int offT = 0;
            Assert.AreEqual("/flag", ReadOscString(packetT, ref offT));
            Assert.AreEqual(",T", ReadOscString(packetT, ref offT));
            Assert.AreEqual(offT, packetT.Length);

            var packetF = SendAndReceivePacket(new Message(new Address("/flag"), new[] { new Argument(false) }));
            int offF = 0;
            Assert.AreEqual("/flag", ReadOscString(packetF, ref offF));
            Assert.AreEqual(",F", ReadOscString(packetF, ref offF));
            Assert.AreEqual(offF, packetF.Length);
        }

        [Test]
        public void Send_Blob_WritesLengthAndData()
        {
            var blob = new byte[] { 1, 2, 3, 4 };
            var packet = SendAndReceivePacket(new Message(new Address("/blob"), new[] { new Argument(blob) }));
            int off = 0;
            Assert.AreEqual("/blob", ReadOscString(packet, ref off));
            Assert.AreEqual(",b", ReadOscString(packet, ref off));
            var read = ReadBlob(packet, ref off);
            CollectionAssert.AreEqual(blob, read);
            Assert.AreEqual(off, packet.Length);
        }
    }
}

