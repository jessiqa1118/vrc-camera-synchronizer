using System;
using OSC;

namespace VRCCamera
{
    public interface IOSCTransmitter : IDisposable
    {
        public void Send(Message message);
    }
}