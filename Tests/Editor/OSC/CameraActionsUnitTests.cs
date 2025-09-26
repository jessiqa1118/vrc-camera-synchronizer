using System;
using Astearium.Network.Osc;
using UnityEngine;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class CameraActionsUnitTests
    {
        private class MockTransmitter : IOSCTransmitter
        {
            public IOSCMessage LastSentMessage { get; private set; }
            public bool IsDisposed { get; private set; }

            public void Send(IOSCMessage message)
            {
                if (IsDisposed) throw new ObjectDisposedException(nameof(MockTransmitter));
                LastSentMessage = message;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Test]
        public void Close_SendsCorrectActionMessage()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);

            // Act
            synchronizer.Close();

            // Assert
            var msg = transmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.Close.Value, msg.Address.Value);
            Assert.AreEqual(0, msg.Arguments.Length);
            Assert.AreEqual(string.Empty, msg.TypeTag.Value);

            // Cleanup
            synchronizer.Dispose();
            vrcCamera.Dispose();
        }

        [Test]
        public void Close_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.Close());

            // Cleanup
            vrcCamera.Dispose();
        }

        [Test]
        public void Capture_SendsCorrectActionMessage()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);

            // Act
            synchronizer.Capture();

            // Assert
            var msg = transmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.Capture.Value, msg.Address.Value);
            Assert.AreEqual(0, msg.Arguments.Length);
            Assert.AreEqual(string.Empty, msg.TypeTag.Value);

            // Cleanup
            synchronizer.Dispose();
            vrcCamera.Dispose();
        }

        [Test]
        public void Capture_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.Capture());

            // Cleanup
            vrcCamera.Dispose();
        }

        [Test]
        public void CaptureDelayed_SendsCorrectActionMessage()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);

            // Act
            synchronizer.CaptureDelayed();

            // Assert
            var msg = transmitter.LastSentMessage;
            Assert.AreEqual(OSCCameraEndpoints.CaptureDelayed.Value, msg.Address.Value);
            Assert.AreEqual(0, msg.Arguments.Length);
            Assert.AreEqual(string.Empty, msg.TypeTag.Value);

            // Cleanup
            synchronizer.Dispose();
            vrcCamera.Dispose();
        }

        [Test]
        public void CaptureDelayed_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var vrcCamera = new VRCCamera();
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.CaptureDelayed());

            // Cleanup
            vrcCamera.Dispose();
        }
    }
}
