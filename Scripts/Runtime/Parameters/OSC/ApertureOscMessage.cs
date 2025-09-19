using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct ApertureOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Exposure;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public ApertureOscMessage(Aperture aperture)
        {
            Arguments = new[] { new Argument(aperture) };
        }
    }
}