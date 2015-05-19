namespace GKS.Crusader.Protocols.TCP.Internal
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;

	using GKS;
	using GKS.Crusader;
	using GKS.Crusader.Core;
	using GKS.Crusader.Core.Internal;

    internal abstract class TCPChannel : Channel
    {
        private bool _disposed = false;
        private NetworkStack _stack = null;
        private Options _options = null;

        protected Socket Socket { get; private set; }

        protected TCPChannel(NetworkStack stack, Options options, Socket socket)
            : base(stack, options, ProtocolType.Tcp)
        {
            _stack = stack;
            _options = options;

            RemoteAddress = socket.RemoteEndPoint;

            Socket = socket;

            HandleConnected();

            StartReceiving();

            return;
        }

        private void StartReceiving()
        {
            SocketAsyncEventArgs receiveEvent = AcquireReceiveEvent();

            if (!Socket.ReceiveAsync(receiveEvent)) {
                HandleReceive(Socket, receiveEvent);
            }

            return;
        }

        private void HandleReceive(object sender, SocketAsyncEventArgs e)
        {
            do {
                if (StillConnected(e))
                {
                    _options.PacketListener.Handle(this, ReceiveEventBuffer(e), 0, e.BytesTransferred);

                    ReleaseReceiveEvent(e);
                    e = AcquireReceiveEvent();
                }
                else
                {
                    ReleaseReceiveEvent(e);
                    return;
                }
            } while (!SafeReceive(e));

            return;
        }

        private bool SafeReceive(SocketAsyncEventArgs e)
        {
            bool shouldContinue = false;

            try
            {
                shouldContinue = Socket.ReceiveAsync(e);
            }
            catch (ObjectDisposedException)
            {
                HandleDisconnected();
                shouldContinue = true;
            }

            return shouldContinue;
        }

        private bool StillConnected(SocketAsyncEventArgs e)
        {
            bool connected = true;

            if (_disposed)
            {
                connected = false;
            }

            if (e.SocketError == SocketError.OperationAborted)
            {
                connected = false;
            }

            if (e.BytesTransferred == 0)
            {
                connected = false;
            }

            if(false == connected)
            {
                HandleDisconnected();
            }

            return connected;
        }

		protected override SocketAsyncEventArgs AcquireReceiveEvent ()
		{
			SocketAsyncEventArgs receiveEvent = base.AcquireReceiveEvent ();

			receiveEvent.Completed += HandleReceive;

			return receiveEvent;
		}

		protected override void ReleaseReceiveEvent (SocketAsyncEventArgs receiveEvent)
		{
			receiveEvent.Completed -= HandleReceive;

			base.ReleaseReceiveEvent (receiveEvent);
			return;
		}

		public override void Send (byte[] buffer, int offset, int length)
		{
			if (_disposed) throw new ObjectDisposedException ("TCPChannel");

			Socket.Send (buffer, offset, length, SocketFlags.None);

			return;
		}

		protected override void Dispose (bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					if (Socket != null) {
						Socket.Close ();
						Socket.Dispose ();
					}
				}

				_disposed = true;
			}

			base.Dispose (disposing);
			return;
		}
	}
}

