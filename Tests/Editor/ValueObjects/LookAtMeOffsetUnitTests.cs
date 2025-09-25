using System;
using Astearium.VRChat.Camera;
using NUnit.Framework;
using UnityEngine;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class LookAtMeOffsetUnitTests
    {
        [Test]
        public void Constructor_WithFloatXAndY_StoresValues()
        {
            var offset = new LookAtMeOffset(10f, -15f);

            Assert.AreEqual(10f, (float)offset.X);
            Assert.AreEqual(-15f, (float)offset.Y);
        }

        [Test]
        public void Constructor_WithLookAtMeOffsets_StoresValues()
        {
            var x = new LookAtMeXOffset(8f);
            var y = new LookAtMeYOffset(-6f);

            var offset = new LookAtMeOffset(x, y);

            Assert.AreEqual(x, offset.X);
            Assert.AreEqual(y, offset.Y);
        }

        [Test]
        public void Constructor_WithVector2_StoresValues()
        {
            var vector = new Vector2(5f, -3f);

            var offset = new LookAtMeOffset(vector);

            Assert.AreEqual(vector.x, (float)offset.X);
            Assert.AreEqual(vector.y, (float)offset.Y);
        }

        [Test]
        public void Constructor_WithFloatValuesOutsideRange_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeOffset(30f, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeOffset(0f, 30f));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeOffset(-30f, 0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new LookAtMeOffset(0f, -30f));
        }

        [Test]
        public void ToVector2_ReturnsExpectedVector()
        {
            var offset = new LookAtMeOffset(3f, -7f);

            var vector = offset.ToVector2();

            Assert.AreEqual(3f, vector.x);
            Assert.AreEqual(-7f, vector.y);
        }

        [Test]
        public void Equality_SameValues_AreEqual()
        {
            var left = new LookAtMeOffset(5f, 10f);
            var right = new LookAtMeOffset(5f, 10f);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.AreEqual(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValues_AreNotEqual()
        {
            var left = new LookAtMeOffset(5f, 10f);
            var right = new LookAtMeOffset(5f, 12f);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.AreNotEqual(left, right);
        }

        [Test]
        public void ImplicitConversion_ReturnsVector2()
        {
            var offset = new LookAtMeOffset(12f, -8f);

            Vector2 vector = offset;

            Assert.AreEqual(12f, vector.x);
            Assert.AreEqual(-8f, vector.y);
        }
    }
}
