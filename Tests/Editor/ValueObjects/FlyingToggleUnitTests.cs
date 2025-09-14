using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class FlyingToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new FlyingToggle(true).Value);
            Assert.IsFalse(new FlyingToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new FlyingToggle(true);
            var b = new FlyingToggle(true);
            var c = new FlyingToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new FlyingToggle(true);
            bool f = new FlyingToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new FlyingToggle(true).ToString());
            Assert.AreEqual("False", new FlyingToggle(false).ToString());
        }
    }
}

