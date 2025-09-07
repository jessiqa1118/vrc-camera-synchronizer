using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class SaturationUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 50f;
            
            // Act
            var saturation = new Saturation(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, saturation.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -10f;
            
            // Act
            var saturation = new Saturation(inputValue);
            
            // Assert
            Assert.AreEqual(Saturation.MinValue, saturation.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 150f;
            
            // Act
            var saturation = new Saturation(inputValue);
            
            // Assert
            Assert.AreEqual(Saturation.MaxValue, saturation.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultParameter_UsesDefaultValue()
        {
            // Act
            var saturation = new Saturation(Saturation.DefaultValue);
            
            // Assert
            Assert.AreEqual(Saturation.DefaultValue, saturation.Value);
        }
        
        [Test]
        public void MinValue_Is_Zero()
        {
            // Assert
            Assert.AreEqual(0f, Saturation.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_OneHundred()
        {
            // Assert
            Assert.AreEqual(100f, Saturation.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_OneHundred()
        {
            // Assert
            Assert.AreEqual(100f, Saturation.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var saturation1 = new Saturation(75f);
            var saturation2 = new Saturation(75f);
            
            // Act & Assert
            Assert.AreEqual(saturation1, saturation2);
            Assert.IsTrue(saturation1.Equals(saturation2));
            Assert.IsTrue(saturation1 == saturation2);
            Assert.IsFalse(saturation1 != saturation2);
            Assert.AreEqual(saturation1.GetHashCode(), saturation2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var saturation1 = new Saturation(25f);
            var saturation2 = new Saturation(75f);
            
            // Act & Assert
            Assert.AreNotEqual(saturation1, saturation2);
            Assert.IsFalse(saturation1.Equals(saturation2));
            Assert.IsFalse(saturation1 == saturation2);
            Assert.IsTrue(saturation1 != saturation2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var saturation1 = new Saturation(50.0f);
            var saturation2 = new Saturation(50.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(saturation1, saturation2);
            Assert.IsTrue(saturation1 == saturation2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var saturation1 = new Saturation(50.0f);
            var saturation2 = new Saturation(50.01f);
            
            // Act & Assert
            Assert.AreNotEqual(saturation1, saturation2);
            Assert.IsTrue(saturation1 != saturation2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var saturation = new Saturation(80f);
            
            // Act
            float value = saturation;
            
            // Assert
            Assert.AreEqual(80f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var saturation = new Saturation(60f);
            object boxedSaturation = new Saturation(60f);
            object differentObject = "not a saturation";
            
            // Act & Assert
            Assert.IsTrue(saturation.Equals(boxedSaturation));
            Assert.IsFalse(saturation.Equals(differentObject));
            Assert.IsFalse(saturation.Equals(null));
        }
        
        [TestCase(0f)]
        [TestCase(25f)]
        [TestCase(50f)]
        [TestCase(75f)]
        [TestCase(100f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var saturation = new Saturation(value);
            
            // Assert
            Assert.AreEqual(value, saturation.Value);
        }
    }
}