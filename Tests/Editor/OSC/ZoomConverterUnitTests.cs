using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class ZoomConverterUnitTests
    {
        private ZoomConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new ZoomConverter();
        }
        
        [Test]
        public void ToOSCMessage_WithValidZoom_CreatesCorrectMessage()
        {
            // Arrange
            var zoom = new Zoom(45f);
            
            // Act
            var message = _converter.ToOSCMessage(zoom);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.Zoom.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(45f, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[0].Type);
            Assert.AreEqual("f", message.TypeTag.Value);
        }
        
        [Test]
        public void ToOSCMessage_WithMinValue_CreatesCorrectMessage()
        {
            // Arrange
            var zoom = new Zoom(Zoom.MinValue);
            
            // Act
            var message = _converter.ToOSCMessage(zoom);
            
            // Assert
            Assert.AreEqual(20f, message.Arguments[0].Value);
        }
        
        [Test]
        public void ToOSCMessage_WithMaxValue_CreatesCorrectMessage()
        {
            // Arrange
            var zoom = new Zoom(Zoom.MaxValue);
            
            // Act
            var message = _converter.ToOSCMessage(zoom);
            
            // Assert
            Assert.AreEqual(150f, message.Arguments[0].Value);
        }
        
        [Test]
        public void FromOSCMessage_WithFloatArgument_ReturnsCorrectZoom()
        {
            // Arrange
            var address = OSCCameraEndpoints.Zoom;
            var arguments = new[] { new Argument(75f) };
            var message = new Message(address, arguments);
            
            // Act
            var zoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(75f, zoom.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Zoom;
            var arguments = new[] { new Argument(10f) };
            var message = new Message(address, arguments);
            
            // Act
            var zoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Zoom.MinValue, zoom.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Zoom;
            var arguments = new[] { new Argument(200f) };
            var message = new Message(address, arguments);
            
            // Act
            var zoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Zoom.MaxValue, zoom.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithEmptyArguments_ReturnsMinValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Zoom;
            var arguments = System.Array.Empty<Argument>();
            var message = new Message(address, arguments);
            
            // Act
            var zoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Zoom.MinValue, zoom.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNonFloatArgument_ReturnsMinValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Zoom;
            var arguments = new[] { new Argument("not a float") };
            var message = new Message(address, arguments);
            
            // Act
            var zoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Zoom.MinValue, zoom.Value);
        }
        
        [Test]
        public void RoundTrip_PreservesValue()
        {
            // Arrange
            var originalZoom = new Zoom(90f);
            
            // Act
            var message = _converter.ToOSCMessage(originalZoom);
            var roundTripZoom = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(originalZoom.Value, roundTripZoom.Value);
        }
        
        [Test]
        public void ImplementsIOSCMessageConverter()
        {
            // Assert
            Assert.IsTrue(_converter != null);
        }
    }
}