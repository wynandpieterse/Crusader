namespace GKS.Crusader.Protocols.UDP.Internal.Client
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    using GKS;
    using GKS.Crusader;
    using GKS.Crusader.Core;
    using GKS.Crusader.Core.Internal;
    using GKS.Crusader.Core.Internal.Client;

    internal sealed class UDPClientStack : ClientStack, IUDPClientStack
    {
        private bool _disposed = false;
        private UDPClientOptions _options = null;

        public UDPClientStack(UDPClientOptions options)
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

