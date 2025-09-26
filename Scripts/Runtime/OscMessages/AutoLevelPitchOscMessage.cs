using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct AutoLevelPitchToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.AutoLevelPitch;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public AutoLevelPitchToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}

