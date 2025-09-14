using OSC;
using Parameters;

namespace VRCCamera
{
    public class DollyPathsStayVisibleToggleConverter : IOSCMessageConverter<DollyPathsStayVisibleToggle>
    {
        public DollyPathsStayVisibleToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.DollyPathsStayVisible)
            {
                return new DollyPathsStayVisibleToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new DollyPathsStayVisibleToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new DollyPathsStayVisibleToggle(value);
        }

        public Message ToOSCMessage(DollyPathsStayVisibleToggle toggle)
        {
            return new Message(OSCCameraEndpoints.DollyPathsStayVisible, new[] { new Argument(toggle.Value) });
        }
    }
}

