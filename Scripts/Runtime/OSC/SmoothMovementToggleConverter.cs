using OSC;
using Parameters;

namespace VRCCamera
{
    public class SmoothMovementToggleConverter : IOSCMessageConverter<SmoothMovementToggle>
    {
        public SmoothMovementToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.SmoothMovement)
            {
                return new SmoothMovementToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new SmoothMovementToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new SmoothMovementToggle(value);
        }

        public Message ToOSCMessage(SmoothMovementToggle toggle)
        {
            return new Message(OSCCameraEndpoints.SmoothMovement, new[] { new Argument(toggle.Value) });
        }
    }
}

