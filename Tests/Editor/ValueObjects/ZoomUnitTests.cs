using NUnit.Framework;
using Parameters;
using OSC;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class ZoomUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesZoomDefaultValue()
        {
            // Act
            var zoom = new Zoom(Zoom.DefaultValue, true);
            
            // Assert
            Assert.AreEqual(Zoom.DefaultValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 45f;
            
            // Act
            var zoom = new Zoom(expectedValue, true);
            
            // Assert
            Assert.AreEqual(expectedValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = 10f;
            
            // Act
            var zoom = new Zoom(inputValue, true);
            
            // Assert
            Assert.AreEqual(Zoom.MinValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 200f;
            
            // Act
            var zoom = new Zoom(inputValue, true);
            
            // Assert
            Assert.AreEqual(Zoom.MaxValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithClampFalse_AndInvalidValue_ThrowsException()
        {
            // Arrange
            const float invalidValue = 10f;
            
            // Act & Assert
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _ = new Zoom(invalidValue, clamp: false));
        }
        
        [Test]
        public void Constructor_WithClampFalse_AndValidValue_StoresValue()
        {
            // Arrange
            const float validValue = 50f;
            
            // Act
            var zoom = new Zoom(validValue, clamp: false);
            
            // Assert
            Assert.AreEqual(validValue, zoom.Value);
        }
        
        [TestCase(20f)]
        [TestCase(85f)]
        [TestCase(150f)]
        public void Constructor_WithBoundaryValues_StoresCorrectly(float value)
        {
            // Act
            var zoom = new Zoom(value, true);
            
            // Assert
            Assert.AreEqual(value, zoom.Value);
        }
        
        [Test]
        public void DefaultValue_Is_FortyFive()
        {
            // Assert
            Assert.AreEqual(45f, Zoom.DefaultValue);
        }
        
        [Test]
        public void MinMaxValues_AreCorrect()
        {
            // Assert
            Assert.AreEqual(20f, Zoom.MinValue);
            Assert.AreEqual(150f, Zoom.MaxValue);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45f, true);
            var zoom2 = new Zoom(45f, true);
            
            // Act & Assert
            Assert.AreEqual(zoom1, zoom2);
            Assert.IsTrue(zoom1.Equals(zoom2));
            Assert.IsTrue(zoom1 == zoom2);
            Assert.IsFalse(zoom1 != zoom2);
            Assert.AreEqual(zoom1.GetHashCode(), zoom2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45f, true);
            var zoom2 = new Zoom(90f, true);
            
            // Act & Assert
            Assert.AreNotEqual(zoom1, zoom2);
            Assert.IsFalse(zoom1.Equals(zoom2));
            Assert.IsFalse(zoom1 == zoom2);
            Assert.IsTrue(zoom1 != zoom2);
        }
        
        [Test]
        public void Equality_WithSmallDifference_AreEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45.0f, true);
            var zoom2 = new Zoom(45.0f + UnityEngine.Mathf.Epsilon, true);
            
            // Act & Assert
            Assert.AreEqual(zoom1, zoom2);
            Assert.IsTrue(zoom1 == zoom2);
        }
        
        [Test]
        public void Equality_WithLargerDifference_AreNotEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45.0f, true);
            var zoom2 = new Zoom(45.01f, true);
            
            // Act & Assert
            Assert.AreNotEqual(zoom1, zoom2);
            Assert.IsTrue(zoom1 != zoom2);
        }
        
        [Test]
        public void ImplicitFloatConversion_ReturnsValue()
        {
            // Arrange
            var zoom = new Zoom(75f, true);
            
            // Act
            float value = zoom;
            
            // Assert
            Assert.AreEqual(75f, value);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var zoom = new Zoom(50f, true);
            object boxedZoom = new Zoom(50f, true);
            object differentObject = "not a zoom";
            
            // Act & Assert
            Assert.IsTrue(zoom.Equals(boxedZoom));
            Assert.IsFalse(zoom.Equals(differentObject));
            Assert.IsFalse(zoom.Equals(null));
        }
    }
}