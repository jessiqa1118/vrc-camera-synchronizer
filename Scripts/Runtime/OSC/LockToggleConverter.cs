using OSC;
using Parameters;

namespace VRCCamera
{
    public class LockToggleConverter : IOSCMessageConverter<LockToggle>
    {
        public LockToggle FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Lock)
            {
                return new LockToggle(false);
            }

            // Validate arguments exist and count
            if (message.Arguments is not { Length: > 0 })
            {
                return new LockToggle(false);
            }

            var arg = message.Arguments[0];

            // Handle Bool, Int32, and Float32
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new LockToggle(value);
        }

        public Message ToOSCMessage(LockToggle toggle)
        {
            return new Message(OSCCameraEndpoints.Lock, new[] { new Argument(toggle.Value) });
        }
    }
}

