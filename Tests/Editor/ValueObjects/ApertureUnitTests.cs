using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ApertureUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var aperture = new Aperture(5.6f);

            Assert.AreEqual(5.6f, (float)aperture);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var aperture = new Aperture(Aperture.MinValue);

            Assert.AreEqual(Aperture.MinValue, (float)aperture);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var aperture = new Aperture(Aperture.MaxValue);

            Assert.AreEqual(Aperture.MaxValue, (float)aperture);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Aperture(Aperture.MinValue - 0.1f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Aperture(Aperture.MaxValue + 0.1f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Aperture(8f);
            var right = new Aperture(8f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Aperture(2.8f);
            var right = new Aperture(5.6f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var aperture = new Aperture(11f);

            float value = aperture;

            Assert.AreEqual(11f, value);
        }
    }
}
