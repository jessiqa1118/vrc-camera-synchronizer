using OSC;
using Parameters;

namespace VRCCamera
{
    public class FocalDistanceConverter : IOSCMessageConverter<FocalDistance>
    {
        public FocalDistance FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.FocalDistance)
            {
                return new FocalDistance(FocalDistance.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new FocalDistance(FocalDistance.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new FocalDistance(FocalDistance.DefaultValue);
            }
            
            // Extract value with automatic clamping in FocalDistance constructor
            var value = arg.AsFloat32();
            return new FocalDistance(value);
        }

        public Message ToOSCMessage(FocalDistance focalDistance)
        {
            return new Message(OSCCameraEndpoints.FocalDistance, new[] { new Argument(focalDistance.Value) });
        }
    }
}