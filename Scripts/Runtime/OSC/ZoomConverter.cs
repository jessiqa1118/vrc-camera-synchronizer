namespace JessiQa
{
    public class ZoomConverter : IOSCMessageConverter<Zoom>
    {
        public Zoom FromOSCMessage(Message message)
        {
            // Validate OSC address
            if (message.Address != OSCCameraEndpoints.Zoom)
            {
                return new Zoom(Zoom.MinValue, true);
            }
            
            // Validate arguments exist and count
            if (message.Arguments is not { Length: 1 })
            {
                return new Zoom(Zoom.MinValue, true);
            }

            var arg = message.Arguments[0];
            
            // Validate argument type
            if (arg.Type != Argument.ValueType.Float32)
            {
                return new Zoom(Zoom.MinValue, true);
            }
            
            // Extract value with automatic clamping in Zoom constructor
            var value = arg.AsFloat32();
            return new Zoom(value, true);
        }

        public Message ToOSCMessage(Zoom zoom)
        {
            return new Message(OSCCameraEndpoints.Zoom, new[] { new Argument(zoom.Value) });
        }
    }
}