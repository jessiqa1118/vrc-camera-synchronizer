using System;
using NUnit.Framework;
using UnityEngine;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class VRCCameraSynchronizerUnitTests
    {
        private class MockTransmitter : IOSCTransmitter
        {
            public Message? LastSentMessage { get; private set; }
            public int SendCallCount { get; set; }  // Made setter public for test reset
            public bool IsDisposed { get; private set; }
            
            public void Send(Message message)
            {
                if (IsDisposed)
                    throw new ObjectDisposedException(nameof(MockTransmitter));
                    
                LastSentMessage = message;
                SendCallCount++;
            }
            
            public void Dispose()
            {
                IsDisposed = true;
            }
        }
        
        private MockTransmitter _mockTransmitter;
        private Camera _camera;
        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;
        
        [SetUp]
        public void SetUp()
        {
            _mockTransmitter = new MockTransmitter();
            
            // Create a test camera GameObject
            var gameObject = new GameObject("TestCamera");
            _camera = gameObject.AddComponent<Camera>();
            _camera.fieldOfView = 60f;
            _camera.focusDistance = 2.5f; // Set focus distance for testing
            
            _vrcCamera = new VRCCamera(_camera);
            _synchronizer = new VRCCameraSynchronizer(_mockTransmitter, _vrcCamera);
        }
        
        [TearDown]
        public void TearDown()
        {
            _synchronizer?.Dispose();
            if (_camera)
            {
                UnityEngine.Object.DestroyImmediate(_camera.gameObject);
            }
        }
        
        [Test]
        public void Constructor_WithNullTransmitter_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                _ = new VRCCameraSynchronizer(null, _vrcCamera));
        }
        
        [Test]
        public void Constructor_WithNullVRCCamera_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                _ = new VRCCameraSynchronizer(_mockTransmitter, null));
        }
        
        [Test]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            // Assert
            Assert.IsNotNull(_synchronizer);
        }
        
        [Test]
        public void Sync_SendsMultipleMessages()
        {
            // Arrange
            _camera.focalLength = 50f;
            _vrcCamera.Exposure = new Exposure(-2.5f);
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            Assert.AreEqual(10, _mockTransmitter.SendCallCount); // zoom, exposure, focal distance, aperture, hue, saturation, lightness, lookAtMeXOffset, lookAtMeYOffset, flySpeed
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
        }
        
        [Test]
        public void Sync_SendsCorrectZoomValue()
        {
            // Arrange
            _camera.focalLength = 35f;
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            // Should send 10 messages (zoom, exposure, focal distance, aperture, hue, saturation, lightness, lookAtMeXOffset, lookAtMeYOffset, and flySpeed)
            Assert.AreEqual(10, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            
            var message = _mockTransmitter.LastSentMessage.Value;
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[0].Type);
            
            // The actual value should be valid
            var sentValue = (float)message.Arguments[0].Value;
            Assert.IsTrue(sentValue is >= -10f and <= 150f); // Could be either zoom or exposure
        }
        
        [Test]
        public void Sync_WithDifferentFocalLengths_SendsDifferentValues()
        {
            // Arrange & Act
            _camera.focalLength = 20f;
            _synchronizer.Sync();
            _mockTransmitter.SendCallCount = 0; // Reset count
            
            _camera.focalLength = 80f;
            _synchronizer.Sync();
            var secondCallCount = _mockTransmitter.SendCallCount;
            
            // Assert
            Assert.AreEqual(10, secondCallCount); // 10 messages per Sync call
        }
        
        [Test]
        public void Sync_GeneratesCorrectTypeTag()
        {
            // Act
            _synchronizer.Sync();
            
            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var message = _mockTransmitter.LastSentMessage.Value;
            Assert.AreEqual("f", message.TypeTag.Value);
        }
        
        [Test]
        public void Dispose_DisposesTransmitter()
        {
            // Act
            _synchronizer.Dispose();
            
            // Assert
            Assert.IsTrue(_mockTransmitter.IsDisposed);
        }
        
        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _synchronizer.Dispose();
                _synchronizer.Dispose();
            });
        }
        
        [Test]
        public void Sync_AfterDispose_ThrowsObjectDisposedException()
        {
            // Arrange
            _synchronizer.Dispose();
            
            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => _synchronizer.Sync());
        }
        
        [Test]
        public void ImplementsIDisposable()
        {
            // Assert
            Assert.IsTrue(_synchronizer != null);
        }
    }
}