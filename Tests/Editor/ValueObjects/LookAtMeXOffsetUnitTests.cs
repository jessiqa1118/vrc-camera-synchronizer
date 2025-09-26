using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class LookAtMeXOffsetUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var offset = new LookAtMeXOffset(10f);

            Assert.AreEqual(10f, (float)offset);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var offset = new LookAtMeXOffset(LookAtMeXOffset.MinValue);

            Assert.AreEqual(LookAtMeXOffset.MinValue, (float)offset);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var offset = new LookAtMeXOffset(LookAtMeXOffset.MaxValue);

            Assert.AreEqual(LookAtMeXOffset.MaxValue, (float)offset);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeXOffset(LookAtMeXOffset.MinValue - 1f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeXOffset(LookAtMeXOffset.MaxValue + 1f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new LookAtMeXOffset(5f);
            var right = new LookAtMeXOffset(5f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new LookAtMeXOffset(5f);
            var right = new LookAtMeXOffset(-5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var offset = new LookAtMeXOffset(12.5f);

            float value = offset;

            Assert.AreEqual(12.5f, value);
        }
    }
}
