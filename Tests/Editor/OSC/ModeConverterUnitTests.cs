using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ModeConverterUnitTests
    {
        private ModeConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new ModeConverter();
        }

        [Test]
        public void ToOSCMessage_BuildsMessage_WithInt32()
        {
            var msg = _converter.ToOSCMessage(Mode.Stream);
            Assert.AreEqual(OSCCameraEndpoints.Mode.Value, msg.Address.Value);
            Assert.AreEqual(1, msg.Arguments.Length);
            Assert.AreEqual(2, msg.Arguments[0].AsInt32());
            Assert.AreEqual(Argument.ValueType.Int32, msg.Arguments[0].Type);
            Assert.AreEqual("i", msg.TypeTag.Value);
        }

        [Test]
        public void FromOSCMessage_WithValidInt_ReturnsEnum()
        {
            Assert.AreEqual(Mode.Off, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(0) })));
            Assert.AreEqual(Mode.Photo, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(1) })));
            Assert.AreEqual(Mode.Stream, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(2) })));
            Assert.AreEqual(Mode.Emoji, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(3) })));
            Assert.AreEqual(Mode.Multilayer, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(4) })));
            Assert.AreEqual(Mode.Print, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(5) })));
            Assert.AreEqual(Mode.Drone, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(6) })));
        }

        [Test]
        public void FromOSCMessage_WithFloatIndex_TruncatesToEnum()
        {
            Assert.AreEqual(Mode.Photo, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(1.9f) })));
            Assert.AreEqual(Mode.Stream, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(2.0f) })));
        }

        [Test]
        public void FromOSCMessage_InvalidInputs_ReturnsOff()
        {
            Assert.AreEqual(Mode.Off, _converter.FromOSCMessage(new Message(new Address("/other"), new[] { new Argument(2) })));
            Assert.AreEqual(Mode.Off, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, System.Array.Empty<Argument>())));
            Assert.AreEqual(Mode.Off, _converter.FromOSCMessage(new Message(OSCCameraEndpoints.Mode, new[] { new Argument(999) })));
        }
    }
}
