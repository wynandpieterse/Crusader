using System;
using System.Text;
using System.Net;
using GKS.Crusader.Core;
using GKS.Crusader.Protocols.TCP;

namespace Crusader
{
	internal class ChannelListener : IChannelListener
	{
        private bool _server = false;

        public ChannelListener(bool server)
        {
            _server = server;

            return;
        }

		public void HandleConnected (IChannel channel)
		{
            if (_server)
            {
                Console.WriteLine("Client connected : {0}", channel.RemoteAddress);
            }
            else
            {
                var response = Encoding.ASCII.GetBytes("Hello, World");

                channel.Send(response, 0, response.Length);

                Console.WriteLine("Connected to server at : {0}", channel.RemoteAddress);
            }

			return;
		}

		public void HandleDisconnected (IChannel channel)
		{
            if (_server)
            {
                Console.WriteLine("Client disconnected : {0}", channel.RemoteAddress);
            }
            else
            {
                Console.WriteLine("Disconnected from server at : {0}", channel.RemoteAddress);
            }

            return;
		}

		public void HandleExceptioned (IChannel channel, Exception exception)
		{
            if (_server)
            {
                Console.WriteLine("Client exceptioned : {0} : {1}", channel.RemoteAddress, exception);
            }
            else
            {
                Console.WriteLine("Server exceptioned : {0} : {1}", channel.RemoteAddress, exception);
            }

            return;
		}
	}

	internal class PacketListener : IPacketListener
	{
        private bool _server = false;

        public PacketListener(bool server)
        {
            _server = server;

            return;
        }

		public void Handle (IChannel channel, byte[] buffer, int offset, int length)
		{
            if (_server)
            {
                var message = Encoding.ASCII.GetString(buffer, offset, length);

                Console.WriteLine("New message : {0} : {1}", channel.RemoteAddress, message);

                var response = Encoding.ASCII.GetBytes("Hello, World");

                channel.Send(response, 0, response.Length);

                channel.Dispose();
            }
            else
            {
                var message = Encoding.ASCII.GetString(buffer, offset, length);

                Console.WriteLine("New message from server : {0} : {1}", channel.RemoteAddress, message);
                
                channel.Dispose();
            }

			return;
		}
	}

	internal static class Program
	{
		private static void SetUp()
		{
			Console.Title = "Crusader";
			Console.WindowWidth = 120;
			Console.WindowHeight = 40;

			return;
		}

		private static void Pause()
		{
			bool enterPressed = false;
			ConsoleKeyInfo lastKey = default(ConsoleKeyInfo);

			Console.WriteLine ("Press ENTER to continue...");

			while (!enterPressed) 
			{
				if (Console.KeyAvailable) 
				{
					lastKey = Console.ReadKey (true);

					if (lastKey.Key == ConsoleKey.Enter) 
					{
						enterPressed = true;
					}
				}
			}

			return;
		}

		private static void Main (string[] args)
		{
			TCPServerOptions serverOptions = new TCPServerOptions ();

			serverOptions.ListenOn = new IPEndPoint (IPAddress.Loopback, 80);
			serverOptions.ChannelListener = new ChannelListener(true);
			serverOptions.PacketListener = new PacketListener(true);

            TCPClientOptions clientOptions = new TCPClientOptions();

            clientOptions.ConnectTo = new IPEndPoint(IPAddress.Loopback, 80);
            clientOptions.ChannelListener = new ChannelListener(false);
            clientOptions.PacketListener = new PacketListener(false);

			SetUp ();

			using (var serverStack = TCPStackFactory.Create (serverOptions)) 
			{
                using (var clientStack = TCPStackFactory.Create(clientOptions))
                {
                    Pause();
                }
			}

			return;
		}
	}
}
