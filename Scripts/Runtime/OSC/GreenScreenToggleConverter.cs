using OSC;
using Parameters;

namespace VRCCamera
{
    public class GreenScreenToggleConverter : IOSCMessageConverter<GreenScreenToggle>
    {
        public GreenScreenToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.GreenScreen)
            {
                return new GreenScreenToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new GreenScreenToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new GreenScreenToggle(value);
        }

        public Message ToOSCMessage(GreenScreenToggle toggle)
        {
            return new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(toggle.Value) });
        }
    }
}

