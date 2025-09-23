using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct LookAtMeYOffsetOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.LookAtMeYOffset;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public LookAtMeYOffsetOscMessage(LookAtMeYOffset offset)
        {
            Arguments = new[] { new Argument(offset) };
        }
    }
}