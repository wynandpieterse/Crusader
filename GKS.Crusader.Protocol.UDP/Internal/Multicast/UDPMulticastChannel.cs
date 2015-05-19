namespace GKS.Crusader.Protocols.UDP.Internal.Multicast
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    internal sealed class UDPMulticastChannel : UDPChannel
    {
        private bool _disposed = false;
        private UDPMulticastStack _stack = null;
        private UDPMulticastOptions _options = null;

        public UDPMulticastChannel(UDPMulticastStack stack, UDPMulticastOptions options, Socket socket)
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

