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
            public int SendCallCount { get; private set; }
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
        private VRCCameraSynchronizer _synchronizer;
        
        [SetUp]
        public void SetUp()
        {
            _mockTransmitter = new MockTransmitter();
            
            // Create a test camera GameObject
            var gameObject = new GameObject("TestCamera");
            _camera = gameObject.AddComponent<Camera>();
            _camera.fieldOfView = 60f;
            
            _synchronizer = new VRCCameraSynchronizer(_mockTransmitter, _camera);
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
                _ = new VRCCameraSynchronizer(null, _camera));
        }
        
        [Test]
        public void Constructor_WithNullCamera_ThrowsArgumentNullException()
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
        public void Sync_SendsZoomMessage()
        {
            // Arrange
            _camera.focalLength = 50f;
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            Assert.AreEqual(1, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            Assert.AreEqual(OSCCameraEndpoints.Zoom.Value, _mockTransmitter.LastSentMessage.Value.Address.Value);
        }
        
        [Test]
        public void Sync_SendsCorrectZoomValue()
        {
            // Arrange
            _camera.focalLength = 35f;
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var message = _mockTransmitter.LastSentMessage.Value;
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Float32, message.Arguments[0].Type);
            
            // The actual value should be a Zoom created from focal length
            var sentValue = (float)message.Arguments[0].Value;
            Assert.IsTrue(sentValue is >= Zoom.MinValue and <= Zoom.MaxValue);
        }
        
        [Test]
        public void Sync_WithDifferentFocalLengths_SendsDifferentValues()
        {
            // Arrange & Act
            _camera.focalLength = 20f;
            _synchronizer.Sync();
            var firstMessage = _mockTransmitter.LastSentMessage;
            
            _camera.focalLength = 80f;
            _synchronizer.Sync();
            var secondMessage = _mockTransmitter.LastSentMessage;
            
            // Assert
            Assert.AreEqual(2, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(firstMessage);
            Assert.IsNotNull(secondMessage);
            Assert.AreNotEqual(firstMessage.Value.Arguments[0].Value, secondMessage.Value.Arguments[0].Value);
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