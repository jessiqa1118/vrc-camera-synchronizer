using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class FlyingToggleConverter : IOSCMessageConverter<bool>
    {
        public bool FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Flying)
            {
                return false;
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return false;
            }

            var arg = message.Arguments[0];

            return arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false,
            };
        }

        public Message ToOSCMessage(bool value)
        {
            return new Message(OSCCameraEndpoints.Flying, new[] { new Argument(value) });
        }
    }
}
