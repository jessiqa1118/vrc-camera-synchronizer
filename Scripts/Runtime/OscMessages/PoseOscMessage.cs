using UnityEngine;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera
{
    public readonly struct PoseOscMessage : IOSCMessage
    {
        public Address Address => OSCCameraEndpoints.Pose;
        public Argument[] Arguments { get; }
        public TypeTag TypeTag => new("ffffff");

        public PoseOscMessage(Pose pose)
        {
            var euler = pose.rotation.eulerAngles;
            Arguments = new[]
            {
                new Argument(pose.position.x),
                new Argument(pose.position.y),
                new Argument(pose.position.z),
                new Argument(euler.x),
                new Argument(euler.y),
                new Argument(euler.z)
            };
        }
    }
}
