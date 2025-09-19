using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct LookAtMeXOffsetOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.LookAtMeXOffset;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public LookAtMeXOffsetOscMessage(LookAtMeXOffset offset)
        {
            Arguments = new[] { new Argument(offset) };
        }
    }
}