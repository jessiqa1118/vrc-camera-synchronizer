using NUnit.Framework;
using Parameters;
using OSC;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class GreenScreenToggleConverterUnitTests
    {
        private GreenScreenToggleConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new GreenScreenToggleConverter();
        }

        [Test]
        public void ToOSCMessage_BuildsMessage_WithBool()
        {
            var msgTrue = _converter.ToOSCMessage(new GreenScreenToggle(true));
            Assert.AreEqual(OSCCameraEndpoints.GreenScreen.Value, msgTrue.Address.Value);
            Assert.AreEqual(1, msgTrue.Arguments.Length);
            Assert.AreEqual(true, msgTrue.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, msgTrue.Arguments[0].Type);
            Assert.AreEqual("T", msgTrue.TypeTag.Value);

            var msgFalse = _converter.ToOSCMessage(new GreenScreenToggle(false));
            Assert.AreEqual(OSCCameraEndpoints.GreenScreen.Value, msgFalse.Address.Value);
            Assert.AreEqual(1, msgFalse.Arguments.Length);
            Assert.AreEqual(false, msgFalse.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, msgFalse.Arguments[0].Type);
            Assert.AreEqual("F", msgFalse.TypeTag.Value);
        }

        [Test]
        public void FromOSCMessage_ParsesVariousTypes()
        {
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(true) })).Value);
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(false) })).Value);
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(1) })).Value);
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(0) })).Value);
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(1.0f) })).Value);
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument(0.0f) })).Value);
        }

        [Test]
        public void FromOSCMessage_InvalidInputs_ReturnsFalse()
        {
            Assert.IsFalse(_converter.FromOSCMessage(new Message(new Address("/other"), new[] { new Argument(true) })).Value);
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, System.Array.Empty<Argument>())).Value);
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.GreenScreen, new[] { new Argument("true") })).Value);
        }
    }
}

