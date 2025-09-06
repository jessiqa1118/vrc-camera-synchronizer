using System;

namespace JessiQa
{
    public interface IOSCTransmitter : IDisposable
    {
        public void Send(Message message);
    }
}