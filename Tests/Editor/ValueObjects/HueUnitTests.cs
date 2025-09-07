using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class HueUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 180f;
            
            // Act
            var hue = new Hue(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, hue.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -45f;
            
            // Act
            var hue = new Hue(inputValue);
            
            // Assert
            Assert.AreEqual(Hue.MinValue, hue.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 400f;
            
            // Act
            var hue = new Hue(inputValue);
            
            // Assert
            Assert.AreEqual(Hue.MaxValue, hue.Value);
        }
        
        [Test]
        public void Constructor_WithDefaultParameter_UsesDefaultValue()
        {
            // Act
            var hue = new Hue(Hue.DefaultValue);
            
            // Assert
            Assert.AreEqual(Hue.DefaultValue, hue.Value);
        }
        
        [Test]
        public void MinValue_Is_Zero()
        {
            // Assert
            Assert.AreEqual(0f, Hue.MinValue);
        }
        
        [Test]
        public void MaxValue_Is_ThreeSixty()
        {
            // Assert
            Assert.AreEqual(360f, Hue.MaxValue);
        }
        
        [Test]
        public void DefaultValue_Is_OneTwenty()
        {
            // Assert
            Assert.AreEqual(120f, Hue.DefaultValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var hue1 = new Hue(240f);
            var hue2 = new Hue(240f);
            
            // Act & Assert
            Assert.AreEqual(hue1, hue2);
            Assert.IsTrue(hue1.Equals(hue2));
            Assert.IsTrue(hue1 == hue2);
            Assert.IsFalse(hue1 != hue2);
            Assert.AreEqual(hue1.GetHashCode(), hue2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var hue1 = new Hue(90f);
            var hue2 = new Hue(270f);
            
            // Act & Assert
            Assert.AreNotEqual(hue1, hue2);
            Assert.IsFalse(hue1.Equals(hue2));
            Assert.IsFalse(hue1 == hue2);
            Assert.IsTrue(hue1 != hue2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var hue1 = new Hue(180.0f);
            var hue2 = new Hue(180.0f + UnityEngine.Mathf.Epsilon);
            
            // Act & Assert
            Assert.AreEqual(hue1, hue2);
            Assert.IsTrue(hue1 == hue2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var hue1 = new Hue(180.0f);
            var hue2 = new Hue(180.01f);
            
            // Act & Assert
            Assert.AreNotEqual(hue1, hue2);
            Assert.IsTrue(hue1 != hue2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var hue = new Hue(60f);
            
            // Act
            float value = hue;
            
            // Assert
            Assert.AreEqual(60f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var hue = new Hue(300f);
            object boxedHue = new Hue(300f);
            object differentObject = "not a hue";
            
            // Act & Assert
            Assert.IsTrue(hue.Equals(boxedHue));
            Assert.IsFalse(hue.Equals(differentObject));
            Assert.IsFalse(hue.Equals(null));
        }
        
        [TestCase(0f)]
        [TestCase(60f)]
        [TestCase(120f)]
        [TestCase(180f)]
        [TestCase(240f)]
        [TestCase(300f)]
        [TestCase(360f)]
        public void Constructor_WithValidRangeValues_StoresCorrectly(float value)
        {
            // Act
            var hue = new Hue(value);
            
            // Assert
            Assert.AreEqual(value, hue.Value);
        }
    }
}