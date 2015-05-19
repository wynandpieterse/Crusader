namespace GKS.Crusader.Protocols.UDP.Internal
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    using GKS;
    using GKS.Crusader;
    using GKS.Crusader.Core;
    using GKS.Crusader.Core.Internal;

    internal abstract class UDPChannel : Channel
    {
        private bool _disposed = false;
        private NetworkStack _stack = null;
        private Options _options = null;

        protected UDPChannel(NetworkStack stack, Options options, Socket socket)
            : base(stack, options, ProtocolType.Tcp)
        {
            _stack = stack;
            _options = options;

            RemoteAddress = socket.RemoteEndPoint;

            return;
        }

        public override void Send(byte[] buffer, int offset, int length)
        {
            return;
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                _disposed = true;
            }

            base.Dispose(disposing);
            return;
        }
    }
}

