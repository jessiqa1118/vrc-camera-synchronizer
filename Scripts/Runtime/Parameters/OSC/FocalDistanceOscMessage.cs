using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    public struct FocalDistanceOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.FocalDistance;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("f");

        public FocalDistanceOscMessage(FocalDistance focalDistance)
        {
            Arguments = new[] { new Argument(focalDistance) };
        }
    }
}