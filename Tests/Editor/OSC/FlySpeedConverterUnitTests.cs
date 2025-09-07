using NUnit.Framework;
using Parameters;
using OSC;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class FlySpeedConverterUnitTests
    {
        private FlySpeedConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new FlySpeedConverter();
        }
        
        [Test]
        public void ToOSCMessage_CreatesFlySpeedMessage()
        {
            // Arrange
            var flySpeed = new FlySpeed(5f);
            
            // Act
            var message = _converter.ToOSCMessage(flySpeed);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.FlySpeed, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(5f, message.Arguments[0].AsFloat32());
        }
        
        [Test]
        public void FromOSCMessage_WithValidMessage_ReturnsFlySpeed()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.FlySpeed, new[]
            {
                new Argument(8f)
            });
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(8f, flySpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefault()
        {
            // Arrange
            var message = new Message(new Address("/wrong/address"), new[]
            {
                new Argument(8f)
            });
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(FlySpeed.DefaultValue, flySpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNoArguments_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.FlySpeed, new Argument[0]);
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(FlySpeed.DefaultValue, flySpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongArgumentType_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.FlySpeed, new[]
            {
                new Argument(123) // Int32 instead of Float32
            });
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(FlySpeed.DefaultValue, flySpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.FlySpeed, new[]
            {
                new Argument(20f)
            });
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(FlySpeed.MaxValue, flySpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.FlySpeed, new[]
            {
                new Argument(0.01f)
            });
            
            // Act
            var flySpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(FlySpeed.MinValue, flySpeed.Value);
        }
    }
}