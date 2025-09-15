using OSC;
using Parameters;

namespace VRCCamera
{
    public class ModeConverter : IOSCMessageConverter<Mode>
    {
        public Mode FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Mode)
            {
                return Mode.Off;
            }

            if (message.Arguments is not { Length: 1 })
            {
                return Mode.Off;
            }

            var arg = message.Arguments[0];
            if (arg.Type == Argument.ValueType.Int32)
            {
                var value = arg.AsInt32();
                return value switch
                {
                    0 => Mode.Off,
                    1 => Mode.Photo,
                    2 => Mode.Stream,
                    3 => Mode.Emoji,
                    4 => Mode.Multilayer,
                    5 => Mode.Print,
                    6 => Mode.Drone,
                    _ => Mode.Off
                };
            }

            // Unsupported type
            return Mode.Off;
        }

        public Message ToOSCMessage(Mode mode)
        {
            var intValue = mode switch
            {
                Mode.Off => 0,
                Mode.Photo => 1,
                Mode.Stream => 2,
                Mode.Emoji => 3,
                Mode.Multilayer => 4,
                Mode.Print => 5,
                Mode.Drone => 6,
                _ => 0
            };

            return new Message(OSCCameraEndpoints.Mode, new[] { new Argument(intValue) });
        }
    }
}

