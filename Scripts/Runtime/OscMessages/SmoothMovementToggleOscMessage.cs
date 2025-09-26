using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct SmoothMovementToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.SmoothMovement;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public SmoothMovementToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
