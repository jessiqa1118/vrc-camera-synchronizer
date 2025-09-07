using OSC;
using Parameters;

namespace VRCCamera
{
    public class LookAtMeYOffsetConverter : IOSCMessageConverter<LookAtMeYOffset>
    {
        public LookAtMeYOffset FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.LookAtMeYOffset)
            {
                return new LookAtMeYOffset(LookAtMeYOffset.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new LookAtMeYOffset(LookAtMeYOffset.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new LookAtMeYOffset(LookAtMeYOffset.DefaultValue);
            }
            
            // Extract value with automatic clamping in LookAtMeYOffset constructor
            var value = arg.AsFloat32();
            return new LookAtMeYOffset(value);
        }

        public Message ToOSCMessage(LookAtMeYOffset offset)
        {
            return new Message(OSCCameraEndpoints.LookAtMeYOffset, new[] { new Argument(offset.Value) });
        }
    }
}