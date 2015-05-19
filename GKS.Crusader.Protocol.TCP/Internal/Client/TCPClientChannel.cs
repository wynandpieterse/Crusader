namespace GKS.Crusader.Protocols.TCP.Internal.Client
{
	using System;
	using System.Net;
	using System.Net.Sockets;

	internal sealed class TCPClientChannel : TCPChannel
	{
		private bool _disposed;
		private TCPClientStack _stack;
		private TCPClientOptions _options;

		public TCPClientChannel(TCPClientStack stack, TCPClientOptions options, Socket socket)
			: base(stack, options, socket)
		{
			_disposed = false;
			_stack = stack;
			_options = options;

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

