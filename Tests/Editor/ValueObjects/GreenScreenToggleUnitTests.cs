using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class GreenScreenToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new GreenScreenToggle(true).Value);
            Assert.IsFalse(new GreenScreenToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new GreenScreenToggle(true);
            var b = new GreenScreenToggle(true);
            var c = new GreenScreenToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new GreenScreenToggle(true);
            bool f = new GreenScreenToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new GreenScreenToggle(true).ToString());
            Assert.AreEqual("False", new GreenScreenToggle(false).ToString());
        }
    }
}

