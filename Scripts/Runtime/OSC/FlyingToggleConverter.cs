using OSC;
using Parameters;

namespace VRCCamera
{
    public class FlyingToggleConverter : IOSCMessageConverter<FlyingToggle>
    {
        public FlyingToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Flying)
            {
                return new FlyingToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new FlyingToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new FlyingToggle(value);
        }

        public Message ToOSCMessage(FlyingToggle toggle)
        {
            return new Message(OSCCameraEndpoints.Flying, new[] { new Argument(toggle.Value) });
        }
    }
}

