using System.Net;

namespace UdpConstantsLibrary
{
    public class UdpConstants
    {
        // TODO: Point this to the server's static IP
        public static readonly IPAddress ServerIp = IPAddress.Parse("127.0.0.1");

        // TODO: Find a suitable port for the server
        public const int Port = 1777;

        public static readonly byte[] IdentifierBytes = { 55, 4, 3, 2 };
    }
}
