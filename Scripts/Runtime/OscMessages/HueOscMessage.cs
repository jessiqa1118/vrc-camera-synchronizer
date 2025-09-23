using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct HueOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Hue;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public HueOscMessage(Hue hue)
        {
            Arguments = new[] { new Argument(hue) };
        }
    }
}