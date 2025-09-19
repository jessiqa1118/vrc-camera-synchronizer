using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class SmoothingStrengthConverterUnitTests
    {
        private SmoothingStrengthConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new SmoothingStrengthConverter();
        }
        
        [Test]
        public void ToOSCMessage_CreatesSmoothingStrengthMessage()
        {
            // Arrange
            var smoothingStrength = new SmoothingStrength(7.5f);
            
            // Act
            var message = _converter.ToOSCMessage(smoothingStrength);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.SmoothingStrength, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(7.5f, message.Arguments[0].AsFloat32());
        }
        
        [Test]
        public void FromOSCMessage_WithValidMessage_ReturnsSmoothingStrength()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.SmoothingStrength, new[]
            {
                new Argument(6f)
            });
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(6f, smoothingStrength.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefault()
        {
            // Arrange
            var message = new Message(new Address("/wrong/address"), new[]
            {
                new Argument(6f)
            });
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.DefaultValue, smoothingStrength.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNoArguments_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.SmoothingStrength, new Argument[0]);
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.DefaultValue, smoothingStrength.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongArgumentType_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.SmoothingStrength, new[]
            {
                new Argument(123) // Int32 instead of Float32
            });
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.DefaultValue, smoothingStrength.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.SmoothingStrength, new[]
            {
                new Argument(15f)
            });
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.MaxValue, smoothingStrength.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.SmoothingStrength, new[]
            {
                new Argument(0.01f)
            });
            
            // Act
            var smoothingStrength = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.MinValue, smoothingStrength.Value);
        }
    }
}