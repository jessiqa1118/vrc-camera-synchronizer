using OSC;
using Parameters;

namespace VRCCamera
{
    public class LightnessConverter : IOSCMessageConverter<Lightness>
    {
        public Lightness FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Lightness)
            {
                return new Lightness(Lightness.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Lightness(Lightness.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Lightness(Lightness.DefaultValue);
            }
            
            // Extract value with automatic clamping in Lightness constructor
            var value = arg.AsFloat32();
            return new Lightness(value);
        }

        public Message ToOSCMessage(Lightness lightness)
        {
            return new Message(OSCCameraEndpoints.Lightness, new[] { new Argument(lightness.Value) });
        }
    }
}