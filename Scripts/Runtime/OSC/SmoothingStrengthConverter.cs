namespace JessiQa
{
    public class SmoothingStrengthConverter : IOSCMessageConverter<SmoothingStrength>
    {
        public SmoothingStrength FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.SmoothingStrength)
            {
                return new SmoothingStrength(SmoothingStrength.DefaultValue);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new SmoothingStrength(SmoothingStrength.DefaultValue);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new SmoothingStrength(SmoothingStrength.DefaultValue);
            }
            
            // Extract value with automatic clamping in SmoothingStrength constructor
            var value = arg.AsFloat32();
            return new SmoothingStrength(value);
        }

        public Message ToOSCMessage(SmoothingStrength smoothingStrength)
        {
            return new Message(OSCCameraEndpoints.SmoothingStrength, new[] { new Argument(smoothingStrength.Value) });
        }
    }
}