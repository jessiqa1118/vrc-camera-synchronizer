using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct DurationOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Duration;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public DurationOscMessage(Duration duration)
        {
            Arguments = new[] { new Argument(duration) };
        }
    }
}
