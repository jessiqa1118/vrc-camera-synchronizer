using OSC;
using Parameters;

namespace VRCCamera
{
    public interface IOSCMessageConverter<T>
    {
        public T FromOSCMessage(Message message);
        public Message ToOSCMessage(T value);
    }
}