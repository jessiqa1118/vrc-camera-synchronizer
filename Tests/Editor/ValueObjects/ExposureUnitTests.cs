using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class ExposureUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesExposureDefaultValue()
        {
            // Act
            var exposure = new Exposure(Exposure.DefaultValue);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = -2.5f;
            
            // Act
            var exposure = new Exposure(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, exposure.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -15f;
            
            // Act
            var exposure = new Exposure(inputValue);
            
            // Assert
            Assert.AreEqual(Exposure.MinValue, exposure.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 10f;
            
            // Act
            var exposure = new Exposure(inputValue);
            
            // Assert
            Assert.AreEqual(Exposure.MaxValue, exposure.Value);
        }
        
        [Test]
        public void Constructor_WithZero_StoresZero()
        {
            // Arrange & Act
            var exposure = new Exposure(0f);
            
            // Assert
            Assert.AreEqual(0f, exposure.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultValue_StoresDefault()
        {
            // Arrange & Act
            var exposure = new Exposure(Exposure.DefaultValue);
            
            // Assert
            Assert.AreEqual(Exposure.DefaultValue, exposure.Value);
        }
        
        [Test]
        public void MinValue_Is_NegativeTen()
        {
            // Assert
            Assert.AreEqual(-10f, Exposure.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_Four()
        {
            // Assert
            Assert.AreEqual(4f, Exposure.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_Zero()
        {
            // Assert
            Assert.AreEqual(0f, Exposure.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var exposure1 = new Exposure(-3.5f);
            var exposure2 = new Exposure(-3.5f);
            
            // Act & Assert
            Assert.AreEqual(exposure1, exposure2);
            Assert.IsTrue(exposure1.Equals(exposure2));
            Assert.IsTrue(exposure1 == exposure2);
            Assert.IsFalse(exposure1 != exposure2);
            Assert.AreEqual(exposure1.GetHashCode(), exposure2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var exposure1 = new Exposure(-2f);
            var exposure2 = new Exposure(2f);
            
            // Act & Assert
            Assert.AreNotEqual(exposure1, exposure2);
            Assert.IsFalse(exposure1.Equals(exposure2));
            Assert.IsFalse(exposure1 == exposure2);
            Assert.IsTrue(exposure1 != exposure2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var exposure1 = new Exposure(1.0f);
            var exposure2 = new Exposure(1.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(exposure1, exposure2);
            Assert.IsTrue(exposure1 == exposure2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var exposure1 = new Exposure(1.0f);
            var exposure2 = new Exposure(1.01f);
            
            // Act & Assert
            Assert.AreNotEqual(exposure1, exposure2);
            Assert.IsTrue(exposure1 != exposure2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var exposure = new Exposure(2.5f);
            
            // Act
            float value = exposure;
            
            // Assert
            Assert.AreEqual(2.5f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var exposure = new Exposure(1f);
            object boxedExposure = new Exposure(1f);
            object differentObject = "not an exposure";
            
            // Act & Assert
            Assert.IsTrue(exposure.Equals(boxedExposure));
            Assert.IsFalse(exposure.Equals(differentObject));
            Assert.IsFalse(exposure.Equals(null));
        }
        
        [TestCase(-10f)]
        [TestCase(-5f)]
        [TestCase(0f)]
        [TestCase(2f)]
        [TestCase(4f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var exposure = new Exposure(value);
            
            // Assert
            Assert.AreEqual(value, exposure.Value);
        }
    }
}