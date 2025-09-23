using NUnit.Framework;
using Astearium.Osc;

namespace Astearium.VRChat.Camera.Tests.Unit
{
    [TestFixture]
    public class StreamingToggleOscMessageUnitTests
    {
        [TestCase(true, "T")]
        [TestCase(false, "F")]
        public void Ctor_WithToggle_BuildsOscMessage(bool toggle, string expectedTypeTag)
        {
            var message = new StreamingToggleOscMessage(toggle);

            Assert.AreEqual(OSCCameraEndpoints.Streaming, message.Address);
            Assert.AreEqual(1, message.Arguments.Length);
            Assert.AreEqual(toggle, message.Arguments[0].Value);
            Assert.AreEqual(Argument.ValueType.Bool, message.Arguments[0].Type);
            Assert.AreEqual(expectedTypeTag, message.TypeTag.Value);
        }
    }
}
