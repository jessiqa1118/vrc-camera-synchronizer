using OSC;
using Parameters;

namespace VRCCamera
{
    public class AutoLevelRollToggleConverter : IOSCMessageConverter<AutoLevelRollToggle>
    {
        public AutoLevelRollToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.AutoLevelRoll)
            {
                return new AutoLevelRollToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new AutoLevelRollToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new AutoLevelRollToggle(value);
        }

        public Message ToOSCMessage(AutoLevelRollToggle toggle)
        {
            return new Message(OSCCameraEndpoints.AutoLevelRoll, new[] { new Argument(toggle.Value) });
        }
    }
}

