using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct ZoomOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Zoom;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public ZoomOscMessage(Zoom zoom)
        {
            Arguments = new[] { new Argument(zoom) };
        }
    }
}
