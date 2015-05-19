namespace GKS.Crusader.Protocols.TCP.Internal.Client
{
	using System;
	using System.Net;
	using System.Net.Sockets;

	using GKS;
	using GKS.Crusader;
	using GKS.Crusader.Core;
	using GKS.Crusader.Core.Internal;
	using GKS.Crusader.Core.Internal.Client;

	internal sealed class TCPClientStack : ClientStack, ITCPClientStack
	{
		private bool _disposed = false;
		private TCPClientOptions _options = null;
		private Socket _serverSocket = null;
		private TCPClientChannel _serverChannel = null;

		public TCPClientStack (TCPClientOptions options)
			: base(options, ProtocolType.Tcp)
		{
			_options = options;

			CreateServerSocket ();
			ConnectToServer ();

			return;
		}

		private void CreateServerSocket()
		{
			_serverSocket = new Socket (_options.ConnectTo.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			_serverSocket.Bind (new IPEndPoint (IPAddress.Any, 0));

			return;
		}

		private void ConnectToServer()
		{
			SocketAsyncEventArgs connectionEvent = new SocketAsyncEventArgs ();

			connectionEvent.Completed += HandleConnected;
			connectionEvent.RemoteEndPoint = _options.ConnectTo;

			if (!_serverSocket.ConnectAsync (connectionEvent)) {
				HandleConnected (_serverSocket, connectionEvent);
			}

			return;
		}

		private void HandleConnected(object sender, SocketAsyncEventArgs e)
		{
			_serverChannel = new TCPClientChannel (this, _options, _serverSocket);

			_options.ConnectionListener.HandleConnected (_serverChannel);

			return;
		}

		public override void HandleChannelClosed (IChannel channel)
		{
			base.HandleChannelClosed (channel);
			return;
		}

		protected override void Dispose (bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					if (null != _serverSocket) {
						_serverSocket.Close ();
						_serverSocket.Dispose ();
					}
				}

				_disposed = true;
			}

			base.Dispose (disposing);
			return;
		}
	}
}

