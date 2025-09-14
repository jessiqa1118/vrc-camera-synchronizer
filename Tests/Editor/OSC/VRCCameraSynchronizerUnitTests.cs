using System;
using Parameters;
using OSC;
using NUnit.Framework;
using UnityEngine;

namespace VRCCamera.Tests.Unit
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
            
            public void Reset()
            {
                SendCallCount = 0;
                LastSentMessage = null;
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
            _vrcCamera?.Dispose();
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
        public void Constructor_SendsInitialValues()
        {
            // Arrange
            var mockTransmitter = new MockTransmitter();
            var camera = new GameObject("TestCamera").AddComponent<Camera>();
            var vrcCamera = new VRCCamera(camera);
            
            // Act
            var synchronizer = new VRCCameraSynchronizer(mockTransmitter, vrcCamera);
            
            // Assert - Constructor should send all 20 initial values (14 sliders + 6 toggles)
            Assert.AreEqual(20, mockTransmitter.SendCallCount);
            
            // Cleanup
            synchronizer.Dispose();
            vrcCamera.Dispose();
            UnityEngine.Object.DestroyImmediate(camera.gameObject);
        }
        
        [Test]
        public void Sync_ForceSendsAllMessages()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            _camera.focalLength = 50f;
            _vrcCamera.SetExposure(new Exposure(-2.5f));
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            // Sync now force sends all 20 parameters (14 sliders + 6 toggles) + 1 from SetExposure = 21
            Assert.AreEqual(21, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
        }
        
        [Test]
        public void Sync_ForceSendsAllValues()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            _camera.focalLength = 35f;
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            // Force sends all 16 messages (14 sliders + 2 toggles)
            Assert.AreEqual(16, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            
            // Last message is ShowUIInCamera toggle which has Bool type
            var message = _mockTransmitter.LastSentMessage.Value;
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
        }
        
        [Test]
        public void Sync_WithDifferentFocalLengths_SendsDifferentValues()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            
            // Act
            _camera.focalLength = 20f;
            _synchronizer.Sync();
            _mockTransmitter.Reset(); // Reset mock state
            
            _camera.focalLength = 80f;
            _synchronizer.Sync();
            var secondCallCount = _mockTransmitter.SendCallCount;
            
            // Assert
            Assert.AreEqual(20, secondCallCount); // 20 messages per Sync call (14 sliders + 6 toggles)
        }
        
        [Test]
        public void Sync_GeneratesCorrectTypeTag()
        {
            // Act
            _synchronizer.Sync();
            
            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var message = _mockTransmitter.LastSentMessage.Value;
            // Last message is a toggle with default false, which has TypeTag "F"
            Assert.AreEqual("F", message.TypeTag.Value);
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
        
        [Test]
        public void ReactiveProperty_OnZoomChange_SendsMessage()
        {
            // Arrange
            _mockTransmitter.Reset();
            
            // Act - Change camera focal length and update
            _camera.focalLength = 85f;
            _vrcCamera.UpdateFromCamera();
            
            // Assert - Should send only one message for the zoom change
            Assert.AreEqual(1, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
        }
        
        [Test]
        public void ReactiveProperty_OnExposureChange_SendsMessage()
        {
            // Arrange
            _mockTransmitter.Reset();
            
            // Act
            _vrcCamera.SetExposure(new Exposure(-5f));
            
            // Assert - Should send only one message for the exposure change
            Assert.AreEqual(1, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
        }
        
        [Test]
        public void ReactiveProperty_NoChange_DoesNotSendMessage()
        {
            // Arrange
            _mockTransmitter.Reset();
            var currentExposure = _vrcCamera.Exposure.Value;
            
            // Act - Set same value
            _vrcCamera.SetExposure(currentExposure);
            
            // Assert - Should not send any message
            Assert.AreEqual(0, _mockTransmitter.SendCallCount);
        }
    }
}
