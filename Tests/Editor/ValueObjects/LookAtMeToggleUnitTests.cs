using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class LookAtMeToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new LookAtMeToggle(true).Value);
            Assert.IsFalse(new LookAtMeToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new LookAtMeToggle(true);
            var b = new LookAtMeToggle(true);
            var c = new LookAtMeToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new LookAtMeToggle(true);
            bool f = new LookAtMeToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new LookAtMeToggle(true).ToString());
            Assert.AreEqual("False", new LookAtMeToggle(false).ToString());
        }
    }
}

