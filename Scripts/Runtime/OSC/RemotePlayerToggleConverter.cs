using OSC;
using Parameters;

namespace VRCCamera
{
    public class RemotePlayerToggleConverter : IOSCMessageConverter<RemotePlayerToggle>
    {
        public RemotePlayerToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.RemotePlayer)
            {
                return new RemotePlayerToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new RemotePlayerToggle(false);
            }

            var arg = message.Arguments[0];

            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new RemotePlayerToggle(value);
        }

        public Message ToOSCMessage(RemotePlayerToggle toggle)
        {
            return new Message(OSCCameraEndpoints.RemotePlayer, new[] { new Argument(toggle.Value) });
        }
    }
}

