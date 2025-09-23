using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct GreenScreenToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.GreenScreen;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public GreenScreenToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
