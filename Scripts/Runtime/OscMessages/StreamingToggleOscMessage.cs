using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct StreamingToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Streaming;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public StreamingToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
