using NUnit.Framework;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class LockToggleConverterUnitTests
    {
        private LockToggleConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new LockToggleConverter();
        }

        [Test]
        public void ToOSCMessage_WithTrueToggle_CreatesCorrectMessage()
        {
            var message = _converter.ToOSCMessage(true);
            Assert.AreEqual(OSCCameraEndpoints.Lock.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(true, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            Assert.AreEqual("T", message.TypeTag.Value);
        }

        [Test]
        public void ToOSCMessage_WithFalseToggle_CreatesCorrectMessage()
        {
            var message = _converter.ToOSCMessage(false);
            Assert.AreEqual(OSCCameraEndpoints.Lock.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(false, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            Assert.AreEqual("F", message.TypeTag.Value);
        }

        [Test]
        public void FromOSCMessage_WithBooleanTrueArgument_ReturnsCorrectToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(true) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsTrue(toggle);
        }

        [Test]
        public void FromOSCMessage_WithBooleanFalseArgument_ReturnsCorrectToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(false) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }

        [Test]
        public void FromOSCMessage_WithInt32OneArgument_ReturnsTrueToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(1) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsTrue(toggle);
        }

        [Test]
        public void FromOSCMessage_WithInt32ZeroArgument_ReturnsFalseToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(0) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }

        [Test]
        public void FromOSCMessage_WithFloat32NonZeroArgument_ReturnsTrueToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(1.0f) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsTrue(toggle);
        }

        [Test]
        public void FromOSCMessage_WithFloat32ZeroArgument_ReturnsFalseToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(0.0f) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }

        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsFalseToggle()
        {
            var message = new Message(new Address("/some/other/address"), new[] { new Argument(true) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }

        [Test]
        public void FromOSCMessage_WithEmptyArguments_ReturnsFalseToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, System.Array.Empty<Argument>());
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }

        [Test]
        public void FromOSCMessage_WithTooManyArguments_UsesFirstArgument()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument(true), new Argument(false) });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsTrue(toggle);
        }

        [Test]
        public void FromOSCMessage_WithUnsupportedArgumentType_ReturnsFalseToggle()
        {
            var message = new Message(OSCCameraEndpoints.Lock, new[] { new Argument("true") });
            var toggle = _converter.FromOSCMessage(message);
            Assert.IsFalse(toggle);
        }
    }
}
