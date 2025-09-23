using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Network.Osc;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class PhotoRateUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesPhotoRateDefaultValue()
        {
            // Act
            var photoRate = new PhotoRate(PhotoRate.DefaultValue);
            
            // Assert
            Assert.AreEqual(PhotoRate.DefaultValue, photoRate.Value);
        }
        
        [Test]
        public void Constructor_WithValue_StoresValue()
        {
            // Arrange
            const float testValue = 1.5f;
            
            // Act
            var photoRate = new PhotoRate(testValue);
            
            // Assert
            Assert.AreEqual(testValue, photoRate.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            const float testValue = 0.05f;
            
            // Act
            var photoRate = new PhotoRate(testValue);
            
            // Assert
            Assert.AreEqual(PhotoRate.MinValue, photoRate.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            const float testValue = 3f;
            
            // Act
            var photoRate = new PhotoRate(testValue);
            
            // Assert
            Assert.AreEqual(PhotoRate.MaxValue, photoRate.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var photoRate1 = new PhotoRate(1.2f);
            var photoRate2 = new PhotoRate(1.2f);
            
            // Act & Assert
            Assert.IsTrue(photoRate1.Equals(photoRate2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var photoRate1 = new PhotoRate(1.2f);
            var photoRate2 = new PhotoRate(1.5f);
            
            // Act & Assert
            Assert.IsFalse(photoRate1.Equals(photoRate2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var photoRate1 = new PhotoRate(1.2f);
            var photoRate2 = new PhotoRate(1.2f);
            
            // Act & Assert
            Assert.IsTrue(photoRate1 == photoRate2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            var photoRate1 = new PhotoRate(1.2f);
            var photoRate2 = new PhotoRate(1.5f);
            
            // Act & Assert
            Assert.IsTrue(photoRate1 != photoRate2);
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var photoRate = new PhotoRate(1.8f);
            
            // Act
            float value = photoRate;
            
            // Assert
            Assert.AreEqual(1.8f, value);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var photoRate1 = new PhotoRate(1.3f);
            var photoRate2 = new PhotoRate(1.3f);
            
            // Act & Assert
            Assert.AreEqual(photoRate1.GetHashCode(), photoRate2.GetHashCode());
        }
        
        [Test]
        public void MinValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(0.1f, PhotoRate.MinValue);
        }
        
        [Test]
        public void MaxValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(2f, PhotoRate.MaxValue);
        }
        
        [Test]
        public void DefaultValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(1f, PhotoRate.DefaultValue);
        }
    }
}
