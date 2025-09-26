using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct ExposureOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Exposure;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public ExposureOscMessage(Exposure exposure)
        {
            Arguments = new[] { new Argument(exposure) };
        }
    }
}
