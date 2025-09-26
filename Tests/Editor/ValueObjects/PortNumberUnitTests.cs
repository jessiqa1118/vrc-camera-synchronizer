using NUnit.Framework;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class PortNumberUnitTests
    {
        [Test]
        public void Constructor_WithinRange_SetsValue()
        {
            var port = new PortNumber(7500);
            Assert.AreEqual(7500, port.Value);
        }

        [Test]
        public void Constructor_BelowMin_Throws()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new PortNumber(0));
        }

        [Test]
        public void Constructor_AboveMax_Throws()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new PortNumber(PortNumber.MaxValue + 1));
        }

        [Test]
        public void ImplicitConversion_ReturnsUnderlyingValue()
        {
            PortNumber port = new PortNumber(12345);
            int value = port;
            Assert.AreEqual(12345, value);
        }
    }
}
