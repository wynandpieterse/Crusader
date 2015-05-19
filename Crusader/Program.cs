using System;
using System.Text;
using System.Net;
using GKS.Crusader.Core;
using GKS.Crusader.Protocols.TCP;

namespace Crusader
{
	internal class ConnectionListener : IConnectionListener
	{
		public void HandleConnected (IChannel channel)
		{
			Console.WriteLine ("Client connected : {0}", channel.RemoteAddress);
			return;
		}

		public void HandleDisconnected (IChannel channel)
		{
			Console.WriteLine ("Client disconnected : {0}", channel.RemoteAddress);
			return;
		}

		public void HandleException (IChannel channel, Exception exception)
		{
			Console.WriteLine ("Client exceptioned : {0} : {1}", channel.RemoteAddress, exception);
			return;
		}
	}

	internal class RawPacketListener : IPacketListener
	{
		public void Handle (IChannel channel, byte[] buffer, int offset, int length)
		{
			string message = Encoding.ASCII.GetString (buffer, offset, length);

			Console.WriteLine ("New message : {0} : {1}", channel.RemoteAddress, message);

			//channel.Dispose ();

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
			TCPServerOptions options = new TCPServerOptions ();

			options.ListenOn = new IPEndPoint (IPAddress.Loopback, 2048);
			options.ConnectionListener = new ConnectionListener();
			options.RawPacketListener = new RawPacketListener();

			SetUp ();

			using (var stack = TCPStackFactory.Create (options)) 
			{
				Pause ();
			}

			return;
		}
	}
}
