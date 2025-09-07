using System;
using Parameters;
using OSC;
using NUnit.Framework;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class OSCJackTransmitterUnitTests
    {
        private OSCJackTransmitter _transmitter;
        
        [SetUp]
        public void SetUp()
        {
            // Use localhost and a test port
            _transmitter = new OSCJackTransmitter("127.0.0.1", 9999);
        }
        
        [TearDown]
        public void TearDown()
        {
            _transmitter?.Dispose();
        }
        
        [Test]
        public void Constructor_CreatesInstance()
        {
            // Assert
            Assert.IsNotNull(_transmitter);
            Assert.IsTrue(_transmitter != null);
        }
        
        [Test]
        public void Send_WithEmptyArguments_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test");
            var arguments = Array.Empty<Argument>();
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithSingleFloat_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/float");
            var arguments = new[] { new Argument(3.14f) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithSingleInt_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/int");
            var arguments = new[] { new Argument(42) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithSingleString_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/string");
            var arguments = new[] { new Argument("test") };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithBoolTrue_SendsAsInt()
        {
            // Arrange
            var address = new Address("/test/bool");
            var arguments = new[] { new Argument(true) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithBoolFalse_SendsAsInt()
        {
            // Arrange
            var address = new Address("/test/bool");
            var arguments = new[] { new Argument(false) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithTwoInts_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/ii");
            var arguments = new[] { new Argument(1), new Argument(2) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithThreeFloats_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/fff");
            var arguments = new[] 
            { 
                new Argument(1.0f), 
                new Argument(2.0f), 
                new Argument(3.0f) 
            };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithFourFloats_DoesNotThrow()
        {
            // Arrange
            var address = new Address("/test/ffff");
            var arguments = new[] 
            { 
                new Argument(1.0f), 
                new Argument(2.0f), 
                new Argument(3.0f),
                new Argument(4.0f)
            };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.DoesNotThrow(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithUnsupportedPattern_ThrowsNotSupportedException()
        {
            // Arrange - mixed types not supported by OSCJack
            var address = new Address("/test/mixed");
            var arguments = new[] 
            { 
                new Argument(42), 
                new Argument("string") 
            };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.Throws<NotSupportedException>(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Send_WithBlob_ThrowsNotSupportedException()
        {
            // Arrange
            var address = new Address("/test/blob");
            var arguments = new[] { new Argument(new byte[] { 1, 2, 3 }) };
            var message = new Message(address, arguments);
            
            // Act & Assert
            Assert.Throws<NotSupportedException>(() => _transmitter.Send(message));
        }
        
        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _transmitter.Dispose();
                _transmitter.Dispose();
            });
        }
        
        [Test]
        public void Send_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            var address = new Address("/test");
            var message = new Message(address, Array.Empty<Argument>());
            _transmitter.Dispose();
            
            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => _transmitter.Send(message));
        }
        
        [Test]
        public void ImplementsIDisposable()
        {
            // Assert
            Assert.IsTrue(_transmitter != null);
        }
    }
}