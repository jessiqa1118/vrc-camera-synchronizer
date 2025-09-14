using OSC;
using Parameters;

namespace VRCCamera
{
    public class EnvironmentToggleConverter : IOSCMessageConverter<EnvironmentToggle>
    {
        public EnvironmentToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Environment)
            {
                return new EnvironmentToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new EnvironmentToggle(false);
            }

            var arg = message.Arguments[0];

            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new EnvironmentToggle(value);
        }

        public Message ToOSCMessage(EnvironmentToggle toggle)
        {
            return new Message(OSCCameraEndpoints.Environment, new[] { new Argument(toggle.Value) });
        }
    }
}

