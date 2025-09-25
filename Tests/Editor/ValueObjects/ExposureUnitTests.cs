using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ExposureUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var exposure = new Exposure(-2.5f);

            Assert.AreEqual(-2.5f, (float)exposure);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var exposure = new Exposure(Exposure.MinValue);

            Assert.AreEqual(Exposure.MinValue, (float)exposure);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var exposure = new Exposure(Exposure.MaxValue);

            Assert.AreEqual(Exposure.MaxValue, (float)exposure);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Exposure(Exposure.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Exposure(Exposure.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Exposure(1.5f);
            var right = new Exposure(1.5f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Exposure(1.5f);
            var right = new Exposure(2.5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var exposure = new Exposure(3f);

            float value = exposure;

            Assert.AreEqual(3f, value);
        }
    }
}
