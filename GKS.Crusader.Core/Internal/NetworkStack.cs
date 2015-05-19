namespace GKS.Crusader.Core.Internal
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections;
	using System.Collections.Concurrent;

	using Microsoft;
	using Microsoft.IO;

	public abstract class NetworkStack : INetworkStack
	{
		private bool _disposed = false;
		private Options _options = null;
		private CancellationTokenSource _stackRunning = null;

		public ProtocolType Protocol { get; private set; }
		public CancellationToken StackRunning { get { return _stackRunning.Token; } }
		public RecyclableMemoryStreamManager ReceiveStreamPool { get; private set; }
		public ConcurrentBag<SocketAsyncEventArgs> ReceiveEventPool { get; private set; }

		protected NetworkStack(Options options, ProtocolType protocol)
		{
			_disposed = false;
			_options = options;
			_stackRunning = new CancellationTokenSource ();

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

		public virtual void HandleChannelClosed(IChannel channel)
		{
			_options.ConnectionListener.HandleDisconnected (channel);

			return;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					if (null != _stackRunning) {
						_stackRunning.Cancel ();
						_stackRunning.Dispose ();
					}
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

