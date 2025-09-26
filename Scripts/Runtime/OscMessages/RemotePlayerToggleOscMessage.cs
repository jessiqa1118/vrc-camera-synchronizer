using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct RemotePlayerToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.RemotePlayer;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public RemotePlayerToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
