using OSC;
using Parameters;

namespace VRCCamera
{
    public class LocalPlayerToggleConverter : IOSCMessageConverter<LocalPlayerToggle>
    {
        public LocalPlayerToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.LocalPlayer)
            {
                return new LocalPlayerToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new LocalPlayerToggle(false);
            }

            var arg = message.Arguments[0];

            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new LocalPlayerToggle(value);
        }

        public Message ToOSCMessage(LocalPlayerToggle toggle)
        {
            return new Message(OSCCameraEndpoints.LocalPlayer, new[] { new Argument(toggle.Value) });
        }
    }
}

