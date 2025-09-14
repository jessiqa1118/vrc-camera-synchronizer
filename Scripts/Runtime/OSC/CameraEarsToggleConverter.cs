using OSC;
using Parameters;

namespace VRCCamera
{
    public class CameraEarsToggleConverter : IOSCMessageConverter<CameraEarsToggle>
    {
        public CameraEarsToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.CameraEars)
            {
                return new CameraEarsToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new CameraEarsToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new CameraEarsToggle(value);
        }

        public Message ToOSCMessage(CameraEarsToggle toggle)
        {
            return new Message(OSCCameraEndpoints.CameraEars, new[] { new Argument(toggle.Value) });
        }
    }
}

