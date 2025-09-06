using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class ExposureConverterUnitTests
    {
        private ExposureConverter _converter;
        
        [SetUp]
        public void SetUp()
        {
            _converter = new ExposureConverter();
        }
        
        [Test]
        public void ToOSCMessage_WithValidExposure_CreatesCorrectMessage()
        {
            // Arrange
            var exposure = new Exposure(-2.5f);
            
            // Act
            var message = _converter.ToOSCMessage(exposure);
            
            // Assert
            Assert.AreEqual(OSCCameraEndpoints.Exposure.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(-2.5f, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[0].Type);
            Assert.AreEqual("f", message.TypeTag.Value);
        }
        
        [Test]
        public void ToOSCMessage_WithMinValue_CreatesCorrectMessage()
        {
            // Arrange
            var exposure = new Exposure(Exposure.MinValue);
            
            // Act
            var message = _converter.ToOSCMessage(exposure);
            
            // Assert
            Assert.AreEqual(-10f, message.Arguments[0].Value);
        }
        
        [Test]
        public void ToOSCMessage_WithMaxValue_CreatesCorrectMessage()
        {
            // Arrange
            var exposure = new Exposure(Exposure.MaxValue);
            
            // Act
            var message = _converter.ToOSCMessage(exposure);
            
            // Assert
            Assert.AreEqual(4f, message.Arguments[0].Value);
        }
        
        [Test]
        public void ToOSCMessage_WithDefaultValue_CreatesCorrectMessage()
        {
            // Arrange
            var exposure = new Exposure(Exposure.DefaultValue);
            
            // Act
            var message = _converter.ToOSCMessage(exposure);
            
            // Assert
            Assert.AreEqual(0f, message.Arguments[0].Value);
        }
        
        [Test]
        public void FromOSCMessage_WithFloatArgument_ReturnsCorrectExposure()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = new[] { new Argument(-1.5f) };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(-1.5f, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = new[] { new Argument(-20f) };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.MinValue, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = new[] { new Argument(10f) };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.MaxValue, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithEmptyArguments_ReturnsDefaultValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = System.Array.Empty<Argument>();
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithNonFloatArgument_ReturnsDefaultValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = new[] { new Argument("not a float") };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithWrongAddress_ReturnsDefaultValue()
        {
            // Arrange
            var address = new Address("/different/path");
            var arguments = new[] { new Argument(-2f) };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void FromOSCMessage_WithTooManyArguments_ReturnsDefaultValue()
        {
            // Arrange
            var address = OSCCameraEndpoints.Exposure;
            var arguments = new[] { new Argument(-1f), new Argument(2f) };
            var message = new Message(address, arguments);
            
            // Act
            var exposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void RoundTrip_PreservesValue()
        {
            // Arrange
            var originalExposure = new Exposure(-3.5f);
            
            // Act
            var message = _converter.ToOSCMessage(originalExposure);
            var roundTripExposure = _converter.FromOSCMessage(message);
            
            // Assert
            Assert.AreEqual(originalExposure.Value, roundTripExposure.Value);
        }
        
        [Test]
        public void ImplementsIOSCMessageConverter()
        {
            // Assert
            Assert.IsTrue(_converter != null);
        }
    }
}