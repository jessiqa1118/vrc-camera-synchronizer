using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct AutoLevelRollToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.AutoLevelRoll;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public AutoLevelRollToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
