namespace JessiQa
{
    public class ExposureConverter : IOSCMessageConverter<Exposure>
    {
        public Exposure FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Exposure)
            {
                return new Exposure(Exposure.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Exposure(Exposure.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Exposure(Exposure.DefaultValue);
            }
            
            // Extract value with automatic clamping in Exposure constructor
            var value = arg.AsFloat32();
            return new Exposure(value);
        }

        public Message ToOSCMessage(Exposure exposure)
        {
            return new Message(OSCCameraEndpoints.Exposure, new[] { new Argument(exposure.Value) });
        }
    }
}