using OSC;
using Parameters;

namespace VRCCamera
{
    public class ShowFocusToggleConverter : IOSCMessageConverter<ShowFocusToggle>
    {
        public ShowFocusToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.ShowFocus)
            {
                return new ShowFocusToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new ShowFocusToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new ShowFocusToggle(value);
        }

        public Message ToOSCMessage(ShowFocusToggle toggle)
        {
            return new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(toggle.Value) });
        }
    }
}

