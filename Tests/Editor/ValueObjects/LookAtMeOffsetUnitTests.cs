using NUnit.Framework;
using Parameters;
using OSC;
using UnityEngine;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class LookAtMeOffsetUnitTests
    {
        [Test]
        public void Constructor_WithFloatXAndY_StoresValues()
        {
            // Arrange
            const float testX = 10f;
            const float testY = -15f;
            
            // Act
            var offset = new LookAtMeOffset(testX, testY);
            
            // Assert
            Assert.AreEqual(testX, offset.X.Value);
            Assert.AreEqual(testY, offset.Y.Value);
        }
        
        [Test]
        public void Constructor_WithLookAtMeOffsets_StoresValues()
        {
            // Arrange
            var testX = new LookAtMeXOffset(10f);
            var testY = new LookAtMeYOffset(-15f);
            
            // Act
            var offset = new LookAtMeOffset(testX, testY);
            
            // Assert
            Assert.AreEqual(testX, offset.X);
            Assert.AreEqual(testY, offset.Y);
        }
        
        [Test]
        public void Constructor_WithVector2_StoresValues()
        {
            // Arrange
            var testVector = new Vector2(5f, -10f);
            
            // Act
            var offset = new LookAtMeOffset(testVector);
            
            // Assert
            Assert.AreEqual(testVector.x, offset.X.Value);
            Assert.AreEqual(testVector.y, offset.Y.Value);
        }
        
        [Test]
        public void ToVector2_ReturnsCorrectVector()
        {
            // Arrange
            var offset = new LookAtMeOffset(3f, -7f);
            
            // Act
            var vector = offset.ToVector2();
            
            // Assert
            Assert.AreEqual(3f, vector.x);
            Assert.AreEqual(-7f, vector.y);
        }
        
        [Test]
        public void Equals_WithSameValues_ReturnsTrue()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(5f, 10f);
            var offset2 = new LookAtMeOffset(5f, 10f);
            
            // Act & Assert
            Assert.IsTrue(offset1.Equals(offset2));
        }
        
        [Test]
        public void Equals_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(5f, 10f);
            var offset2 = new LookAtMeOffset(5f, 15f);
            
            // Act & Assert
            Assert.IsFalse(offset1.Equals(offset2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValues_ReturnsTrue()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(5f, 10f);
            var offset2 = new LookAtMeOffset(5f, 10f);
            
            // Act & Assert
            Assert.IsTrue(offset1 == offset2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValues_ReturnsTrue()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(5f, 10f);
            var offset2 = new LookAtMeOffset(5f, 15f);
            
            // Act & Assert
            Assert.IsTrue(offset1 != offset2);
        }
        
        [Test]
        public void ImplicitOperator_ToVector2_ReturnsCorrectVector()
        {
            // Arrange
            var offset = new LookAtMeOffset(12f, -8f);
            
            // Act
            Vector2 vector = offset;
            
            // Assert
            Assert.AreEqual(12f, vector.x);
            Assert.AreEqual(-8f, vector.y);
        }
        
        [Test]
        public void GetHashCode_WithSameValues_ReturnsSameHash()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(2f, 3f);
            var offset2 = new LookAtMeOffset(2f, 3f);
            
            // Act & Assert
            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }
        
        [Test]
        public void GetHashCode_WithDifferentValues_UsuallyReturnsDifferentHash()
        {
            // Arrange
            var offset1 = new LookAtMeOffset(2f, 3f);
            var offset2 = new LookAtMeOffset(3f, 2f);
            
            // Act & Assert
            // Hash codes might collide but should usually be different
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }
        
        [Test]
        public void Constructor_WithFloatValues_ClampsToValidRange()
        {
            // Arrange & Act
            var offsetAboveMax = new LookAtMeOffset(30f, 30f);
            var offsetBelowMin = new LookAtMeOffset(-30f, -30f);
            
            // Assert
            Assert.AreEqual(LookAtMeXOffset.MaxValue, offsetAboveMax.X.Value);
            Assert.AreEqual(LookAtMeYOffset.MaxValue, offsetAboveMax.Y.Value);
            Assert.AreEqual(LookAtMeXOffset.MinValue, offsetBelowMin.X.Value);
            Assert.AreEqual(LookAtMeYOffset.MinValue, offsetBelowMin.Y.Value);
        }
    }
}