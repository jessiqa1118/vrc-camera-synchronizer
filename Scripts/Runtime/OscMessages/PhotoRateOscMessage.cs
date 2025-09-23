using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct PhotoRateOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.PhotoRate;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public PhotoRateOscMessage(PhotoRate rate)
        {
            Arguments = new[] { new Argument(rate) };
        }
    }
}
