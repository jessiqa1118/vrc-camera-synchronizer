using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class TurnSpeedUnitTests
    {
        [Test]
        public void Constructor_WithValidValue_StoresValue()
        {
            var turnSpeed = new TurnSpeed(3f);

            Assert.AreEqual(3f, (float)turnSpeed);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var turnSpeed = new TurnSpeed(TurnSpeed.MinValue);

            Assert.AreEqual(TurnSpeed.MinValue, (float)turnSpeed);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var turnSpeed = new TurnSpeed(TurnSpeed.MaxValue);

            Assert.AreEqual(TurnSpeed.MaxValue, (float)turnSpeed);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new TurnSpeed(TurnSpeed.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new TurnSpeed(TurnSpeed.MaxValue + 0.01f));
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new TurnSpeed(4f);
            var right = new TurnSpeed(4f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new TurnSpeed(4f);
            var right = new TurnSpeed(5f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var turnSpeed = new TurnSpeed(6f);

            float value = turnSpeed;

            Assert.AreEqual(6f, value);
        }
    }
}
