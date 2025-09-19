using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class DurationConverterUnitTests
    {
        private DurationConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new DurationConverter();
        }
        
        [Test]
        public void ToOSCMessage_CreatesDurationMessage()
        {
            // Arrange
            var duration = new Duration(10f);
            
            // Act
            var message = _converter.ToOSCMessage(duration);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.Duration, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(10f, message.Arguments[0].AsFloat32());
        }
        
        [Test]
        public void FromOSCMessage_WithValidMessage_ReturnsDuration()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.Duration, new[]
            {
                new Argument(30f)
            });
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(30f, duration.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefault()
        {
            // Arrange
            var message = new Message(new Address("/wrong/address"), new[]
            {
                new Argument(30f)
            });
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Duration.DefaultValue, duration.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNoArguments_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.Duration, new Argument[0]);
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Duration.DefaultValue, duration.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongArgumentType_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.Duration, new[]
            {
                new Argument(123) // Int32 instead of Float32
            });
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Duration.DefaultValue, duration.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.Duration, new[]
            {
                new Argument(100f)
            });
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Duration.MaxValue, duration.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.Duration, new[]
            {
                new Argument(0.01f)
            });
            
            // Act
            var duration = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Duration.MinValue, duration.Value);
        }
    }
}