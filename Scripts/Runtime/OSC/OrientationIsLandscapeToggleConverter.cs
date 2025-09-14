using OSC;
using Parameters;

namespace VRCCamera
{
    public class OrientationIsLandscapeToggleConverter : IOSCMessageConverter<OrientationIsLandscapeToggle>
    {
        public OrientationIsLandscapeToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.OrientationIsLandscape)
            {
                return new OrientationIsLandscapeToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new OrientationIsLandscapeToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new OrientationIsLandscapeToggle(value);
        }

        public Message ToOSCMessage(OrientationIsLandscapeToggle toggle)
        {
            return new Message(OSCCameraEndpoints.OrientationIsLandscape, new[] { new Argument(toggle.Value) });
        }
    }
}

