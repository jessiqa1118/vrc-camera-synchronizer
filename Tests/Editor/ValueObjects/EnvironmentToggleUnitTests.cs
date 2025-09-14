using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class EnvironmentToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new EnvironmentToggle(true).Value);
            Assert.IsFalse(new EnvironmentToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new EnvironmentToggle(true);
            var b = new EnvironmentToggle(true);
            var c = new EnvironmentToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new EnvironmentToggle(true);
            bool f = new EnvironmentToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new EnvironmentToggle(true).ToString());
            Assert.AreEqual("False", new EnvironmentToggle(false).ToString());
        }
    }
}

