using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class FlySpeedUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var flySpeed = new FlySpeed(3f);

            Assert.AreEqual(3f, (float)flySpeed);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var flySpeed = new FlySpeed(FlySpeed.MinValue);

            Assert.AreEqual(FlySpeed.MinValue, (float)flySpeed);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var flySpeed = new FlySpeed(FlySpeed.MaxValue);

            Assert.AreEqual(FlySpeed.MaxValue, (float)flySpeed);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new FlySpeed(FlySpeed.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new FlySpeed(FlySpeed.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new FlySpeed(4f);
            var right = new FlySpeed(4f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new FlySpeed(4f);
            var right = new FlySpeed(5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var flySpeed = new FlySpeed(6f);

            float value = flySpeed;

            Assert.AreEqual(6f, value);
        }
    }
}
