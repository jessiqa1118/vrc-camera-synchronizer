using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class LockToggleUnitTests
    {
        [Test]
        public void Constructor_WithTrueValue_StoresTrue()
        {
            var toggle = new LockToggle(true);
            Assert.IsTrue(toggle.Value);
        }

        [Test]
        public void Constructor_WithFalseValue_StoresFalse()
        {
            var toggle = new LockToggle(false);
            Assert.IsFalse(toggle.Value);
        }

        [Test]
        public void Equals_WithSameValue_ReturnsTrue()
        {
            var t1 = new LockToggle(true);
            var t2 = new LockToggle(true);
            Assert.IsTrue(t1.Equals(t2));
            Assert.IsTrue(t1 == t2);
            Assert.IsFalse(t1 != t2);
        }

        [Test]
        public void Equals_WithDifferentValue_ReturnsFalse()
        {
            var t1 = new LockToggle(true);
            var t2 = new LockToggle(false);
            Assert.IsFalse(t1.Equals(t2));
            Assert.IsFalse(t1 == t2);
            Assert.IsTrue(t1 != t2);
        }

        [Test]
        public void GetHashCode_VariesByValue()
        {
            var t1 = new LockToggle(true);
            var t2 = new LockToggle(false);
            Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
        }

        [Test]
        public void Implicit_ToBool_ReturnsUnderlyingValue()
        {
            bool v1 = new LockToggle(true);
            bool v2 = new LockToggle(false);
            Assert.IsTrue(v1);
            Assert.IsFalse(v2);
        }

        [Test]
        public void ToString_ReflectsValue()
        {
            Assert.AreEqual("True", new LockToggle(true).ToString());
            Assert.AreEqual("False", new LockToggle(false).ToString());
        }
    }
}

