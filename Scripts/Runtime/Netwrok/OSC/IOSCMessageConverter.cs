namespace Astearium.Network.Osc
{
    public interface IOSCMessageConverter<T>
    {
        public T FromOSCMessage(Message message);
        public Message ToOSCMessage(T value);
    }
}
