using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class FlySpeedConverter : IOSCMessageConverter<FlySpeed>
    {
        public FlySpeed FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.FlySpeed)
            {
                return new FlySpeed(FlySpeed.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new FlySpeed(FlySpeed.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new FlySpeed(FlySpeed.DefaultValue);
            }
            
            // Extract value with automatic clamping in FlySpeed constructor
            var value = arg.AsFloat32();
            return new FlySpeed(value);
        }

        public Message ToOSCMessage(FlySpeed flySpeed)
        {
            return new Message(OSCCameraEndpoints.FlySpeed, new[] { new Argument(flySpeed.Value) });
        }
    }
}