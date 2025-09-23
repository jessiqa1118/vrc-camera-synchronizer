using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct TriggerTakesPhotosToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.TriggerTakesPhotos;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public TriggerTakesPhotosToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
