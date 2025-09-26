using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct CameraEarsToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.CameraEars;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public CameraEarsToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
