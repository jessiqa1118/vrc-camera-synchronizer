using System;
using Parameters;
using OSC;
using NUnit.Framework;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class ArgumentUnitTests
    {
        [Test]
        public void Constructor_WithInt_StoresValueAndType()
        {
            // Arrange
            const int value = 42;
            
            // Act
            var argument = new Argument(value);
            
            // Assert
            Assert.AreEqual(value, argument.Value);
            Assert.AreEqual(Argument.ValueType.Int32, argument.Type);
        }
        
        [Test]
        public void Constructor_WithFloat_StoresValueAndType()
        {
            // Arrange
            const float value = 3.14f;
            
            // Act
            var argument = new Argument(value);
            
            // Assert
            Assert.AreEqual(value, argument.Value);
            Assert.AreEqual(Argument.ValueType.Float32, argument.Type);
        }
        
        [Test]
        public void Constructor_WithString_StoresValueAndType()
        {
            // Arrange
            const string value = "test";
            
            // Act
            var argument = new Argument(value);
            
            // Assert
            Assert.AreEqual(value, argument.Value);
            Assert.AreEqual(Argument.ValueType.String, argument.Type);
        }
        
        [Test]
        public void Constructor_WithNullString_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Argument((string)null));
        }
        
        [Test]
        public void Constructor_WithByteArray_StoresDefensiveCopy()
        {
            // Arrange
            byte[] value = { 1, 2, 3, 4 };
            
            // Act
            var argument = new Argument(value);
            value[0] = 99; // Modify original array
            
            // Assert
            var stored = (byte[])argument.Value;
            Assert.AreEqual(1, stored[0]); // Should be unchanged
            Assert.AreEqual(Argument.ValueType.Blob, argument.Type);
        }
        
        [Test]
        public void Constructor_WithNullByteArray_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Argument((byte[])null));
        }
        
        [Test]
        public void Constructor_WithBool_StoresValueAndType()
        {
            // Act
            var trueArg = new Argument(true);
            var falseArg = new Argument(false);
            
            // Assert
            Assert.AreEqual(true, trueArg.Value);
            Assert.AreEqual(Argument.ValueType.Bool, trueArg.Type);
            Assert.AreEqual(false, falseArg.Value);
            Assert.AreEqual(Argument.ValueType.Bool, falseArg.Type);
        }
        
        [Test]
        public void AsInt32_WithInt32Type_ReturnsValue()
        {
            // Arrange
            var argument = new Argument(42);
            
            // Act
            var result = argument.AsInt32();
            
            // Assert
            Assert.AreEqual(42, result);
        }
        
        [Test]
        public void AsInt32_WithFloat32Type_ReturnsConvertedValue()
        {
            // Arrange
            var argument = new Argument(42.7f);
            
            // Act
            var result = argument.AsInt32();
            
            // Assert
            Assert.AreEqual(42, result);
        }
        
        [Test]
        public void AsInt32_WithStringType_ThrowsException()
        {
            // Arrange
            var argument = new Argument("test");
            
            // Act & Assert
            Assert.Throws<InvalidCastException>(() => argument.AsInt32());
        }
        
        [Test]
        public void AsFloat32_WithFloat32Type_ReturnsValue()
        {
            // Arrange
            var argument = new Argument(3.14f);
            
            // Act
            var result = argument.AsFloat32();
            
            // Assert
            Assert.AreEqual(3.14f, result);
        }
        
        [Test]
        public void AsFloat32_WithInt32Type_ReturnsConvertedValue()
        {
            // Arrange
            var argument = new Argument(42);
            
            // Act
            var result = argument.AsFloat32();
            
            // Assert
            Assert.AreEqual(42f, result);
        }
        
        [Test]
        public void AsString_WithStringType_ReturnsValue()
        {
            // Arrange
            var argument = new Argument("test");
            
            // Act
            var result = argument.AsString();
            
            // Assert
            Assert.AreEqual("test", result);
        }
        
        [Test]
        public void AsString_WithOtherTypes_ReturnsStringRepresentation()
        {
            // Arrange
            var intArg = new Argument(42);
            var floatArg = new Argument(3.14f);
            
            // Act & Assert
            Assert.AreEqual("42", intArg.AsString());
            Assert.AreEqual("3.14", floatArg.AsString());
        }
        
        [Test]
        public void AsBlob_WithBlobType_ReturnsDefensiveCopy()
        {
            // Arrange
            byte[] data = { 1, 2, 3 };
            var argument = new Argument(data);
            
            // Act
            var result = argument.AsBlob();
            result[0] = 99; // Modify returned array
            
            // Assert
            var secondResult = argument.AsBlob();
            Assert.AreEqual(1, secondResult[0]); // Should be unchanged
        }
        
        [Test]
        public void AsBlob_WithNonBlobType_ThrowsException()
        {
            // Arrange
            var argument = new Argument(42);
            
            // Act & Assert
            Assert.Throws<InvalidCastException>(() => argument.AsBlob());
        }
        
        [Test]
        public void AsBool_WithBoolType_ReturnsValue()
        {
            // Arrange
            var trueArg = new Argument(true);
            var falseArg = new Argument(false);
            
            // Act & Assert
            Assert.IsTrue(trueArg.AsBool());
            Assert.IsFalse(falseArg.AsBool());
        }
        
        [Test]
        public void AsBool_WithInt32Type_ReturnsCorrectValue()
        {
            // Arrange
            var zeroArg = new Argument(0);
            var nonZeroArg = new Argument(1);
            
            // Act & Assert
            Assert.IsFalse(zeroArg.AsBool());
            Assert.IsTrue(nonZeroArg.AsBool());
        }
        
        [Test]
        public void Equality_SameTypeAndValue_AreEqual()
        {
            // Arrange
            var arg1 = new Argument(42);
            var arg2 = new Argument(42);
            
            // Act & Assert
            Assert.AreEqual(arg1, arg2);
            Assert.IsTrue(arg1.Equals(arg2));
            Assert.IsTrue(arg1 == arg2);
            Assert.IsFalse(arg1 != arg2);
            Assert.AreEqual(arg1.GetHashCode(), arg2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var arg1 = new Argument(42);
            var arg2 = new Argument(43);
            
            // Act & Assert
            Assert.AreNotEqual(arg1, arg2);
            Assert.IsFalse(arg1.Equals(arg2));
            Assert.IsFalse(arg1 == arg2);
            Assert.IsTrue(arg1 != arg2);
        }
        
        [Test]
        public void Equality_DifferentTypes_AreNotEqual()
        {
            // Arrange
            var intArg = new Argument(42);
            var floatArg = new Argument(42f);
            
            // Act & Assert
            Assert.AreNotEqual(intArg, floatArg);
            Assert.IsFalse(intArg.Equals(floatArg));
        }
        
        [Test]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var intArg = new Argument(42);
            var stringArg = new Argument("test");
            
            // Act & Assert
            Assert.AreEqual("Int32: 42", intArg.ToString());
            Assert.AreEqual("String: test", stringArg.ToString());
        }
    }
}