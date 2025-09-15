using NUnit.Framework;
using OSC;
using UnityEngine;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class PoseConverterUnitTests
    {
        private PoseConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new PoseConverter();
        }

        [Test]
        public void ToOSCMessage_BuildsMessage_WithSixFloats()
        {
            var pose = new Pose(new Vector3(1f, 2f, 3f), Quaternion.Euler(10f, 20f, 30f));
            var msg = _converter.ToOSCMessage(pose);

            Assert.AreEqual(OSCCameraEndpoints.Pose.Value, msg.Address.Value);
            Assert.AreEqual(6, msg.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Float32, msg.Arguments[0].Type);
            Assert.AreEqual(Argument.ValueType.Float32, msg.Arguments[5].Type);
            Assert.AreEqual(1f, msg.Arguments[0].AsFloat32());
            Assert.AreEqual(30f, msg.Arguments[5].AsFloat32());
        }

        [Test]
        public void FromOSCMessage_WithFloats_ReturnsPose()
        {
            var msg = new Message(OSCCameraEndpoints.Pose, new[]
            {
                new Argument(1f), new Argument(2f), new Argument(3f),
                new Argument(10f), new Argument(20f), new Argument(30f)
            });
            var pose = _converter.FromOSCMessage(msg);
            Assert.AreEqual(1f, pose.position.x);
            Assert.AreEqual(2f, pose.position.y);
            Assert.AreEqual(3f, pose.position.z);
            Assert.AreEqual(10f, pose.rotation.eulerAngles.x);
            Assert.AreEqual(20f, pose.rotation.eulerAngles.y);
            Assert.AreEqual(30f, pose.rotation.eulerAngles.z);
        }

        [Test]
        public void FromOSCMessage_WithInts_ReturnsPose()
        {
            var msg = new Message(OSCCameraEndpoints.Pose, new[]
            {
                new Argument(1), new Argument(2), new Argument(3),
                new Argument(10), new Argument(20), new Argument(30)
            });
            var pose = _converter.FromOSCMessage(msg);
            Assert.AreEqual(1f, pose.position.x);
            Assert.AreEqual(2f, pose.position.y);
            Assert.AreEqual(3f, pose.position.z);
            Assert.AreEqual(10f, pose.rotation.eulerAngles.x);
            Assert.AreEqual(20f, pose.rotation.eulerAngles.y);
            Assert.AreEqual(30f, pose.rotation.eulerAngles.z);
        }

        [Test]
        public void FromOSCMessage_WrongAddress_ReturnsZeroPose()
        {
            var msg = new Message(new Address("/wrong"), new[]
            {
                new Argument(1f), new Argument(2f), new Argument(3f),
                new Argument(10f), new Argument(20f), new Argument(30f)
            });
            var pose = _converter.FromOSCMessage(msg);
            Assert.AreEqual(0f, pose.position.x);
            Assert.AreEqual(0f, pose.position.y);
            Assert.AreEqual(0f, pose.position.z);
            Assert.AreEqual(0f, pose.rotation.eulerAngles.x);
            Assert.AreEqual(0f, pose.rotation.eulerAngles.y);
            Assert.AreEqual(0f, pose.rotation.eulerAngles.z);
        }
    }
}



