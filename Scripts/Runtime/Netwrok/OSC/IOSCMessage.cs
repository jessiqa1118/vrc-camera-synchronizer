namespace Astearium.Network.Osc
{
    public interface IOSCMessage
    {
        public Address Address { get; }
        public Argument[] Arguments { get; }
        public TypeTag TypeTag { get; }
    }
}
