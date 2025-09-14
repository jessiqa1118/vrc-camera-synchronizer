using NUnit.Framework;
using Parameters;

namespace VRCCamera.Tests.Unit
{
    [TestFixture]
    public class OrientationIsLandscapeToggleUnitTests
    {
        [Test]
        public void Constructor_StoresValue()
        {
            Assert.IsTrue(new OrientationIsLandscapeToggle(true).Value);
            Assert.IsFalse(new OrientationIsLandscapeToggle(false).Value);
        }

        [Test]
        public void Equality_WorksByValue()
        {
            var a = new OrientationIsLandscapeToggle(true);
            var b = new OrientationIsLandscapeToggle(true);
            var c = new OrientationIsLandscapeToggle(false);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test]
        public void ImplicitBool_And_ToString()
        {
            bool t = new OrientationIsLandscapeToggle(true);
            bool f = new OrientationIsLandscapeToggle(false);
            Assert.IsTrue(t);
            Assert.IsFalse(f);
            Assert.AreEqual("True", new OrientationIsLandscapeToggle(true).ToString());
            Assert.AreEqual("False", new OrientationIsLandscapeToggle(false).ToString());
        }
    }
}

