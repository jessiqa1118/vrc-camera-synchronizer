using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class AutoLevelRollToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new AutoLevelRollToggle(true).Value);
            Assert.IsFalse(new AutoLevelRollToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new AutoLevelRollToggle(true);
            var b = new AutoLevelRollToggle(true);
            var c = new AutoLevelRollToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new AutoLevelRollToggle(true);
            bool f = new AutoLevelRollToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new AutoLevelRollToggle(true).ToString());
            Assert.AreEqual("False", new AutoLevelRollToggle(false).ToString());
        }
    }
}

