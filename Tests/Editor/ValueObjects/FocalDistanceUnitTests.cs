using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class FocalDistanceUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 3.5f;
            
            // Act
            var focalDistance = new FocalDistance(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, focalDistance.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -1f;
            
            // Act
            var focalDistance = new FocalDistance(inputValue);
            
            // Assert
            Assert.AreEqual(FocalDistance.MinValue, focalDistance.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 15f;
            
            // Act
            var focalDistance = new FocalDistance(inputValue);
            
            // Assert
            Assert.AreEqual(FocalDistance.MaxValue, focalDistance.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultParameter_UsesDefaultValue()
        {
            // Act
            var focalDistance = new FocalDistance(FocalDistance.DefaultValue);
            
            // Assert
            Assert.AreEqual(FocalDistance.DefaultValue, focalDistance.Value);
        }
        
        [Test]
        public void MinValue_Is_Zero()
        {
            // Assert
            Assert.AreEqual(0f, FocalDistance.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_Ten()
        {
            // Assert
            Assert.AreEqual(10f, FocalDistance.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_OnePointFive()
        {
            // Assert
            Assert.AreEqual(1.5f, FocalDistance.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var fd1 = new FocalDistance(5f);
            var fd2 = new FocalDistance(5f);
            
            // Act & Assert
            Assert.AreEqual(fd1, fd2);
            Assert.IsTrue(fd1.Equals(fd2));
            Assert.IsTrue(fd1 == fd2);
            Assert.IsFalse(fd1 != fd2);
            Assert.AreEqual(fd1.GetHashCode(), fd2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var fd1 = new FocalDistance(2f);
            var fd2 = new FocalDistance(8f);
            
            // Act & Assert
            Assert.AreNotEqual(fd1, fd2);
            Assert.IsFalse(fd1.Equals(fd2));
            Assert.IsFalse(fd1 == fd2);
            Assert.IsTrue(fd1 != fd2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var fd1 = new FocalDistance(5.0f);
            var fd2 = new FocalDistance(5.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(fd1, fd2);
            Assert.IsTrue(fd1 == fd2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var fd1 = new FocalDistance(5.0f);
            var fd2 = new FocalDistance(5.01f);
            
            // Act & Assert
            Assert.AreNotEqual(fd1, fd2);
            Assert.IsTrue(fd1 != fd2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var focalDistance = new FocalDistance(7.5f);
            
            // Act
            float value = focalDistance;
            
            // Assert
            Assert.AreEqual(7.5f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var focalDistance = new FocalDistance(3f);
            object boxedFocalDistance = new FocalDistance(3f);
            object differentObject = "not a focal distance";
            
            // Act & Assert
            Assert.IsTrue(focalDistance.Equals(boxedFocalDistance));
            Assert.IsFalse(focalDistance.Equals(differentObject));
            Assert.IsFalse(focalDistance.Equals(null));
        }
        
        [TestCase(0f)]
        [TestCase(1.5f)]
        [TestCase(5f)]
        [TestCase(7.5f)]
        [TestCase(10f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var focalDistance = new FocalDistance(value);
            
            // Assert
            Assert.AreEqual(value, focalDistance.Value);
        }
    }
}