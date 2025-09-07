using NUnit.Framework;
using UnityEngine;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class DurationUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesDurationDefaultValue()
        {
            // Act
            var duration = new Duration(Duration.DefaultValue);
            
            // Assert
            Assert.AreEqual(Duration.DefaultValue, duration.Value);
        }
        
        [Test]
        public void Constructor_WithValue_StoresValue()
        {
            // Arrange
            const float testValue = 10f;
            
            // Act
            var duration = new Duration(testValue);
            
            // Assert
            Assert.AreEqual(testValue, duration.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            const float testValue = 0.05f;
            
            // Act
            var duration = new Duration(testValue);
            
            // Assert
            Assert.AreEqual(Duration.MinValue, duration.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            const float testValue = 100f;
            
            // Act
            var duration = new Duration(testValue);
            
            // Assert
            Assert.AreEqual(Duration.MaxValue, duration.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var duration1 = new Duration(30f);
            var duration2 = new Duration(30f);
            
            // Act & Assert
            Assert.IsTrue(duration1.Equals(duration2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var duration1 = new Duration(30f);
            var duration2 = new Duration(45f);
            
            // Act & Assert
            Assert.IsFalse(duration1.Equals(duration2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var duration1 = new Duration(30f);
            var duration2 = new Duration(30f);
            
            // Act & Assert
            Assert.IsTrue(duration1 == duration2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            var duration1 = new Duration(30f);
            var duration2 = new Duration(45f);
            
            // Act & Assert
            Assert.IsTrue(duration1 != duration2);
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var duration = new Duration(15f);
            
            // Act
            float value = duration;
            
            // Assert
            Assert.AreEqual(15f, value);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var duration1 = new Duration(5f);
            var duration2 = new Duration(5f);
            
            // Act & Assert
            Assert.AreEqual(duration1.GetHashCode(), duration2.GetHashCode());
        }
        
        [Test]
        public void MinValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(0.1f, Duration.MinValue);
        }
        
        [Test]
        public void MaxValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(60f, Duration.MaxValue);
        }
        
        [Test]
        public void DefaultValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(2f, Duration.DefaultValue);
        }
    }
}