using UnityEngine;
using Astearium.Osc;

namespace Astearium.VRChat.Camera
{
    /// <summary>
    /// Converts OSC messages to/from Pose for the /usercamera/Pose endpoint.
    /// Layout: pos.x, pos.y, pos.z, rot.x, rot.y, rot.z (floats)
    /// </summary>
    public class PoseConverter : IOSCMessageConverter<Pose>
    {
        public Pose FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Pose || message.Arguments is not { Length: 6 })
            {
                return new Pose(Vector3.zero, Quaternion.identity);
            }

            var px = Read(0);
            var py = Read(1);
            var pz = Read(2);
            var rx = Read(3);
            var ry = Read(4);
            var rz = Read(5);

            return new Pose(new Vector3(px, py, pz), Quaternion.Euler(rx, ry, rz));

            float Read(int i)
            {
                var arg = message.Arguments[i];
                return arg.Type switch
                {
                    Argument.ValueType.Float32 => arg.AsFloat32(),
                    Argument.ValueType.Int32 => arg.AsInt32(),
                    _ => 0f
                };
            }
        }

        public Message ToOSCMessage(Pose pose)
        {
            return new Message(OSCCameraEndpoints.Pose, new[]
            {
                new Argument(pose.position.x),
                new Argument(pose.position.y),
                new Argument(pose.position.z),
                new Argument(pose.rotation.eulerAngles.x),
                new Argument(pose.rotation.eulerAngles.y),
                new Argument(pose.rotation.eulerAngles.z)
            });
        }
    }
}

