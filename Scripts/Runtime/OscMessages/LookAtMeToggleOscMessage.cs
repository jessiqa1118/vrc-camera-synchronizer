using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct LookAtMeToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.LookAtMe;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public LookAtMeToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
