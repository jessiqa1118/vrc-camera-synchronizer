using OSC;
using Parameters;

namespace VRCCamera
{
    public class RollWhileFlyingToggleConverter : IOSCMessageConverter<RollWhileFlyingToggle>
    {
        public RollWhileFlyingToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.RollWhileFlying)
            {
                return new RollWhileFlyingToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new RollWhileFlyingToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new RollWhileFlyingToggle(value);
        }

        public Message ToOSCMessage(RollWhileFlyingToggle toggle)
        {
            return new Message(OSCCameraEndpoints.RollWhileFlying, new[] { new Argument(toggle.Value) });
        }
    }
}

