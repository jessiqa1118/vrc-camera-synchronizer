using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct LockToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Lock;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public LockToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
