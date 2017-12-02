using System;
using UdpLibrary;

namespace ServerConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var udpReceiver = new UdpReceiver();
            udpReceiver.Start();

            Console.ReadLine();
            udpReceiver.Stop();
        }
    }
}
