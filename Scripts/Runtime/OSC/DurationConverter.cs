using OSC;
using Parameters;

namespace VRCCamera
{
    public class DurationConverter : IOSCMessageConverter<Duration>
    {
        public Duration FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Duration)
            {
                return new Duration(Duration.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Duration(Duration.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Duration(Duration.DefaultValue);
            }
            
            // Extract value with automatic clamping in Duration constructor
            var value = arg.AsFloat32();
            return new Duration(value);
        }

        public Message ToOSCMessage(Duration duration)
        {
            return new Message(OSCCameraEndpoints.Duration, new[] { new Argument(duration.Value) });
        }
    }
}