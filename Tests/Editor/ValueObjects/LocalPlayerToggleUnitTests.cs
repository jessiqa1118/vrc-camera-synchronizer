using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class LocalPlayerToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new LocalPlayerToggle(true).Value);
            Assert.IsFalse(new LocalPlayerToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new LocalPlayerToggle(true);
            var b = new LocalPlayerToggle(true);
            var c = new LocalPlayerToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new LocalPlayerToggle(true);
            bool f = new LocalPlayerToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new LocalPlayerToggle(true).ToString());
            Assert.AreEqual("False", new LocalPlayerToggle(false).ToString());
        }
    }
}

