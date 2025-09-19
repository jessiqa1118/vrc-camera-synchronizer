using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Osc;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class SmoothingStrengthUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesSmoothingStrengthDefaultValue()
        {
            // Act
            var smoothingStrength = new SmoothingStrength(SmoothingStrength.DefaultValue);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.DefaultValue, smoothingStrength.Value);
        }
        
        [Test]
        public void Constructor_WithValue_StoresValue()
        {
            // Arrange
            const float testValue = 7.5f;
            
            // Act
            var smoothingStrength = new SmoothingStrength(testValue);
            
            // Assert
            Assert.AreEqual(testValue, smoothingStrength.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            const float testValue = 0.05f;
            
            // Act
            var smoothingStrength = new SmoothingStrength(testValue);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.MinValue, smoothingStrength.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            const float testValue = 15f;
            
            // Act
            var smoothingStrength = new SmoothingStrength(testValue);
            
            // Assert
            Assert.AreEqual(SmoothingStrength.MaxValue, smoothingStrength.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var smoothingStrength1 = new SmoothingStrength(6f);
            var smoothingStrength2 = new SmoothingStrength(6f);
            
            // Act & Assert
            Assert.IsTrue(smoothingStrength1.Equals(smoothingStrength2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var smoothingStrength1 = new SmoothingStrength(6f);
            var smoothingStrength2 = new SmoothingStrength(8f);
            
            // Act & Assert
            Assert.IsFalse(smoothingStrength1.Equals(smoothingStrength2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var smoothingStrength1 = new SmoothingStrength(6f);
            var smoothingStrength2 = new SmoothingStrength(6f);
            
            // Act & Assert
            Assert.IsTrue(smoothingStrength1 == smoothingStrength2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            var smoothingStrength1 = new SmoothingStrength(6f);
            var smoothingStrength2 = new SmoothingStrength(8f);
            
            // Act & Assert
            Assert.IsTrue(smoothingStrength1 != smoothingStrength2);
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var smoothingStrength = new SmoothingStrength(4.5f);
            
            // Act
            float value = smoothingStrength;
            
            // Assert
            Assert.AreEqual(4.5f, value);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var smoothingStrength1 = new SmoothingStrength(3f);
            var smoothingStrength2 = new SmoothingStrength(3f);
            
            // Act & Assert
            Assert.AreEqual(smoothingStrength1.GetHashCode(), smoothingStrength2.GetHashCode());
        }
        
        [Test]
        public void MinValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(0.1f, SmoothingStrength.MinValue);
        }
        
        [Test]
        public void MaxValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(10f, SmoothingStrength.MaxValue);
        }
        
        [Test]
        public void DefaultValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(5f, SmoothingStrength.DefaultValue);
        }
    }
}