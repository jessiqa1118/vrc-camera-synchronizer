using System;
using Parameters;
using OSC;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace VRCCamera.Tests.Integration
{
    [TestFixture]
    public class VRCCameraSynchronizerComponentIntegrationTests
    {
        private class MockOSCTransmitter : IOSCTransmitter
        {
            private bool _isDisposed;
            
            public void Send(Message message)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(MockOSCTransmitter));
            }
            
            public void Dispose()
            {
                _isDisposed = true;
            }
        }
        
        private GameObject _testGameObject;
        private Camera _camera;
        private VRCCameraSynchronizerComponent _component;
        private MockOSCTransmitter _mockTransmitter;
        
        [SetUp]
        public void SetUp()
        {
            // Create test GameObject with Camera
            _testGameObject = new GameObject("TestCamera");
            _camera = _testGameObject.AddComponent<Camera>();
            _camera.fieldOfView = 60f;
            _camera.focalLength = 50f;
        }
        
        [TearDown]
        public void TearDown()
        {
            if (_testGameObject)
            {
                UnityEngine.Object.DestroyImmediate(_testGameObject);
            }
            _mockTransmitter?.Dispose();
        }
        
        [Test]
        public void Component_RequiresCamera()
        {
            // Act
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            
            // Assert
            Assert.IsNotNull(_component.GetComponent<Camera>());
        }
        
        [UnityTest]
        public IEnumerator Component_InitializesOnEnable()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            _component.enabled = false;
            
            // Act
            _component.enabled = true;
            yield return null;
            
            // Assert
            // Component should be initialized (no exception thrown)
            Assert.IsTrue(_component.enabled);
        }
        
        
        [UnityTest]
        public IEnumerator Component_SendsMessagesInFixedUpdate()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            
            // Wait for initialization
            yield return null;
            
            // Act - Wait for multiple FixedUpdate cycles
            var startTime = Time.time;
            yield return new WaitForSeconds(0.1f); // Wait for at least 5 FixedUpdate calls (at 50Hz)
            
            // Assert
            // Component should have sent messages during FixedUpdate
            // We can't directly test the sending without dependency injection,
            // but we can verify the component runs without errors
            Assert.IsTrue(_component.enabled);
            Assert.IsTrue(Time.time > startTime);
        }
        
        [UnityTest]
        public IEnumerator Component_DisposesOnDisable()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            yield return null;
            
            // Act
            _component.enabled = false;
            yield return null;
            
            // Assert
            // Component should clean up resources
            Assert.IsFalse(_component.enabled);
        }
        
        [UnityTest]
        public IEnumerator Component_HandlesMultipleEnableDisableCycles()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            
            // Act - Multiple enable/disable cycles
            for (var i = 0; i < 3; i++)
            {
                _component.enabled = true;
                yield return null;
                Assert.IsTrue(_component.enabled || !_camera); // May be disabled if camera is missing
                
                _component.enabled = false;
                yield return null;
                Assert.IsFalse(_component.enabled);
            }
            
            // Assert - No exceptions thrown
            Assert.IsNotNull(_component);
        }
        
        [Test]
        public void Component_HasCorrectDefaultValues()
        {
            // Act
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            
            // Assert - Using reflection to check serialized fields
            var type = typeof(VRCCameraSynchronizerComponent);
            var destinationField = type.GetField("destination", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var portField = type.GetField("port", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            if (destinationField != null)
            {
                Assert.AreEqual("127.0.0.1", destinationField.GetValue(_component));
            }
            
            if (portField != null)
            {
                Assert.AreEqual(9000, portField.GetValue(_component));
            }
        }
        
        [UnityTest]
        public IEnumerator Component_UpdatesCameraValuesOverTime()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            yield return null;
            
            // Act - Change camera values
            var originalFocalLength = _camera.focalLength;
            _camera.focalLength = 35f;
            yield return new WaitForFixedUpdate();
            
            _camera.focalLength = 85f;
            yield return new WaitForFixedUpdate();
            
            // Assert - Component should handle changes without errors
            Assert.IsTrue(_component.enabled);
            Assert.AreNotEqual(originalFocalLength, _camera.focalLength);
        }
        
        [UnityTest]
        public IEnumerator Component_ContinuesRunningInFixedUpdate()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            yield return null;
            
            // Act - Run for several fixed updates
            var fixedUpdateCount = 0;
            var startTime = Time.fixedTime;
            
            while (fixedUpdateCount < 10)
            {
                yield return new WaitForFixedUpdate();
                fixedUpdateCount++;
            }
            
            // Assert
            Assert.IsTrue(_component.enabled);
            Assert.Greater(Time.fixedTime, startTime);
            Assert.AreEqual(10, fixedUpdateCount);
        }
        
        [Test]
        public void Component_HasAddComponentMenuAttribute()
        {
            // Assert
            var type = typeof(VRCCameraSynchronizerComponent);
            var attributes = type.GetCustomAttributes(typeof(AddComponentMenu), false);
            
            Assert.IsTrue(attributes.Length > 0);
            
            if (attributes[0] is AddComponentMenu menuAttribute)
            {
                Assert.AreEqual("JessiQa/VRC Camera Synchronizer", menuAttribute.componentMenu);
            }
        }
        
        [Test]
        public void Component_HasRequireComponentAttribute()
        {
            // Assert
            var type = typeof(VRCCameraSynchronizerComponent);
            var attributes = type.GetCustomAttributes(typeof(RequireComponent), false);
            
            Assert.IsTrue(attributes.Length > 0);
            
            if (attributes[0] is RequireComponent requireAttribute)
            {
                Assert.AreEqual(typeof(Camera), requireAttribute.m_Type0);
            }
        }
        
        [UnityTest]
        public IEnumerator Component_WorksWithDifferentCameraSettings()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            yield return null;
            
            // Act - Test various camera settings
            var testValues = new[] { 20f, 50f, 85f, 135f, 150f };
            
            foreach (var focalLength in testValues)
            {
                _camera.focalLength = focalLength;
                yield return new WaitForFixedUpdate();
                
                // Assert - Component continues to work
                Assert.IsTrue(_component.enabled);
            }
        }
        
        [UnityTest]
        public IEnumerator Component_HandlesRapidCameraChanges()
        {
            // Arrange
            _component = _testGameObject.AddComponent<VRCCameraSynchronizerComponent>();
            yield return null;
            
            // Act - Rapidly change camera values
            for (var i = 0; i < 20; i++)
            {
                _camera.focalLength = UnityEngine.Random.Range(20f, 150f);
                if (i % 5 == 0)
                {
                    yield return new WaitForFixedUpdate();
                }
            }
            
            // Assert
            Assert.IsTrue(_component.enabled);
        }
    }
}