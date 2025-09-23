using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public struct FlySpeedOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.FlySpeed;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public FlySpeedOscMessage(FlySpeed flySpeed)
        {
            Arguments = new[] { new Argument(flySpeed) };
        }
    }
}
