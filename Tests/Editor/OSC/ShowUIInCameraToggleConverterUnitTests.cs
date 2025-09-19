using NUnit.Framework;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ShowUIInCameraToggleConverterUnitTests
    {
        private ShowUIInCameraToggleConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new ShowUIInCameraToggleConverter();
        }
        
        [Test]
        public void ToOSCMessage_WithTrueToggle_CreatesCorrectMessage()
        {
            // Act
            var message = _converter.ToOSCMessage(true);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.ShowUIInCamera.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(true, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            Assert.AreEqual("T", message.TypeTag.Value);
        }
        
        [Test]
        public void ToOSCMessage_WithFalseToggle_CreatesCorrectMessage()
        {
            // Act
            var message = _converter.ToOSCMessage(false);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.ShowUIInCamera.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(false, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            Assert.AreEqual("F", message.TypeTag.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithBooleanTrueArgument_ReturnsCorrectToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(true) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsTrue(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithBooleanFalseArgument_ReturnsCorrectToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(false) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsFalse(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithInt32OneArgument_ReturnsTrueToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(1) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsTrue(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithInt32ZeroArgument_ReturnsFalseToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(0) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsFalse(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithFloat32NonZeroArgument_ReturnsTrueToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(1.0f) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsTrue(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithFloat32ZeroArgument_ReturnsFalseToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(0.0f) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsFalse(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsFalseToggle()
        {
            // Arrange
            var address = new Address("/some/other/address");
            var arguments = new[] { new Argument(true) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsFalse(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithEmptyArguments_ReturnsFalseToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var message = new Message(address, System.Array.Empty<Argument>());
            
            // Act
            var toggle = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.IsFalse(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithTooManyArguments_UsesFirstArgument()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument(true), new Argument(false) };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsTrue(toggle);
        }
        
        [Test]
        public void FromOSCMessage_WithUnsupportedArgumentType_ReturnsFalseToggle()
        {
            // Arrange
            var address = OSCCameraEndpoints.ShowUIInCamera;
            var arguments = new[] { new Argument("true") };
            var message = new Message(address, arguments);
            
            // Act
            var toggle = _converter.FromOSCMessage(message);

            // Assert
            Assert.IsFalse(toggle);
        }
    }
}
