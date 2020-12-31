using System;

using Sodao.FastSocket.Server;

namespace TSFCS.SCOP.Udp
{
    public class UdpService : IUdpService<UdpMessage>
    {
        public void OnReceived(Sodao.FastSocket.Server.UdpSession session, UdpMessage message)
        {
            if (message != null)
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<UdpMessage>(message, "Recv");
        }

        public void OnError(Sodao.FastSocket.Server.UdpSession session, Exception ex)
        {
        }
    }
}
