using System;
using UnityEngine;
using NUnit.Framework;
using Astearium.Osc;

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
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
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
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }

        [Test]
        public void Close_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.Close());

            // Cleanup
            vrcCamera.Dispose();
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }

        [Test]
        public void Capture_SendsCorrectActionMessage()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
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
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }

        [Test]
        public void Capture_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.Capture());

            // Cleanup
            vrcCamera.Dispose();
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }

        [Test]
        public void CaptureDelayed_SendsCorrectActionMessage()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
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
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }

        [Test]
        public void CaptureDelayed_AfterDispose_Throws()
        {
            // Arrange
            var transmitter = new MockTransmitter();
            var cameraGo = new GameObject("TestCamera");
            var camera = cameraGo.AddComponent<UnityEngine.Camera>();
            var vrcCamera = new VRCCamera(camera);
            var synchronizer = new VRCCameraSynchronizer(transmitter, vrcCamera);
            synchronizer.Dispose();

            // Act & Assert
            Assert.Throws<ObjectDisposedException>(() => synchronizer.CaptureDelayed());

            // Cleanup
            vrcCamera.Dispose();
            UnityEngine.Object.DestroyImmediate(cameraGo);
        }
    }
}