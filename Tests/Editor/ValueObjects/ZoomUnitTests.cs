using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ZoomUnitTests
    {
        [Test]
        public void Constructor_WithValueInsideRange_StoresValue()
        {
            var zoom = new Zoom(75f);

            Assert.AreEqual(75f, (float)zoom);
        }

        [Test]
        public void Constructor_WithMinValue_AllowsValue()
        {
            var zoom = new Zoom(Zoom.MinValue);

            Assert.AreEqual(Zoom.MinValue, (float)zoom);
        }

        [Test]
        public void Constructor_WithMaxValue_AllowsValue()
        {
            var zoom = new Zoom(Zoom.MaxValue);

            Assert.AreEqual(Zoom.MaxValue, (float)zoom);
        }

        [Test]
        public void Constructor_WithValueBelowMin_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Zoom(Zoom.MinValue - 0.01f));
        }

        [Test]
        public void Constructor_WithValueAboveMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Zoom(Zoom.MaxValue + 0.01f));
        }

        [Test]
        public void DefaultValue_IsFortyFive()
        {
            Assert.AreEqual(45f, Zoom.DefaultValue);
        }

        [Test]
        public void MinValue_IsTwenty()
        {
            Assert.AreEqual(20f, Zoom.MinValue);
        }

        [Test]
        public void MaxValue_IsOneHundredFifty()
        {
            Assert.AreEqual(150f, Zoom.MaxValue);
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new Zoom(80f);
            var right = new Zoom(80f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new Zoom(80f);
            var right = new Zoom(90f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingFloat()
        {
            var zoom = new Zoom(60f);

            float value = zoom;

            Assert.AreEqual(60f, value);
        }
    }
}
