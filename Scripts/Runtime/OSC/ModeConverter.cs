using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Converts OSC messages to/from <see cref="Mode"/> for the /usercamera/Mode endpoint.
    /// </summary>
    public class ModeConverter : IOSCMessageConverter<Mode>
    {
        /// <summary>
        /// Parses a <see cref="Message"/> into a <see cref="Mode"/> value.
        /// Accepts Int32 indices (0-6). Float32 values are also accepted by truncation for robustness.
        /// Returns <see cref="Mode.Off"/> on invalid input.
        /// </summary>
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
            int value;
            switch (arg.Type)
            {
                case Argument.ValueType.Int32:
                    value = arg.AsInt32();
                    break;
                case Argument.ValueType.Float32:
                    value = (int)arg.AsFloat32();
                    break;
                default:
                    return Mode.Off;
            }

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

        /// <summary>
        /// Builds a <see cref="Message"/> containing the OSC index for the given <see cref="Mode"/>.
        /// </summary>
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
