using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct FlyingToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Flying;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public FlyingToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
