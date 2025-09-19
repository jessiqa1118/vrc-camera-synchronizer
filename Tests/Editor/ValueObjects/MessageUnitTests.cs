using System;
using Astearium.VRChat.Camera;
using Astearium.Osc;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class MessageUnitTests
    {
        [Test]
        public void Constructor_WithValidAddressAndArguments_StoresValues()
        {
            // Arrange
            var address = new Address("/test/path");
            var arguments = new[] { new Argument(42), new Argument(3.14f) };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual(address, message.Address);
            Assert.AreEqual(arguments, message.Arguments);
            Assert.IsNotNull(message.TypeTag);
        }
        
        [Test]
        public void Constructor_WithNullArguments_ThrowsException()
        {
            // Arrange
            var address = new Address("/test/path");
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new Message(address, null));
        }
        
        [Test]
        public void Constructor_WithEmptyArguments_CreatesEmptyTypeTag()
        {
            // Arrange
            var address = new Address("/test/path");
            var arguments = Array.Empty<Argument>();
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual(address, message.Address);
            Assert.AreEqual(0, message.Arguments.Length);
            Assert.AreEqual("", message.TypeTag.Value);
        }
        
        [Test]
        public void TypeTag_GeneratedCorrectlyFromArguments()
        {
            // Arrange
            var address = new Address("/test");
            var arguments = new[]
            {
                new Argument(42),        // i
                new Argument(3.14f),     // f
                new Argument("test"),    // s
                new Argument(true)       // T
            };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("ifsT", message.TypeTag.Value);
        }
        
        [Test]
        public void TypeTag_WithBoolFalse_GeneratesF()
        {
            // Arrange
            var address = new Address("/test");
            var arguments = new[] { new Argument(false) };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("F", message.TypeTag.Value);
        }
        
        [Test]
        public void TypeTag_WithBlob_GeneratesB()
        {
            // Arrange
            var address = new Address("/test");
            var arguments = new[] { new Argument(new byte[] { 1, 2, 3 }) };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("b", message.TypeTag.Value);
        }
        
        [Test]
        public void Message_WithSingleInt_CreatesCorrectStructure()
        {
            // Arrange
            var address = new Address("/usercamera/Mode");
            var arguments = new[] { new Argument(1) };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("/usercamera/Mode", message.Address.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(1, message.Arguments[0].Value);
            Assert.AreEqual("i", message.TypeTag.Value);
        }
        
        [Test]
        public void Message_WithMultipleFloats_CreatesCorrectStructure()
        {
            // Arrange
            var address = new Address("/position");
            var arguments = new[]
            {
                new Argument(1.0f),
                new Argument(2.0f),
                new Argument(3.0f)
            };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("/position", message.Address.Value);
            Assert.AreEqual(3, message.Arguments.Length);
            Assert.AreEqual("fff", message.TypeTag.Value);
        }
        
        [Test]
        public void Message_PreservesArgumentOrder()
        {
            // Arrange
            var address = new Address("/mixed");
            var arguments = new[]
            {
                new Argument("first"),
                new Argument(42),
                new Argument(3.14f),
                new Argument(true)
            };
            
            // Act
            var message = new Message(address, arguments);
            
            // Assert
            Assert.AreEqual("first", message.Arguments[0].Value);
            Assert.AreEqual(42, message.Arguments[1].Value);
            Assert.AreEqual(3.14f, message.Arguments[2].Value);
            Assert.AreEqual(true, message.Arguments[3].Value);
            Assert.AreEqual("sifT", message.TypeTag.Value);
        }
    }
}