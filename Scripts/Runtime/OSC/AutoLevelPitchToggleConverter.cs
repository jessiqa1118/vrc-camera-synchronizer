using OSC;
using Parameters;

namespace VRCCamera
{
    public class AutoLevelPitchToggleConverter : IOSCMessageConverter<AutoLevelPitchToggle>
    {
        public AutoLevelPitchToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.AutoLevelPitch)
            {
                return new AutoLevelPitchToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new AutoLevelPitchToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new AutoLevelPitchToggle(value);
        }

        public Message ToOSCMessage(AutoLevelPitchToggle toggle)
        {
            return new Message(OSCCameraEndpoints.AutoLevelPitch, new[] { new Argument(toggle.Value) });
        }
    }
}

