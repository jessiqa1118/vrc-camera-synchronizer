using NUnit.Framework;
using Astearium.VRChat.Camera;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class LookAtMeXOffsetUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesLookAtMeXOffsetDefaultValue()
        {
            // Act
            var offset = new LookAtMeXOffset(LookAtMeXOffset.DefaultValue);
            
            // Assert
            Assert.AreEqual(LookAtMeXOffset.DefaultValue, offset.Value);
        }
        
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            // Arrange
            const float expectedValue = 10f;
            
            // Act
            var offset = new LookAtMeXOffset(expectedValue);
            
            // Assert
            Assert.AreEqual(expectedValue, offset.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMinValue()
        {
            // Arrange
            const float inputValue = -30f;
            
            // Act
            var offset = new LookAtMeXOffset(inputValue);
            
            // Assert
            Assert.AreEqual(LookAtMeXOffset.MinValue, offset.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMaxValue()
        {
            // Arrange
            const float inputValue = 30f;
            
            // Act
            var offset = new LookAtMeXOffset(inputValue);
            
            // Assert
            Assert.AreEqual(LookAtMeXOffset.MaxValue, offset.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var offset1 = new LookAtMeXOffset(5f);
            var offset2 = new LookAtMeXOffset(5f);
            
            // Act & Assert
            Assert.IsTrue(offset1.Equals(offset2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var offset1 = new LookAtMeXOffset(5f);
            var offset2 = new LookAtMeXOffset(-5f);
            
            // Act & Assert
            Assert.IsFalse(offset1.Equals(offset2));
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var offset = new LookAtMeXOffset(12.5f);
            
            // Act
            float value = offset;
            
            // Assert
            Assert.AreEqual(12.5f, value);
        }
    }
}
