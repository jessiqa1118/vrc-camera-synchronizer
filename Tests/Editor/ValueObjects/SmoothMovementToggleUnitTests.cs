using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class SmoothMovementToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new SmoothMovementToggle(true).Value);
            Assert.IsFalse(new SmoothMovementToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new SmoothMovementToggle(true);
            var b = new SmoothMovementToggle(true);
            var c = new SmoothMovementToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new SmoothMovementToggle(true);
            bool f = new SmoothMovementToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new SmoothMovementToggle(true).ToString());
            Assert.AreEqual("False", new SmoothMovementToggle(false).ToString());
        }
    }
}

