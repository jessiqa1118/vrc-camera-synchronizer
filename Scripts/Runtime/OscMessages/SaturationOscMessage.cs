using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct SaturationOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Saturation;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public SaturationOscMessage(Saturation saturation)
        {
            Arguments = new[] { new Argument(saturation) };
        }
    }
}