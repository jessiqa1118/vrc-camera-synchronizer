using System;
using Astearium.Network.Osc;
using NUnit.Framework;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class VRCCameraSynchronizerUnitTests
    {
        private class MockTransmitter : IOSCTransmitter
        {
            public IOSCMessage LastSentMessage { get; private set; }
            public int SendCallCount { get; private set; }
            public bool IsDisposed { get; private set; }
            
            public void Send(IOSCMessage message)
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
        private VRCCamera _vrcCamera;
        private VRCCameraSynchronizer _synchronizer;
        
        [SetUp]
        public void SetUp()
        {
            _mockTransmitter = new MockTransmitter();
            _vrcCamera = new VRCCamera();
            _synchronizer = new VRCCameraSynchronizer(_mockTransmitter, _vrcCamera);
        }
        
        [TearDown]
        public void TearDown()
        {
            _synchronizer?.Dispose();
            _vrcCamera?.Dispose();
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
            var vrcCamera = new VRCCamera();
            
            // Act
            var synchronizer = new VRCCameraSynchronizer(mockTransmitter, vrcCamera);
            
            // Assert - Constructor should send all 34 initial values (14 sliders + 18 toggles + 1 mode + 1 pose)
            Assert.AreEqual(34, mockTransmitter.SendCallCount);
            
            // Cleanup
            synchronizer.Dispose();
            vrcCamera.Dispose();
        }
        
        [Test]
        public void Sync_ForceSendsAllMessages()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            _vrcCamera.SetZoom(new Zoom(50f, true));
            _mockTransmitter.Reset();
            _vrcCamera.SetExposure(new Exposure(-2.5f));
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            // Sync now force sends all 34 parameters (14 sliders + 18 toggles + 1 mode + 1 pose) + 1 from SetExposure = 35
            Assert.AreEqual(35, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
        }
        
        [Test]
        public void Sync_ForceSendsAllValues()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            _vrcCamera.SetZoom(new Zoom(35f, true));
            _mockTransmitter.Reset();
            
            // Act
            _synchronizer.Sync();
            
            // Assert
            // Force sends all 34 messages (14 sliders + 18 toggles + 1 mode + 1 pose)
            Assert.AreEqual(34, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            
            // Last message is OrientationIsLandscape which has Bool type
            var message = _mockTransmitter.LastSentMessage;
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
        }
        
        [Test]
        public void Sync_WithDifferentFocalLengths_SendsDifferentValues()
        {
            // Arrange
            _mockTransmitter.Reset(); // Reset to clear initial sync messages
            
            // Act
            _vrcCamera.SetZoom(new Zoom(20f, true));
            _mockTransmitter.Reset();
            _synchronizer.Sync();
            _mockTransmitter.Reset(); // Reset mock state
            
            _vrcCamera.SetZoom(new Zoom(80f, true));
            _mockTransmitter.Reset();
            _synchronizer.Sync();
            var secondCallCount = _mockTransmitter.SendCallCount;
            
            // Assert
            Assert.AreEqual(34, secondCallCount); // 34 messages per Sync call (14 sliders + 18 toggles + 1 mode + 1 pose)
        }
        
        [Test]
        public void Sync_LastMessageIsOrientationBool()
        {
            // Act
            _synchronizer.Sync();

            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var message = _mockTransmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.OrientationIsLandscape.Value, message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            // Default Orientation is Landscape => true
            Assert.IsTrue((bool)message.Arguments[0].Value);
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
            Assert.IsInstanceOf<IDisposable>(_synchronizer);
        }

        [Test]
        public void ReactiveProperty_OnPoseChange_SendsMessage()
        {
            // Arrange
            _mockTransmitter.Reset();

            // Act
            _vrcCamera.SetPose(new Pose(new Vector3(1f, 2f, 3f), Quaternion.Euler(10f, 20f, 30f)));

            // Assert
            Assert.AreEqual(1, _mockTransmitter.SendCallCount);
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var msg = _mockTransmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.Pose.Value, msg.Address.Value);
            Assert.AreEqual(6, msg.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Float32, msg.Arguments[0].Type);
            Assert.AreEqual(Argument.ValueType.Float32, msg.Arguments[5].Type);
        }

        [Test]
        public void Orientation_ChangeToPortrait_SendsFalse()
        {
            // Arrange
            _mockTransmitter.Reset();

            // Act
            _vrcCamera.SetOrientation(Orientation.Portrait);

            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var msg = _mockTransmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.OrientationIsLandscape.Value, msg.Address.Value);
            Assert.AreEqual(1, msg.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Bool, msg.Arguments[0].Type);
            Assert.IsFalse((bool)msg.Arguments[0].Value);
        }

        [Test]
        public void Orientation_ChangeToLandscape_SendsTrue()
        {
            // Arrange
            _mockTransmitter.Reset();
            _vrcCamera.SetOrientation(Orientation.Portrait); // ensure change from default
            _mockTransmitter.Reset();

            // Act
            _vrcCamera.SetOrientation(Orientation.Landscape);

            // Assert
            Assert.IsNotNull(_mockTransmitter.LastSentMessage);
            var msg = _mockTransmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.OrientationIsLandscape.Value, msg.Address.Value);
            Assert.AreEqual(1, msg.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Bool, msg.Arguments[0].Type);
            Assert.IsTrue((bool)msg.Arguments[0].Value);
        }

        [Test]
        public void ReactiveProperty_OnZoomChange_SendsMessage()
        {
            // Arrange
            _mockTransmitter.Reset();
            
            // Act - Change camera focal length and update
            _vrcCamera.SetZoom(new Zoom(85f, true));
            
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
