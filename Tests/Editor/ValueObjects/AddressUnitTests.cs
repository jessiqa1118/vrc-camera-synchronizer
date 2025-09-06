using System;
using NUnit.Framework;

namespace JessiQa.Tests.Unit
{
    [TestFixture]
    public class AddressUnitTests
    {
        [Test]
        public void Constructor_WithValidPath_StoresValue()
        {
            // Arrange
            const string expectedPath = "/test/path";
            
            // Act
            var address = new Address(expectedPath);
            
            // Assert
            Assert.AreEqual(expectedPath, address.Value);
        }
        
        [Test]
        public void Constructor_WithNullValue_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Address(null));
        }
        
        [Test]
        public void Constructor_WithEmptyString_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Address(""));
        }
        
        [Test]
        public void Constructor_WithWhitespace_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Address("   "));
        }
        
        [Test]
        public void Constructor_WithoutLeadingSlash_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Address("test/path"));
        }
        
        [TestCase("/path with spaces")]
        [TestCase("/path#with#hash")]
        [TestCase("/path*with*asterisk")]
        [TestCase("/path,with,comma")]
        [TestCase("/path?with?question")]
        [TestCase("/path[with]brackets")]
        [TestCase("/path{with}braces")]
        public void Constructor_WithInvalidCharacters_ThrowsException(string invalidPath)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _ = new Address(invalidPath));
        }
        
        [TestCase("/")]
        [TestCase("/simple")]
        [TestCase("/multi/level/path")]
        [TestCase("/usercamera/Zoom")]
        [TestCase("/avatar/parameters/VRCEmote")]
        public void Constructor_WithValidPaths_StoresCorrectly(string validPath)
        {
            // Act
            var address = new Address(validPath);
            
            // Assert
            Assert.AreEqual(validPath, address.Value);
        }
        
        [Test]
        public void Equality_SameValues_AreEqual()
        {
            // Arrange
            var address1 = new Address("/test/path");
            var address2 = new Address("/test/path");
            
            // Act & Assert
            Assert.AreEqual(address1, address2);
            Assert.IsTrue(address1.Equals(address2));
            Assert.IsTrue(address1 == address2);
            Assert.IsFalse(address1 != address2);
            Assert.AreEqual(address1.GetHashCode(), address2.GetHashCode());
        }
        
        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            // Arrange
            var address1 = new Address("/test/path1");
            var address2 = new Address("/test/path2");
            
            // Act & Assert
            Assert.AreNotEqual(address1, address2);
            Assert.IsFalse(address1.Equals(address2));
            Assert.IsFalse(address1 == address2);
            Assert.IsTrue(address1 != address2);
        }
        
        [Test]
        public void ToString_ReturnsValue()
        {
            // Arrange
            const string path = "/test/path";
            var address = new Address(path);
            
            // Act
            var result = address.ToString();
            
            // Assert
            Assert.AreEqual(path, result);
        }
        
        [Test]
        public void ImplicitStringConversion_ReturnsValue()
        {
            // Arrange
            const string path = "/test/path";
            var address = new Address(path);
            
            // Act
            string result = address;
            
            // Assert
            Assert.AreEqual(path, result);
        }
        
        [Test]
        public void Equality_WithBoxedObject_WorksCorrectly()
        {
            // Arrange
            var address = new Address("/test");
            object boxedAddress = new Address("/test");
            object differentObject = "not an address";
            
            // Act & Assert
            Assert.IsTrue(address.Equals(boxedAddress));
            Assert.IsFalse(address.Equals(differentObject));
            Assert.IsFalse(address.Equals(null));
        }
    }
}