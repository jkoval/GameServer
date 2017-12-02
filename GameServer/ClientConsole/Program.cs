using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UdpConstantsLibrary;

namespace ClientConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var client = new UdpClient();
            var endPoint = new IPEndPoint(UdpConstants.ServerIp, UdpConstants.Port);
            client.Connect(endPoint);

            string text ;

            do
            {
                text = Console.ReadLine();

                if (text == null)
                    continue;

                var bytes = new List<byte>();
                bytes.AddRange(UdpConstants.IdentifierBytes);
                bytes.AddRange(Encoding.ASCII.GetBytes(text));
                client.Send(bytes.ToArray(), bytes.Count);
            } while (text != "exit");
        }
    }
}
