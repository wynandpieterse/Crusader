namespace GKS.Crusader.Protocols.UDP.Internal.Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    using GKS;
    using GKS.Crusader;
    using GKS.Crusader.Core;
    using GKS.Crusader.Core.Internal;
    using GKS.Crusader.Core.Internal.Server;

    internal sealed class UDPServerStack : ServerStack, IUDPServerStack
    {
        private bool _disposed = false;
        private UDPServerOptions _options = null;

        public UDPServerStack(UDPServerOptions options)
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

