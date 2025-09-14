using OSC;
using Parameters;

namespace VRCCamera
{
    public class TriggerTakesPhotosToggleConverter : IOSCMessageConverter<TriggerTakesPhotosToggle>
    {
        public TriggerTakesPhotosToggle FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.TriggerTakesPhotos)
            {
                return new TriggerTakesPhotosToggle(false);
            }

            if (message.Arguments is not { Length: > 0 })
            {
                return new TriggerTakesPhotosToggle(false);
            }

            var arg = message.Arguments[0];
            bool value = arg.Type switch
            {
                Argument.ValueType.Bool => arg.AsBool(),
                Argument.ValueType.Int32 => arg.AsInt32() != 0,
                Argument.ValueType.Float32 => arg.AsFloat32() != 0f,
                _ => false
            };

            return new TriggerTakesPhotosToggle(value);
        }

        public Message ToOSCMessage(TriggerTakesPhotosToggle toggle)
        {
            return new Message(OSCCameraEndpoints.TriggerTakesPhotos, new[] { new Argument(toggle.Value) });
        }
    }
}

