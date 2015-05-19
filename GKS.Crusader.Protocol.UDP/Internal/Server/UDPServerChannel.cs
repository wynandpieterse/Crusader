namespace GKS.Crusader.Protocols.UDP.Internal.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    internal sealed class UDPServerChannel : UDPChannel
    {
        private bool _disposed = false;
        private UDPServerStack _stack = null;
        private UDPServerOptions _options = null;

        public UDPServerChannel(UDPServerStack stack, UDPServerOptions options, Socket socket)
            : base(stack, options, socket)
        {
            _stack = stack;
            _options = options;

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

