using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Osc;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class FlySpeedUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesFlySpeedDefaultValue()
        {
            // Act
            var flySpeed = new FlySpeed(FlySpeed.DefaultValue);
            
            // Assert
            Assert.AreEqual(FlySpeed.DefaultValue, flySpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValue_StoresValue()
        {
            // Arrange
            const float testValue = 5f;
            
            // Act
            var flySpeed = new FlySpeed(testValue);
            
            // Assert
            Assert.AreEqual(testValue, flySpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            const float testValue = 0.05f;
            
            // Act
            var flySpeed = new FlySpeed(testValue);
            
            // Assert
            Assert.AreEqual(FlySpeed.MinValue, flySpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            const float testValue = 20f;
            
            // Act
            var flySpeed = new FlySpeed(testValue);
            
            // Assert
            Assert.AreEqual(FlySpeed.MaxValue, flySpeed.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var flySpeed1 = new FlySpeed(7.5f);
            var flySpeed2 = new FlySpeed(7.5f);
            
            // Act & Assert
            Assert.IsTrue(flySpeed1.Equals(flySpeed2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var flySpeed1 = new FlySpeed(7.5f);
            var flySpeed2 = new FlySpeed(10f);
            
            // Act & Assert
            Assert.IsFalse(flySpeed1.Equals(flySpeed2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var flySpeed1 = new FlySpeed(7.5f);
            var flySpeed2 = new FlySpeed(7.5f);
            
            // Act & Assert
            Assert.IsTrue(flySpeed1 == flySpeed2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            var flySpeed1 = new FlySpeed(7.5f);
            var flySpeed2 = new FlySpeed(10f);
            
            // Act & Assert
            Assert.IsTrue(flySpeed1 != flySpeed2);
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var flySpeed = new FlySpeed(8f);
            
            // Act
            float value = flySpeed;
            
            // Assert
            Assert.AreEqual(8f, value);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var flySpeed1 = new FlySpeed(5f);
            var flySpeed2 = new FlySpeed(5f);
            
            // Act & Assert
            Assert.AreEqual(flySpeed1.GetHashCode(), flySpeed2.GetHashCode());
        }
        
        [Test]
        public void MinValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(0.1f, FlySpeed.MinValue);
        }
        
        [Test]
        public void MaxValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(15f, FlySpeed.MaxValue);
        }
        
        [Test]
        public void DefaultValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(3f, FlySpeed.DefaultValue);
        }
    }
}