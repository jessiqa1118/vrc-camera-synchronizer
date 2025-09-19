using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public class PhotoRateConverter : IOSCMessageConverter<PhotoRate>
    {
        public PhotoRate FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.PhotoRate)
            {
                return new PhotoRate(PhotoRate.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new PhotoRate(PhotoRate.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new PhotoRate(PhotoRate.DefaultValue);
            }
            
            // Extract value with automatic clamping in PhotoRate constructor
            var value = arg.AsFloat32();
            return new PhotoRate(value);
        }

        public Message ToOSCMessage(PhotoRate photoRate)
        {
            return new Message(OSCCameraEndpoints.PhotoRate, new[] { new Argument(photoRate.Value) });
        }
    }
}