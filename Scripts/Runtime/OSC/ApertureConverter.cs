using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class ApertureConverter : IOSCMessageConverter<Aperture>
    {
        public Aperture FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Aperture)
            {
                return new Aperture(Aperture.DefaultValue);
            }

            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Aperture(Aperture.DefaultValue);
            }

            var arg = message.Arguments[0];

            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Aperture(Aperture.DefaultValue);
            }

            // Extract value with automatic clamping in Aperture constructor
            var value = arg.AsFloat32();
            return new Aperture(value);
        }

        public Message ToOSCMessage(Aperture aperture)
        {
            return new Message(OSCCameraEndpoints.Aperture, new[] { new Argument(aperture.Value) });
        }
    }
}