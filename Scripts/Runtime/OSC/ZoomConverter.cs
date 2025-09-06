namespace JessiQa
{
    public class ZoomConverter : IOSCMessageConverter<Zoom>
    {
        public Zoom FromOSCMessage(Message message)
        {
            if (message.Arguments == null || message.Arguments.Length == 0)
            {
                return new Zoom(Zoom.MinValue);
            }

            var firstArg = message.Arguments[0];
            
            return firstArg.Type switch
            {
                Argument.ValueType.Float32 => new Zoom(firstArg.AsFloat32()),
                _ => new Zoom(Zoom.MinValue)
            };
        }

        public Message ToOSCMessage(Zoom zoom)
        {
            return new Message(OSCCameraEndpoints.Zoom, new[] { new Argument(zoom.Value) });
        }
    }
}