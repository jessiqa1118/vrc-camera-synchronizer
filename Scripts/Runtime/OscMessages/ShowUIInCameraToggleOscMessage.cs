using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct ShowUIInCameraToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.ShowUIInCamera;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public ShowUIInCameraToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
