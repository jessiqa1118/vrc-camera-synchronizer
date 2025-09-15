using OSC;
using Parameters;

namespace VRCCamera
{
    /// <summary>
    /// Converts OSC messages to/from Pose for the /usercamera/Pose endpoint.
    /// Layout: pos.x, pos.y, pos.z, rot.x, rot.y, rot.z (floats)
    /// </summary>
    public class PoseConverter : IOSCMessageConverter<Pose>
    {
        public Pose FromOSCMessage(Message message)
        {
            if (message.Address != OSCCameraEndpoints.Pose)
            {
                return new Pose(new Pose.Position(0, 0, 0), new Pose.Rotation(0, 0, 0));
            }

            if (message.Arguments is not { Length: 6 })
            {
                return new Pose(new Pose.Position(0, 0, 0), new Pose.Rotation(0, 0, 0));
            }

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

            var px = Read(0);
            var py = Read(1);
            var pz = Read(2);
            var rx = Read(3);
            var ry = Read(4);
            var rz = Read(5);

            return new Pose(new Pose.Position(px, py, pz), new Pose.Rotation(rx, ry, rz));
        }

        public Message ToOSCMessage(Pose pose)
        {
            return new Message(OSCCameraEndpoints.Pose, new[]
            {
                new Argument(pose.Pos.X),
                new Argument(pose.Pos.Y),
                new Argument(pose.Pos.Z),
                new Argument(pose.Rot.X),
                new Argument(pose.Rot.Y),
                new Argument(pose.Rot.Z)
            });
        }
    }
}

