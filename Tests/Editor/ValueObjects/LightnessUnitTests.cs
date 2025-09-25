using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class LightnessUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var lightness = new Lightness(0.4f);

            Assert.AreEqual(0.4f, (float)lightness);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var lightness = new Lightness(Lightness.MinValue);

            Assert.AreEqual(Lightness.MinValue, (float)lightness);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var lightness = new Lightness(Lightness.MaxValue);

            Assert.AreEqual(Lightness.MaxValue, (float)lightness);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Lightness(Lightness.MinValue - 0.1f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Lightness(Lightness.MaxValue + 0.1f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Lightness(0.2f);
            var right = new Lightness(0.2f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Lightness(-0.4f);
            var right = new Lightness(0.4f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var lightness = new Lightness(-0.2f);

            float value = lightness;

            Assert.AreEqual(-0.2f, value);
        }
    }
}
