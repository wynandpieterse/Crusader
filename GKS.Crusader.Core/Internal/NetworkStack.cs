namespace GKS.Crusader.Core.Internal
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Collections;
	using System.Collections.Concurrent;

	using Microsoft;
	using Microsoft.IO;

	public abstract class NetworkStack : INetworkStack
	{
		private bool _disposed = false;
		private Options _options = null;

        public ProtocolType Protocol { get; private set; }
		public RecyclableMemoryStreamManager ReceiveStreamPool { get; private set; }
		public ConcurrentBag<SocketAsyncEventArgs> ReceiveEventPool { get; private set; }

		protected NetworkStack(Options options, ProtocolType protocol)
		{
			_disposed = false;
			_options = options;

			Protocol = protocol;

			SetUpReceiveStreamPool ();
			SetUpReceiveEventPool ();

			return;
		}

		private void SetUpReceiveStreamPool()
		{
			ReceiveStreamPool = new RecyclableMemoryStreamManager ();

			return;
		}

		private void SetUpReceiveEventPool()
		{
			ReceiveEventPool = new ConcurrentBag<SocketAsyncEventArgs> ();

			return;
		}

        public virtual void HandleChannelConnected(IChannel channel)
        {
            _options.ChannelListener.HandleConnected(channel);

            return;
        }

		public virtual void HandleChannelDisconnected(IChannel channel)
		{
            _options.ChannelListener.HandleDisconnected(channel);

			return;
		}

        public virtual void HandleChannelExceptioned(IChannel channel, Exception exception)
        {
            _options.ChannelListener.HandleExceptioned(channel, exception);

            return;
        }

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					
				}

				_disposed = true;
			}

			return;
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);

			return;
		}
	}
}

