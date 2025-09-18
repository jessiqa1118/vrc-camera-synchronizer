using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OSC;

namespace VRCCamera
{
    // Transmitter that encodes OSC packets generically, allowing arbitrary argument lists
    public sealed class OSCTransmitter : IOSCTransmitter
    {
        private Socket _socket;
        private bool _disposed;

        public OSCTransmitter(string destination, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            if (destination == "255.255.255.255")
            {
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            }

            var endpoint = new IPEndPoint(IPAddress.Parse(destination), port);
            _socket.Connect(endpoint);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try { _socket?.Close(); } catch { }
            _socket = null;
        }

        public void Send(Message message)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(OSCTransmitter));

            using var ms = new MemoryStream(256);

            // Address pattern
            WriteOscString(ms, message.Address.Value);

            // Type tag (starts with ',')
            WriteOscString(ms, "," + message.TypeTag.Value);

            // Arguments payloads
            foreach (var arg in message.Arguments)
            {
                switch (arg.Type)
                {
                    case Argument.ValueType.Int32:
                        WriteInt32BE(ms, arg.AsInt32());
                        break;

                    case Argument.ValueType.Float32:
                        WriteFloat32BE(ms, arg.AsFloat32());
                        break;

                    case Argument.ValueType.String:
                        WriteOscString(ms, arg.AsString());
                        break;

                    case Argument.ValueType.Blob:
                    {
                        var blob = arg.AsBlob();
                        WriteBlob(ms, blob);
                        break;
                    }

                    case Argument.ValueType.Bool:
                        // OSC bool uses only type tag 'T'/'F' with no payload
                        break;

                    default:
                        throw new NotSupportedException($"Unsupported OSC argument type: {arg.Type}");
                }
            }

            // Send buffer (use ToArray to avoid exposing unused capacity)
            var buffer = ms.ToArray();
            _socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        // Convenience overload: build Message from raw args
        public void Send(string address, params Argument[] args)
        {
            Send(new Message(new Address(address), args ?? Array.Empty<Argument>()));
        }

        private static void WriteOscString(Stream s, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
            s.Write(bytes, 0, bytes.Length);
            s.WriteByte(0);
            PadTo4(s);
        }

        private static void WriteBlob(Stream s, byte[] blob)
        {
            var data = blob ?? Array.Empty<byte>();
            WriteInt32BE(s, data.Length);
            s.Write(data, 0, data.Length);
            PadTo4(s);
        }

        private static void WriteInt32BE(Stream s, int value)
        {
            var be = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
            s.Write(be, 0, 4);
        }

        private static void WriteFloat32BE(Stream s, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            s.Write(bytes, 0, 4);
        }

        private static void PadTo4(Stream s)
        {
            var pad = (int)(4 - (s.Position % 4)) & 3;
            for (int i = 0; i < pad; i++) s.WriteByte(0);
        }
    }
}
