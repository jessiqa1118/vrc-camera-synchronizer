using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class SmoothingStrengthUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var smoothingStrength = new SmoothingStrength(0.5f);

            Assert.AreEqual(0.5f, (float)smoothingStrength);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var smoothingStrength = new SmoothingStrength(SmoothingStrength.MinValue);

            Assert.AreEqual(SmoothingStrength.MinValue, (float)smoothingStrength);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var smoothingStrength = new SmoothingStrength(SmoothingStrength.MaxValue);

            Assert.AreEqual(SmoothingStrength.MaxValue, (float)smoothingStrength);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new SmoothingStrength(SmoothingStrength.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new SmoothingStrength(SmoothingStrength.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new SmoothingStrength(0.25f);
            var right = new SmoothingStrength(0.25f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new SmoothingStrength(0.25f);
            var right = new SmoothingStrength(0.75f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var smoothingStrength = new SmoothingStrength(0.9f);

            float value = smoothingStrength;

            Assert.AreEqual(0.9f, value);
        }
    }
}
