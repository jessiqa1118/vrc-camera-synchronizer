using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class RollWhileFlyingToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new RollWhileFlyingToggle(true).Value);
            Assert.IsFalse(new RollWhileFlyingToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new RollWhileFlyingToggle(true);
            var b = new RollWhileFlyingToggle(true);
            var c = new RollWhileFlyingToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new RollWhileFlyingToggle(true);
            bool f = new RollWhileFlyingToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new RollWhileFlyingToggle(true).ToString());
            Assert.AreEqual("False", new RollWhileFlyingToggle(false).ToString());
        }
    }
}

