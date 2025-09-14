using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class RemotePlayerToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new RemotePlayerToggle(true).Value);
            Assert.IsFalse(new RemotePlayerToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new RemotePlayerToggle(true);
            var b = new RemotePlayerToggle(true);
            var c = new RemotePlayerToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new RemotePlayerToggle(true);
            bool f = new RemotePlayerToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new RemotePlayerToggle(true).ToString());
            Assert.AreEqual("False", new RemotePlayerToggle(false).ToString());
        }
    }
}

