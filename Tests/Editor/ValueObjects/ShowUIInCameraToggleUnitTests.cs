using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class ShowUIInCameraToggleUnitTests
    {
        [Test]
        public void Constructor_WithTrueValue_StoresTrue()
        {
            // Act
            var toggle = new ShowUIInCameraToggle(true);
            
            // Assert
            Assert.IsTrue(toggle.Value);
        }
        
        [Test]
        public void Constructor_WithFalseValue_StoresFalse()
        {
            // Act
            var toggle = new ShowUIInCameraToggle(false);
            
            // Assert
            Assert.IsFalse(toggle.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var toggle1 = new ShowUIInCameraToggle(true);
            var toggle2 = new ShowUIInCameraToggle(true);
            
            // Act & Assert
            Assert.IsTrue(toggle1.Equals(toggle2));
            Assert.IsTrue(toggle1 == toggle2);
            Assert.IsFalse(toggle1 != toggle2);
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var toggle1 = new ShowUIInCameraToggle(true);
            var toggle2 = new ShowUIInCameraToggle(false);
            
            // Act & Assert
            Assert.IsFalse(toggle1.Equals(toggle2));
            Assert.IsFalse(toggle1 == toggle2);
            Assert.IsTrue(toggle1 != toggle2);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var toggle1 = new ShowUIInCameraToggle(true);
            var toggle2 = new ShowUIInCameraToggle(true);
            
            // Act & Assert
            Assert.AreEqual(toggle1.GetHashCode(), toggle2.GetHashCode());
        }
        
        [Test]
        public void GetHashCode_WithDifferentValue_ReturnsDifferentHash()
        {
            // Arrange
            var toggle1 = new ShowUIInCameraToggle(true);
            var toggle2 = new ShowUIInCameraToggle(false);
            
            // Act & Assert
            Assert.AreNotEqual(toggle1.GetHashCode(), toggle2.GetHashCode());
        }
        
        [Test]
        public void ImplicitOperator_ToBool_ReturnsCorrectValue()
        {
            // Arrange
            var toggleTrue = new ShowUIInCameraToggle(true);
            var toggleFalse = new ShowUIInCameraToggle(false);
            
            // Act
            bool valueTrue = toggleTrue;
            bool valueFalse = toggleFalse;
            
            // Assert
            Assert.IsTrue(valueTrue);
            Assert.IsFalse(valueFalse);
        }
        
        [Test]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            var toggleTrue = new ShowUIInCameraToggle(true);
            var toggleFalse = new ShowUIInCameraToggle(false);
            
            // Act & Assert
            Assert.AreEqual("True", toggleTrue.ToString());
            Assert.AreEqual("False", toggleFalse.ToString());
        }
        
        [Test]
        public void Equals_WithNullObject_ReturnsFalse()
        {
            // Arrange
            var toggle = new ShowUIInCameraToggle(true);
            
            // Act & Assert
            Assert.IsFalse(toggle.Equals(null));
        }
        
        [Test]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            var toggle = new ShowUIInCameraToggle(true);
            
            // Act & Assert
            Assert.IsFalse(toggle.Equals("true"));
            Assert.IsFalse(toggle.Equals(1));
        }
    }
}