using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class FocalDistanceUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var focalDistance = new FocalDistance(5f);

            Assert.AreEqual(5f, (float)focalDistance);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var focalDistance = new FocalDistance(FocalDistance.MinValue);

            Assert.AreEqual(FocalDistance.MinValue, (float)focalDistance);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var focalDistance = new FocalDistance(FocalDistance.MaxValue);

            Assert.AreEqual(FocalDistance.MaxValue, (float)focalDistance);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new FocalDistance(FocalDistance.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new FocalDistance(FocalDistance.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new FocalDistance(3f);
            var right = new FocalDistance(3f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new FocalDistance(3f);
            var right = new FocalDistance(4f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var focalDistance = new FocalDistance(2f);

            float value = focalDistance;

            Assert.AreEqual(2f, value);
        }
    }
}
