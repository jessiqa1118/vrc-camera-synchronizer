using OSC;
using Parameters;

namespace VRCCamera
{
    public class LookAtMeXOffsetConverter : IOSCMessageConverter<LookAtMeXOffset>
    {
        public LookAtMeXOffset FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.LookAtMeXOffset)
            {
                return new LookAtMeXOffset(LookAtMeXOffset.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new LookAtMeXOffset(LookAtMeXOffset.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new LookAtMeXOffset(LookAtMeXOffset.DefaultValue);
            }
            
            // Extract value with automatic clamping in LookAtMeXOffset constructor
            var value = arg.AsFloat32();
            return new LookAtMeXOffset(value);
        }

        public Message ToOSCMessage(LookAtMeXOffset offset)
        {
            return new Message(OSCCameraEndpoints.LookAtMeXOffset, new[] { new Argument(offset.Value) });
        }
    }
}