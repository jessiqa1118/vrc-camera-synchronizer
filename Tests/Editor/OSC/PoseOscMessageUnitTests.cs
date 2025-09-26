using NUnit.Framework;
using UnityEngine;
using Astearium.Network.Osc;
using Astearium.VRChat.Camera.Tests.Utility;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class PoseOscMessageUnitTests
    {
        [Test]
        public void Constructor_FromPose_WritesSixFloats()
        {
            var pose = new Pose(new Vector3(1f, 2f, 3f), Quaternion.Euler(10f, 20f, 30f));
            var message = new PoseOscMessage(pose);

            Assert.AreEqual(OSCCameraEndpoints.Pose.Value, message.Address.Value);
            Assert.AreEqual("ffffff", message.TypeTag.Value);
            Assert.AreEqual(6, message.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[0].Type);
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[5].Type);
            Assert.AreEqual(1f, message.Arguments[0].AsFloat32());
            MathAssert.AreApproximatelyEqual(30f, message.Arguments[5].AsFloat32());
        }

    }
}
