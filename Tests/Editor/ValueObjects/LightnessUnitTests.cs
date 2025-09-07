using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class LightnessUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesLightnessDefaultValue()
        {
            // Act
            var lightness = new Lightness(Lightness.DefaultValue);
            
            // Assert
            Assert.AreEqual(Lightness.DefaultValue, lightness.Value);
        }
        
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 30f;
            
            // Act
            var lightness = new Lightness(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, lightness.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -10f;
            
            // Act
            var lightness = new Lightness(inputValue);
            
            // Assert
            Assert.AreEqual(Lightness.MinValue, lightness.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 120f;
            
            // Act
            var lightness = new Lightness(inputValue);
            
            // Assert
            Assert.AreEqual(Lightness.MaxValue, lightness.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultParameter_UsesDefaultValue()
        {
            // Act
            var lightness = new Lightness(Lightness.DefaultValue);
            
            // Assert
            Assert.AreEqual(Lightness.DefaultValue, lightness.Value);
        }
        
        [Test]
        public void MinValue_Is_Zero()
        {
            // Assert
            Assert.AreEqual(0f, Lightness.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_Fifty()
        {
            // Assert
            Assert.AreEqual(50f, Lightness.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_Fifty()
        {
            // Assert
            Assert.AreEqual(50f, Lightness.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var lightness1 = new Lightness(40f);
            var lightness2 = new Lightness(40f);
            
            // Act & Assert
            Assert.AreEqual(lightness1, lightness2);
            Assert.IsTrue(lightness1.Equals(lightness2));
            Assert.IsTrue(lightness1 == lightness2);
            Assert.IsFalse(lightness1 != lightness2);
            Assert.AreEqual(lightness1.GetHashCode(), lightness2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var lightness1 = new Lightness(20f);
            var lightness2 = new Lightness(80f);
            
            // Act & Assert
            Assert.AreNotEqual(lightness1, lightness2);
            Assert.IsFalse(lightness1.Equals(lightness2));
            Assert.IsFalse(lightness1 == lightness2);
            Assert.IsTrue(lightness1 != lightness2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var lightness1 = new Lightness(30.0f);
            var lightness2 = new Lightness(30.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(lightness1, lightness2);
            Assert.IsTrue(lightness1 == lightness2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var lightness1 = new Lightness(30.0f);
            var lightness2 = new Lightness(30.01f);
            
            // Act & Assert
            Assert.AreNotEqual(lightness1, lightness2);
            Assert.IsTrue(lightness1 != lightness2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var lightness = new Lightness(35f);
            
            // Act
            float value = lightness;
            
            // Assert
            Assert.AreEqual(35f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var lightness = new Lightness(45f);
            object boxedLightness = new Lightness(45f);
            object differentObject = "not a lightness";
            
            // Act & Assert
            Assert.IsTrue(lightness.Equals(boxedLightness));
            Assert.IsFalse(lightness.Equals(differentObject));
            Assert.IsFalse(lightness.Equals(null));
        }
        
        [TestCase(0f)]
        [TestCase(10f)]
        [TestCase(20f)]
        [TestCase(30f)]
        [TestCase(40f)]
        [TestCase(50f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var lightness = new Lightness(value);
            
            // Assert
            Assert.AreEqual(value, lightness.Value);
        }
    }
}