using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class ShowFocusToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new ShowFocusToggle(true).Value);
            Assert.IsFalse(new ShowFocusToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new ShowFocusToggle(true);
            var b = new ShowFocusToggle(true);
            var c = new ShowFocusToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new ShowFocusToggle(true);
            bool f = new ShowFocusToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new ShowFocusToggle(true).ToString());
            Assert.AreEqual("False", new ShowFocusToggle(false).ToString());
        }
    }
}

