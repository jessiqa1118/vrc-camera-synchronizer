using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class ZoomUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 45f;
            
            // Act
            var zoom = new Zoom(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = 10f;
            
            // Act
            var zoom = new Zoom(inputValue);
            
            // Assert
            Assert.AreEqual(Zoom.MinValue, zoom.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 200f;
            
            // Act
            var zoom = new Zoom(inputValue);
            
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
            var zoom = new Zoom(value);
            
            // Assert
            Assert.AreEqual(value, zoom.Value);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45f);
            var zoom2 = new Zoom(45f);
            
            // Act & Assert
            Assert.AreEqual(zoom1, zoom2);
            Assert.IsTrue(zoom1.Equals(zoom2));
            Assert.AreEqual(zoom1.GetHashCode(), zoom2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var zoom1 = new Zoom(45f);
            var zoom2 = new Zoom(90f);
            
            // Act & Assert
            Assert.AreNotEqual(zoom1, zoom2);
            Assert.IsFalse(zoom1.Equals(zoom2));
        }
        
        [Test]
        public void MinMaxValues_AreCorrect()
        {
            // Assert
            Assert.AreEqual(20f, Zoom.MinValue);
            Assert.AreEqual(150f, Zoom.MaxValue);
        }
    }
}