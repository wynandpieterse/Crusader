namespace GKS.Crusader.Protocols.UDP.Internal.Multicast
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    using GKS;
    using GKS.Crusader;
    using GKS.Crusader.Core;
    using GKS.Crusader.Core.Internal;
    using GKS.Crusader.Core.Internal.Multicast;

    internal sealed class UDPMulticastStack : MulticastStack, IUDPMulticastStack
    {
        private bool _disposed = false;
        private UDPMulticastOptions _options = null;

        public UDPMulticastStack(UDPMulticastOptions options)
            : base(options, ProtocolType.Udp)
        {
            _disposed = false;
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

