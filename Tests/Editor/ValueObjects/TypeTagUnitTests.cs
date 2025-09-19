using System;
using Astearium.Osc;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class TypeTagUnitTests
    {
        [Test]
        public void Constructor_WithEmptyArguments_GeneratesEmptyString()
        {
            // Arrange
            var arguments = Array.Empty<Argument>();

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("", typeTag.Value);
        }

        [Test]
        public void Constructor_WithSingleInt32_GeneratesI()
        {
            // Arrange
            var arguments = new[] { new Argument(42) };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("i", typeTag.Value);
        }

        [Test]
        public void Constructor_WithSingleFloat32_GeneratesF()
        {
            // Arrange
            var arguments = new[] { new Argument(3.14f) };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("f", typeTag.Value);
        }

        [Test]
        public void Constructor_WithSingleString_GeneratesS()
        {
            // Arrange
            var arguments = new[] { new Argument("test") };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("s", typeTag.Value);
        }

        [Test]
        public void Constructor_WithSingleBlob_GeneratesB()
        {
            // Arrange
            var arguments = new[] { new Argument(new byte[] { 1, 2, 3 }) };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("b", typeTag.Value);
        }

        [Test]
        public void Constructor_WithBoolTrue_GeneratesT()
        {
            // Arrange
            var arguments = new[] { new Argument(true) };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("T", typeTag.Value);
        }

        [Test]
        public void Constructor_WithBoolFalse_GeneratesF()
        {
            // Arrange
            var arguments = new[] { new Argument(false) };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("F", typeTag.Value);
        }

        [Test]
        public void Constructor_WithMultipleArguments_GeneratesCorrectPattern()
        {
            // Arrange
            var arguments = new[]
            {
                new Argument(42),
                new Argument(3.14f),
                new Argument("test"),
                new Argument(new byte[] { 1, 2 }),
                new Argument(true),
                new Argument(false)
            };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("ifsbTF", typeTag.Value);
        }

        [Test]
        public void Constructor_WithTwoIntegers_GeneratesII()
        {
            // Arrange
            var arguments = new[]
            {
                new Argument(1),
                new Argument(2)
            };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("ii", typeTag.Value);
        }

        [Test]
        public void Constructor_WithThreeFloats_GeneratesFFF()
        {
            // Arrange
            var arguments = new[]
            {
                new Argument(1.0f),
                new Argument(2.0f),
                new Argument(3.0f)
            };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("fff", typeTag.Value);
        }

        [Test]
        public void Constructor_WithFourFloats_GeneratesFFFF()
        {
            // Arrange
            var arguments = new[]
            {
                new Argument(1.0f),
                new Argument(2.0f),
                new Argument(3.0f),
                new Argument(4.0f)
            };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("ffff", typeTag.Value);
        }

        [Test]
        public void Constructor_PreservesArgumentOrder()
        {
            // Arrange
            var arguments = new[]
            {
                new Argument("first"),
                new Argument(42),
                new Argument(3.14f),
                new Argument(true),
                new Argument(false)
            };

            // Act
            var typeTag = new TypeTag(arguments);

            // Assert
            Assert.AreEqual("sifTF", typeTag.Value);
        }

        [Test]
        public void Value_IsReadOnly()
        {
            // Arrange
            var arguments = new[] { new Argument(42) };
            var typeTag = new TypeTag(arguments);

            // Act & Assert
            Assert.IsTrue(typeof(TypeTag).GetField("Value").IsInitOnly);
            Assert.AreEqual("i", typeTag.Value);
        }
    }
}