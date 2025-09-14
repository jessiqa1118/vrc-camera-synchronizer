using OSC;
using Parameters;

namespace VRCCamera
{
    public class ShowUIInCameraToggleConverter : IOSCMessageConverter<ShowUIInCameraToggle>
    {
        public ShowUIInCameraToggle FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.ShowUIInCamera)
            {
                return new ShowUIInCameraToggle(false);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: > 0 })
            {
                return new ShowUIInCameraToggle(false);
            }

            var arg = message.Arguments[0];
            
            // Handle both Bool and Int32 types (VRChat may send either)
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };
            
            return new ShowUIInCameraToggle(value);
        }

        public Message ToOSCMessage(ShowUIInCameraToggle toggle)
        {
            return new Message(OSCCameraEndpoints.ShowUIInCamera, new[] { new Argument(toggle.Value) });
        }
    }
}