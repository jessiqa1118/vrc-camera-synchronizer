using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class TurnSpeedConverterUnitTests
    {
        private TurnSpeedConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new TurnSpeedConverter();
        }
        
        [Test]
        public void ToOSCMessage_CreatesTurnSpeedMessage()
        {
            // Arrange
            var turnSpeed = new TurnSpeed(2.5f);
            
            // Act
            var message = _converter.ToOSCMessage(turnSpeed);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.TurnSpeed, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(2.5f, message.Arguments[0].AsFloat32());
        }
        
        [Test]
        public void FromOSCMessage_WithValidMessage_ReturnsTurnSpeed()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.TurnSpeed, new[]
            {
                new Argument(3f)
            });
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(3f, turnSpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefault()
        {
            // Arrange
            var message = new Message(new Address("/wrong/address"), new[]
            {
                new Argument(3f)
            });
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(TurnSpeed.DefaultValue, turnSpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNoArguments_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.TurnSpeed, new Argument[0]);
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(TurnSpeed.DefaultValue, turnSpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongArgumentType_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.TurnSpeed, new[]
            {
                new Argument(123) // Int32 instead of Float32
            });
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(TurnSpeed.DefaultValue, turnSpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.TurnSpeed, new[]
            {
                new Argument(10f)
            });
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(TurnSpeed.MaxValue, turnSpeed.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.TurnSpeed, new[]
            {
                new Argument(0.01f)
            });
            
            // Act
            var turnSpeed = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(TurnSpeed.MinValue, turnSpeed.Value);
        }
    }
}