using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct ShowFocusToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.ShowFocus;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public ShowFocusToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
