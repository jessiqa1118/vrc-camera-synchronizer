using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class TriggerTakesPhotosToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new TriggerTakesPhotosToggle(true).Value);
            Assert.IsFalse(new TriggerTakesPhotosToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new TriggerTakesPhotosToggle(true);
            var b = new TriggerTakesPhotosToggle(true);
            var c = new TriggerTakesPhotosToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new TriggerTakesPhotosToggle(true);
            bool f = new TriggerTakesPhotosToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new TriggerTakesPhotosToggle(true).ToString());
            Assert.AreEqual("False", new TriggerTakesPhotosToggle(false).ToString());
        }
    }
}

