using System;

namespace Astearium.Network.Osc
{
    public interface IOSCTransmitter : IDisposable
    {
        public void Send(IOSCMessage message);
    }
}
