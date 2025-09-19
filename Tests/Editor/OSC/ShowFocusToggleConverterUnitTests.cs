using NUnit.Framework;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ShowFocusToggleConverterUnitTests
    {
        private ShowFocusToggleConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new ShowFocusToggleConverter();
        }

        [Test]
        public void ToOSCMessage_BuildsMessage_WithBool()
        {
            var msgTrue = _converter.ToOSCMessage(true);
            Assert.AreEqual(OSCCameraEndpoints.ShowFocus.Value, msgTrue.Address.Value);
            Assert.AreEqual(1, msgTrue.Arguments.Length);
            Assert.AreEqual(true, msgTrue.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, msgTrue.Arguments[0].Type);
            Assert.AreEqual("T", msgTrue.TypeTag.Value);

            var msgFalse = _converter.ToOSCMessage(false);
            Assert.AreEqual(OSCCameraEndpoints.ShowFocus.Value, msgFalse.Address.Value);
            Assert.AreEqual(1, msgFalse.Arguments.Length);
            Assert.AreEqual(false, msgFalse.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, msgFalse.Arguments[0].Type);
            Assert.AreEqual("F", msgFalse.TypeTag.Value);
        }

        [Test]
        public void FromOSCMessage_ParsesVariousTypes()
        {
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(true) })));
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(false) })));
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(1) })));
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(0) })));
            Assert.IsTrue(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(1.0f) })));
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument(0.0f) })));
        }

        [Test]
        public void FromOSCMessage_InvalidInputs_ReturnsFalse()
        {
            Assert.IsFalse(_converter.FromOSCMessage(new Message(new Address("/other"), new[] { new Argument(true) })));
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, System.Array.Empty<Argument>())));
            Assert.IsFalse(_converter.FromOSCMessage(new Message(OSCCameraEndpoints.ShowFocus, new[] { new Argument("true") })));
        }
    }
}
