using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct LightnessOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Lightness;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public LightnessOscMessage(Lightness lightness)
        {
            Arguments = new[] { new Argument(lightness) };
        }
    }
}
