using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class DurationUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var duration = new Duration(5f);

            Assert.AreEqual(5f, (float)duration);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var duration = new Duration(Duration.MinValue);

            Assert.AreEqual(Duration.MinValue, (float)duration);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var duration = new Duration(Duration.MaxValue);

            Assert.AreEqual(Duration.MaxValue, (float)duration);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Duration(Duration.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Duration(Duration.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Duration(2f);
            var right = new Duration(2f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Duration(2f);
            var right = new Duration(4f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var duration = new Duration(1.5f);

            float value = duration;

            Assert.AreEqual(1.5f, value);
        }
    }
}
