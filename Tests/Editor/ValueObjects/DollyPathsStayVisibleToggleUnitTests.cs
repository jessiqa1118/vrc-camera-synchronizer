using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class DollyPathsStayVisibleToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new DollyPathsStayVisibleToggle(true).Value);
            Assert.IsFalse(new DollyPathsStayVisibleToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new DollyPathsStayVisibleToggle(true);
            var b = new DollyPathsStayVisibleToggle(true);
            var c = new DollyPathsStayVisibleToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new DollyPathsStayVisibleToggle(true);
            bool f = new DollyPathsStayVisibleToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new DollyPathsStayVisibleToggle(true).ToString());
            Assert.AreEqual("False", new DollyPathsStayVisibleToggle(false).ToString());
        }
    }
}

