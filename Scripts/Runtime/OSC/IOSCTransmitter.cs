using System;

namespace Astearium.Osc
{
    public interface IOSCTransmitter : IDisposable
    {
        public void Send(IOSCMessage message);
    }
}