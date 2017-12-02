using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UdpConstantsLibrary;

namespace UdpLibrary
{
    public class UdpReceiver : IUdpReceiver
    {
        private bool _shouldRun;

        public void Start()
        {
            // Don't start if we are already listening
            if (_shouldRun)
                return;

            _shouldRun = true;
            var listenerThread = new Thread(ReceiverThread);
            listenerThread.Start();
        }

        public void Stop()
        {
            _shouldRun = false;
        }

        private void ReceiverThread()
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, UdpConstants.Port);
            var client = new UdpClient(ipEndPoint);

            while (_shouldRun)
            {
                var state = new UdpState
                {
                    IpEndPoint = ipEndPoint,
                    UdpClient = client
                };

                client.BeginReceive(ReceiveCallback, state);
                Thread.Sleep(10);
            }
        }

        private static void ReceiveCallback(IAsyncResult result)
        {
            if (!(result.AsyncState is UdpState state))
                return;

            var endPoint = state.IpEndPoint;
            
            var bytes = state.UdpClient.EndReceive(result, ref endPoint);
            
            // TODO: Do something with the bytes received.
            if (!IdentifyPacket(bytes))
            {
                // TODO: Do something about unknown packets
                Console.WriteLine("Unkown Packet");
                return;
            }

            var startIndex = UdpConstants.IdentifierBytes.Length - 1;
            var data = bytes.ToList().GetRange(startIndex, bytes.Length - 1 - startIndex).ToArray();

            Console.WriteLine(Encoding.ASCII.GetString(data));
        }

        private static bool IdentifyPacket(IEnumerable<byte> bytes)
        {
            try
            {
                var identifierBytes = bytes.Take(UdpConstants.IdentifierBytes.Length).ToList();

                return !UdpConstants.IdentifierBytes.Where((t, i) => identifierBytes[i] != t).Any();
            }
            catch
            {
                return false;
            }
        }
    }
}
