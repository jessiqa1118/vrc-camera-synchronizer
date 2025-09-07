using NUnit.Framework;
using UnityEngine;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class TurnSpeedUnitTests
    {
        [Test]
        public void Constructor_WithDefaultValue_UsesTurnSpeedDefaultValue()
        {
            // Act
            var turnSpeed = new TurnSpeed();
            
            // Assert
            Assert.AreEqual(TurnSpeed.DefaultValue, turnSpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValue_StoresValue()
        {
            // Arrange
            const float testValue = 2.5f;
            
            // Act
            var turnSpeed = new TurnSpeed(testValue);
            
            // Assert
            Assert.AreEqual(testValue, turnSpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValueBelowMin_ClampsToMin()
        {
            // Arrange
            const float testValue = 0.05f;
            
            // Act
            var turnSpeed = new TurnSpeed(testValue);
            
            // Assert
            Assert.AreEqual(TurnSpeed.MinValue, turnSpeed.Value);
        }
        
        [Test]
        public void Constructor_WithValueAboveMax_ClampsToMax()
        {
            // Arrange
            const float testValue = 10f;
            
            // Act
            var turnSpeed = new TurnSpeed(testValue);
            
            // Assert
            Assert.AreEqual(TurnSpeed.MaxValue, turnSpeed.Value);
        }
        
        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var turnSpeed1 = new TurnSpeed(2.5f);
            var turnSpeed2 = new TurnSpeed(2.5f);
            
            // Act & Assert
            Assert.IsTrue(turnSpeed1.Equals(turnSpeed2));
        }
        
        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            // Arrange
            var turnSpeed1 = new TurnSpeed(2.5f);
            var turnSpeed2 = new TurnSpeed(3f);
            
            // Act & Assert
            Assert.IsFalse(turnSpeed1.Equals(turnSpeed2));
        }
        
        [Test]
        public void EqualsOperator_WithSameValue_ReturnsTrue()
        {
            // Arrange
            var turnSpeed1 = new TurnSpeed(2.5f);
            var turnSpeed2 = new TurnSpeed(2.5f);
            
            // Act & Assert
            Assert.IsTrue(turnSpeed1 == turnSpeed2);
        }
        
        [Test]
        public void NotEqualsOperator_WithDifferentValue_ReturnsTrue()
        {
            // Arrange
            var turnSpeed1 = new TurnSpeed(2.5f);
            var turnSpeed2 = new TurnSpeed(3f);
            
            // Act & Assert
            Assert.IsTrue(turnSpeed1 != turnSpeed2);
        }
        
        [Test]
        public void ImplicitOperator_ToFloat_ReturnsValue()
        {
            // Arrange
            var turnSpeed = new TurnSpeed(3.5f);
            
            // Act
            float value = turnSpeed;
            
            // Assert
            Assert.AreEqual(3.5f, value);
        }
        
        [Test]
        public void GetHashCode_WithSameValue_ReturnsSameHash()
        {
            // Arrange
            var turnSpeed1 = new TurnSpeed(2f);
            var turnSpeed2 = new TurnSpeed(2f);
            
            // Act & Assert
            Assert.AreEqual(turnSpeed1.GetHashCode(), turnSpeed2.GetHashCode());
        }
        
        [Test]
        public void MinValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(0.1f, TurnSpeed.MinValue);
        }
        
        [Test]
        public void MaxValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(5f, TurnSpeed.MaxValue);
        }
        
        [Test]
        public void DefaultValue_IsCorrect()
        {
            // Assert
            Assert.AreEqual(1f, TurnSpeed.DefaultValue);
        }
    }
}