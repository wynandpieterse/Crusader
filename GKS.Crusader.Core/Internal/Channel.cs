namespace GKS.Crusader.Core.Internal
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Threading;
	using System.Threading.Tasks;

	public abstract class Channel : IChannel
	{
		private bool _disposed = false;
		private NetworkStack _stack = null;
		private Options _options = null;
		private CancellationTokenSource _channelRunning = null;
		private CancellationTokenSource _systemRunning = null;

		public EndPoint RemoteAddress { get; set; }
		public Object UserData { get; set; }

		public ProtocolType Protocol { get; internal set; }
		public CancellationToken ChannelRunning { get { return _channelRunning.Token; } }
		public CancellationToken SystemRunning { get { return _systemRunning.Token; } }

		protected Channel(NetworkStack stack, Options options, ProtocolType protocol)
		{
			_disposed = false;
			_stack = stack;
			_options = options;
			_channelRunning = new CancellationTokenSource ();
			_systemRunning = CancellationTokenSource.CreateLinkedTokenSource (stack.StackRunning, ChannelRunning);

			Protocol = protocol;

			return;
		}

		protected virtual SocketAsyncEventArgs AcquireReceiveEvent()
		{
			SocketAsyncEventArgs receiveEvent = null;
			MemoryStream receiveStream = null;

			if(!_stack.ReceiveEventPool.TryTake(out receiveEvent))
			{
				receiveEvent = new SocketAsyncEventArgs ();
			}

			receiveStream = AcquireReceiveStream ();

			receiveEvent.UserToken = receiveStream;
			receiveEvent.SetBuffer (receiveStream.GetBuffer (), 0, receiveStream.Capacity);

			return receiveEvent;
		}

		protected virtual void ReleaseReceiveEvent(SocketAsyncEventArgs receiveEvent)
		{
			MemoryStream receiveStream = null;

			receiveStream = receiveEvent.UserToken as MemoryStream;
			receiveStream.Dispose ();

			receiveEvent.UserToken = null;

			_stack.ReceiveEventPool.Add (receiveEvent);

			return;
		}

		protected byte[] ReceiveEventBuffer(SocketAsyncEventArgs receiveEvent)
		{
			MemoryStream receiveStream = receiveEvent.UserToken as MemoryStream;

			return receiveStream.GetBuffer ();
		}

		private MemoryStream AcquireReceiveStream()
		{
			return _stack.ReceiveStreamPool.GetStream ();
		}

		private void ReleaseReceiveStream(MemoryStream stream)
		{
			stream.Dispose ();

			return;
		}

		protected virtual void HandleClosed()
		{
			_stack.HandleChannelClosed (this);

			return;
		}

		protected void HandleException(Exception exception)
		{
			_options.ConnectionListener.HandleException (this, exception);

			HandleClosed ();

			return;
		}

		public abstract void Send (byte[] buffer, int offset, int length);
			
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					if (null != _channelRunning) {
						_channelRunning.Cancel ();
						_channelRunning.Dispose ();
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

