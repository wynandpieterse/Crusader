namespace GKS.Crusader.Protocols.TCP.Internal.Server
{
	using System;
	using System.Net;
	using System.Net.Sockets;

	internal sealed class TCPServerChannel : TCPChannel
	{
		private bool _disposed = false;
		private TCPServerStack _stack = null;
		private TCPServerOptions _options = null;

		public TCPServerChannel(TCPServerStack stack, TCPServerOptions options, Socket socket)
			: base(stack, options, socket)
		{
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

