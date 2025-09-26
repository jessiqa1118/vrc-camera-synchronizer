using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct ApertureOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Aperture;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public ApertureOscMessage(Aperture aperture)
        {
            Arguments = new[] { new Argument(aperture) };
            TypeTag = new TypeTag("f");
        }
    }
}
