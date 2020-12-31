using Sodao.FastSocket.Server.Messaging;

namespace TSFCS.SCOP.Udp
{
    public class UdpMessage : IMessage
    {
        public byte[] Payload { get; set; }
        public int Length { get; set; }

        public UdpMessage()
        { 
        }

        public UdpMessage(byte[] payload, int length)
        {
            this.Payload = payload;
            this.Length = length;
        }
    }
}
