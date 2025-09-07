using OSC;
using Parameters;

namespace VRCCamera
{
    public class HueConverter : IOSCMessageConverter<Hue>
    {
        public Hue FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Hue)
            {
                return new Hue(Hue.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Hue(Hue.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Hue(Hue.DefaultValue);
            }
            
            // Extract value with automatic clamping in Hue constructor
            var value = arg.AsFloat32();
            return new Hue(value);
        }

        public Message ToOSCMessage(Hue hue)
        {
            return new Message(OSCCameraEndpoints.Hue, new[] { new Argument(hue.Value) });
        }
    }
}