using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct TurnSpeedOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.TurnSpeed;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public TurnSpeedOscMessage(TurnSpeed speed)
        {
            Arguments = new[] { new Argument(speed) };
        }
    }
}
