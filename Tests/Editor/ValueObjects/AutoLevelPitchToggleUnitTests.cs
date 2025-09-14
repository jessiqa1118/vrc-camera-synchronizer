using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class AutoLevelPitchToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new AutoLevelPitchToggle(true).Value);
            Assert.IsFalse(new AutoLevelPitchToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new AutoLevelPitchToggle(true);
            var b = new AutoLevelPitchToggle(true);
            var c = new AutoLevelPitchToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new AutoLevelPitchToggle(true);
            bool f = new AutoLevelPitchToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new AutoLevelPitchToggle(true).ToString());
            Assert.AreEqual("False", new AutoLevelPitchToggle(false).ToString());
        }
    }
}

