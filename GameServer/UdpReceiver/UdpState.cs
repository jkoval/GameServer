using System.Net;
using System.Net.Sockets;

namespace UdpLibrary
{
    public class UdpState
    {
        public UdpClient UdpClient { get; set; }
        public IPEndPoint IpEndPoint { get; set; }
    }
}
