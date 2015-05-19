namespace GKS.Crusader.Protocols.TCP.Internal.Server
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.Concurrent;

	using GKS;
	using GKS.Crusader;
	using GKS.Crusader.Core;
	using GKS.Crusader.Core.Internal;
	using GKS.Crusader.Core.Internal.Server;

	internal sealed class TCPServerStack : ServerStack, ITCPServerStack
	{
		private bool _disposed = false;
		private TCPServerOptions _options = null;
		private Socket _listenSocket = null;
		private ConcurrentBag<SocketAsyncEventArgs> _acceptEvents = null;

		public TCPServerStack (TCPServerOptions options)
			: base(options, ProtocolType.Tcp)
		{
			_disposed = false;
			_options = options;
			_acceptEvents = new ConcurrentBag<SocketAsyncEventArgs> ();

			CreateListenSocket ();
			StartAccept ();

			return;
		}

		private void CreateListenSocket()
		{
			_listenSocket = new Socket (_options.ListenOn.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			_listenSocket.Bind (_options.ListenOn);
			_listenSocket.Listen (128);

			return;
		}

		private void StartAccept()
		{
			SocketAsyncEventArgs acceptEvent = AcquireAcceptEvent ();

			if (!_listenSocket.AcceptAsync (acceptEvent)) {
				HandleAccept (_listenSocket, acceptEvent);
			}

			return;
		}

		private void HandleAccept(object sender, SocketAsyncEventArgs e)
		{
			TCPServerChannel channel = null;

			if (StackRunning.IsCancellationRequested) {
				return;
			} else if (e.SocketError == SocketError.OperationAborted) {
				return;
			} else {
				StartAccept ();

				channel = new TCPServerChannel (this, _options, e.AcceptSocket);
				AddClient (channel);
			}

			return;
		}

		private SocketAsyncEventArgs AcquireAcceptEvent()
		{
			SocketAsyncEventArgs acceptEvent = null;

			if (!_acceptEvents.TryTake (out acceptEvent)) {
				acceptEvent = new SocketAsyncEventArgs ();
			}

			acceptEvent.Completed += HandleAccept;

			return acceptEvent;
		}

		private void ReleaseAcceptEvent(SocketAsyncEventArgs acceptEvent)
		{
			acceptEvent.Completed -= HandleAccept;
			acceptEvent.AcceptSocket = null;

			_acceptEvents.Add (acceptEvent);

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

				}

				_disposed = true;
			}

			base.Dispose (disposing);
			return;
		}
	}
}

