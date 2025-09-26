using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct RollWhileFlyingToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.RollWhileFlying;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public RollWhileFlyingToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
