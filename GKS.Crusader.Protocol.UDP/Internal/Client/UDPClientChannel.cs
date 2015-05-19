namespace GKS.Crusader.Protocols.UDP.Internal.Client
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    internal sealed class UDPClientChannel : UDPChannel
    {
        private bool _disposed = false;
        private UDPClientStack _stack = null;
        private UDPClientOptions _options = null;

        public UDPClientChannel(UDPClientStack stack, UDPClientOptions options, Socket socket)
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

