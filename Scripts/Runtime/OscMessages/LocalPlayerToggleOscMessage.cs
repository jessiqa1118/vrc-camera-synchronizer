using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct LocalPlayerToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.LocalPlayer;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public LocalPlayerToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
