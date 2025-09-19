using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class TurnSpeedConverter : IOSCMessageConverter<TurnSpeed>
    {
        public TurnSpeed FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.TurnSpeed)
            {
                return new TurnSpeed(TurnSpeed.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new TurnSpeed(TurnSpeed.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new TurnSpeed(TurnSpeed.DefaultValue);
            }
            
            // Extract value with automatic clamping in TurnSpeed constructor
            var value = arg.AsFloat32();
            return new TurnSpeed(value);
        }

        public Message ToOSCMessage(TurnSpeed turnSpeed)
        {
            return new Message(OSCCameraEndpoints.TurnSpeed, new[] { new Argument(turnSpeed.Value) });
        }
    }
}