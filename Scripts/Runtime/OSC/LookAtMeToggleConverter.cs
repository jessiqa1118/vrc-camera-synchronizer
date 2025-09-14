using OSC;
using Parameters;

namespace VRCCamera
{
    public class LookAtMeToggleConverter : IOSCMessageConverter<LookAtMeToggle>
    {
        public LookAtMeToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.LookAtMe)
            {
                return new LookAtMeToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new LookAtMeToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new LookAtMeToggle(value);
        }

        public Message ToOSCMessage(LookAtMeToggle toggle)
        {
            return new Message(OSCCameraEndpoints.LookAtMe, new[] { new Argument(toggle.Value) });
        }
    }
}

