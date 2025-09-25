using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class PhotoRateUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var photoRate = new PhotoRate(1.5f);

            Assert.AreEqual(1.5f, (float)photoRate);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var photoRate = new PhotoRate(PhotoRate.MinValue);

            Assert.AreEqual(PhotoRate.MinValue, (float)photoRate);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var photoRate = new PhotoRate(PhotoRate.MaxValue);

            Assert.AreEqual(PhotoRate.MaxValue, (float)photoRate);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PhotoRate(PhotoRate.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PhotoRate(PhotoRate.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new PhotoRate(0.5f);
            var right = new PhotoRate(0.5f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new PhotoRate(0.5f);
            var right = new PhotoRate(1.5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var photoRate = new PhotoRate(0.75f);

            float value = photoRate;

            Assert.AreEqual(0.75f, value);
        }
    }
}
