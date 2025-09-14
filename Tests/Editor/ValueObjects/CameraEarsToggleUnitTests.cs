using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class CameraEarsToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new CameraEarsToggle(true).Value);
            Assert.IsFalse(new CameraEarsToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new CameraEarsToggle(true);
            var b = new CameraEarsToggle(true);
            var c = new CameraEarsToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new CameraEarsToggle(true);
            bool f = new CameraEarsToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new CameraEarsToggle(true).ToString());
            Assert.AreEqual("False", new CameraEarsToggle(false).ToString());
        }
    }
}

