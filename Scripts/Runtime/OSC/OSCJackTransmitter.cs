using System;
using OscJack;

namespace JessiQa
{
    public class OSCJackTransmitter : IOSCTransmitter
    {
        private readonly OscClient _client;
        private bool _disposed = false;

        public OSCJackTransmitter(string destination, int port)
        {
            _client = new OscClient(destination, port);
        }

        public void Dispose()
        {
            if (_disposed) return;

            _client.Dispose();
            _disposed = true;
        }

        public void Send(Message message)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(OSCJackTransmitter));

            if (message.Arguments == null || message.Arguments.Length == 0)
            {
                _client.Send(message.Address.Value);
                return;
            }

            // Check type pattern using TypeTag and send with appropriate OSCJack method
            switch (message.TypeTag.Value)
            {
                // Single values
                case "i":
                    _client.Send(message.Address.Value, message.Arguments[0].AsInt32());
                    break;
                case "f":
                    _client.Send(message.Address.Value, message.Arguments[0].AsFloat32());
                    break;
                case "s":
                    _client.Send(message.Address.Value, message.Arguments[0].AsString());
                    break;

                // Two integers
                case "ii":
                    _client.Send(message.Address.Value,
                        message.Arguments[0].AsInt32(),
                        message.Arguments[1].AsInt32());
                    break;

                // Three integers
                case "iii":
                    _client.Send(message.Address.Value,
                        message.Arguments[0].AsInt32(),
                        message.Arguments[1].AsInt32(),
                        message.Arguments[2].AsInt32());
                    break;

                // Two floats
                case "ff":
                    _client.Send(message.Address.Value,
                        message.Arguments[0].AsFloat32(),
                        message.Arguments[1].AsFloat32());
                    break;

                // Three floats
                case "fff":
                    _client.Send(message.Address.Value,
                        message.Arguments[0].AsFloat32(),
                        message.Arguments[1].AsFloat32(),
                        message.Arguments[2].AsFloat32());
                    break;

                // Four floats
                case "ffff":
                    _client.Send(message.Address.Value,
                        message.Arguments[0].AsFloat32(),
                        message.Arguments[1].AsFloat32(),
                        message.Arguments[2].AsFloat32(),
                        message.Arguments[3].AsFloat32());
                    break;

                // Bool (send as int)
                case "T":
                case "F":
                    _client.Send(message.Address.Value, message.Arguments[0].AsBool() ? 1 : 0);
                    break;

                default:
                    throw new NotSupportedException($"OSCJack does not support type pattern: {message.TypeTag.Value}");
            }
        }
    }
}