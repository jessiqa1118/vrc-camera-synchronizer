using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class StreamingToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new StreamingToggle(true).Value);
            Assert.IsFalse(new StreamingToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new StreamingToggle(true);
            var b = new StreamingToggle(true);
            var c = new StreamingToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new StreamingToggle(true);
            bool f = new StreamingToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new StreamingToggle(true).ToString());
            Assert.AreEqual("False", new StreamingToggle(false).ToString());
        }
    }
}

