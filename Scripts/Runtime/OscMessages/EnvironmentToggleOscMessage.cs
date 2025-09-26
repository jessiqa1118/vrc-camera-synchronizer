using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct EnvironmentToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Environment;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public EnvironmentToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
