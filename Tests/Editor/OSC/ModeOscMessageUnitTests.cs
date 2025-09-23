using NUnit.Framework;
using Astearium.Network.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class ModeOscMessageUnitTests
    {
        [Test]
        public void Constructor_FromMode_WritesIntArgument()
        {
            var message = new ModeOscMessage(Mode.Stream);

            Assert.AreEqual(OSCCameraEndpoints.Mode.Value, message.Address.Value);
            Assert.AreEqual("i", message.TypeTag.Value);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(Argument.ValueType.Int32, message.Arguments[0].Type);
            Assert.AreEqual(2, message.Arguments[0].AsInt32());
        }

    }
}
