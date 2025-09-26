using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class HueUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var hue = new Hue(180f);

            Assert.AreEqual(180f, (float)hue);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var hue = new Hue(Hue.MinValue);

            Assert.AreEqual(Hue.MinValue, (float)hue);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var hue = new Hue(Hue.MaxValue);

            Assert.AreEqual(Hue.MaxValue, (float)hue);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Hue(Hue.MinValue - 1f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Hue(Hue.MaxValue + 1f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Hue(90f);
            var right = new Hue(90f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Hue(90f);
            var right = new Hue(120f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var hue = new Hue(200f);

            float value = hue;

            Assert.AreEqual(200f, value);
        }
    }
}
