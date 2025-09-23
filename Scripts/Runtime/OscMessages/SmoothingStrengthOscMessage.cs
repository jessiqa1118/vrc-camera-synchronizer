using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct SmoothingStrengthOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.SmoothingStrength;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public SmoothingStrengthOscMessage(SmoothingStrength strength)
        {
            Arguments = new[] { new Argument(strength) };
        }
    }
}
