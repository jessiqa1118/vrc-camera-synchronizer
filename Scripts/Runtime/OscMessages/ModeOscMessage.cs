using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct ModeOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Mode;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("i");

        public ModeOscMessage(Mode mode)
        {
            Arguments = new[] { new Argument((int)mode) };
        }
    }
}
