using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class SaturationUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var saturation = new Saturation(1.2f);

            Assert.AreEqual(1.2f, (float)saturation);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var saturation = new Saturation(Saturation.MinValue);

            Assert.AreEqual(Saturation.MinValue, (float)saturation);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var saturation = new Saturation(Saturation.MaxValue);

            Assert.AreEqual(Saturation.MaxValue, (float)saturation);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Saturation(Saturation.MinValue - 0.1f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Saturation(Saturation.MaxValue + 0.1f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Saturation(0.5f);
            var right = new Saturation(0.5f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Saturation(0.5f);
            var right = new Saturation(1.5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var saturation = new Saturation(0.75f);

            float value = saturation;

            Assert.AreEqual(0.75f, value);
        }
    }
}
