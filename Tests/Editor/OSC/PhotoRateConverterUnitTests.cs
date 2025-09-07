using NUnit.Framework;
using Parameters;
using OSC;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class PhotoRateConverterUnitTests
    {
        private PhotoRateConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new PhotoRateConverter();
        }
        
        [Test]
        public void ToOSCMessage_CreatesPhotoRateMessage()
        {
            // Arrange
            var photoRate = new PhotoRate(1.5f);
            
            // Act
            var message = _converter.ToOSCMessage(photoRate);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.PhotoRate, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(1.5f, message.Arguments[0].AsFloat32());
        }
        
        [Test]
        public void FromOSCMessage_WithValidMessage_ReturnsPhotoRate()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.PhotoRate, new[]
            {
                new Argument(1.2f)
            });
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(1.2f, photoRate.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefault()
        {
            // Arrange
            var message = new Message(new Address("/wrong/address"), new[]
            {
                new Argument(1.2f)
            });
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(PhotoRate.DefaultValue, photoRate.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNoArguments_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.PhotoRate, new Argument[0]);
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(PhotoRate.DefaultValue, photoRate.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongArgumentType_ReturnsDefault()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.PhotoRate, new[]
            {
                new Argument(123) // Int32 instead of Float32
            });
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(PhotoRate.DefaultValue, photoRate.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.PhotoRate, new[]
            {
                new Argument(3f)
            });
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(PhotoRate.MaxValue, photoRate.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            var message = new Message(OSCCameraEndpoints.PhotoRate, new[]
            {
                new Argument(0.01f)
            });
            
            // Act
            var photoRate = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(PhotoRate.MinValue, photoRate.Value);
        }
    }
}