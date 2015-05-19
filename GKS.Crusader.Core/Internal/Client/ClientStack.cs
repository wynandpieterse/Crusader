namespace GKS.Crusader.Core.Internal.Client
{
	using System;
	using System.Net;
	using System.Net.Sockets;

	public abstract class ClientStack : NetworkStack, IClientStack
	{
		private bool _disposed = false;
		private ClientOptions _options = null;

		protected ClientStack(ClientOptions options, ProtocolType protocol)
			: base(options, protocol)
		{
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

