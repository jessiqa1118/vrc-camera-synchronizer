using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct DollyPathsStayVisibleToggleOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.DollyPathsStayVisible;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }

        public DollyPathsStayVisibleToggleOscMessage(bool toggle)
        {
            Arguments = new[] { new Argument(toggle) };
            TypeTag = new TypeTag(toggle ? "T" : "F");
        }
    }
}
