using OSC;
using Parameters;

namespace VRCCamera
{
    public class StreamingToggleConverter : IOSCMessageConverter<StreamingToggle>
    {
        public StreamingToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Streaming)
            {
                return new StreamingToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new StreamingToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new StreamingToggle(value);
        }

        public Message ToOSCMessage(StreamingToggle toggle)
        {
            return new Message(OSCCameraEndpoints.Streaming, new[] { new Argument(toggle.Value) });
        }
    }
}

