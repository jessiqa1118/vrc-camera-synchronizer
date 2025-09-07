using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class ApertureUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 5.6f;
            
            // Act
            var aperture = new Aperture(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, aperture.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = 0.5f;
            
            // Act
            var aperture = new Aperture(inputValue);
            
            // Assert
            Assert.AreEqual(Aperture.MinValue, aperture.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 50f;
            
            // Act
            var aperture = new Aperture(inputValue);
            
            // Assert
            Assert.AreEqual(Aperture.MaxValue, aperture.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultParameter_UsesDefaultValue()
        {
            // Act
            var aperture = new Aperture(Aperture.DefaultValue);
            
            // Assert
            Assert.AreEqual(Aperture.DefaultValue, aperture.Value);
        }
        
        [Test]
        public void MinValue_Is_OnePointFour()
        {
            // Assert
            Assert.AreEqual(1.4f, Aperture.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_ThirtyTwo()
        {
            // Assert
            Assert.AreEqual(32f, Aperture.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_Fifteen()
        {
            // Assert
            Assert.AreEqual(15f, Aperture.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var aperture1 = new Aperture(8f);
            var aperture2 = new Aperture(8f);
            
            // Act & Assert
            Assert.AreEqual(aperture1, aperture2);
            Assert.IsTrue(aperture1.Equals(aperture2));
            Assert.IsTrue(aperture1 == aperture2);
            Assert.IsFalse(aperture1 != aperture2);
            Assert.AreEqual(aperture1.GetHashCode(), aperture2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var aperture1 = new Aperture(2.8f);
            var aperture2 = new Aperture(5.6f);
            
            // Act & Assert
            Assert.AreNotEqual(aperture1, aperture2);
            Assert.IsFalse(aperture1.Equals(aperture2));
            Assert.IsFalse(aperture1 == aperture2);
            Assert.IsTrue(aperture1 != aperture2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var aperture1 = new Aperture(8.0f);
            var aperture2 = new Aperture(8.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(aperture1, aperture2);
            Assert.IsTrue(aperture1 == aperture2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var aperture1 = new Aperture(8.0f);
            var aperture2 = new Aperture(8.01f);
            
            // Act & Assert
            Assert.AreNotEqual(aperture1, aperture2);
            Assert.IsTrue(aperture1 != aperture2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var aperture = new Aperture(11f);
            
            // Act
            float value = aperture;
            
            // Assert
            Assert.AreEqual(11f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var aperture = new Aperture(4f);
            object boxedAperture = new Aperture(4f);
            object differentObject = "not an aperture";
            
            // Act & Assert
            Assert.IsTrue(aperture.Equals(boxedAperture));
            Assert.IsFalse(aperture.Equals(differentObject));
            Assert.IsFalse(aperture.Equals(null));
        }
        
        [TestCase(1.4f)]
        [TestCase(2.8f)]
        [TestCase(5.6f)]
        [TestCase(15f)]
        [TestCase(32f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var aperture = new Aperture(value);
            
            // Assert
            Assert.AreEqual(value, aperture.Value);
        }
    }
}