using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class SaturationConverter : IOSCMessageConverter<Saturation>
    {
        public Saturation FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Saturation)
            {
                return new Saturation(Saturation.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Saturation(Saturation.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Saturation(Saturation.DefaultValue);
            }
            
            // Extract value with automatic clamping in Saturation constructor
            var value = arg.AsFloat32();
            return new Saturation(value);
        }

        public Message ToOSCMessage(Saturation saturation)
        {
            return new Message(OSCCameraEndpoints.Saturation, new[] { new Argument(saturation.Value) });
        }
    }
}