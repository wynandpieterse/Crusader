namespace GKS.Crusader.Core.Internal.Multicast
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    public abstract class MulticastStack : NetworkStack, IMulticastStack
    {
        private bool _disposed = false;
        private MulticastOptions _options = null;

        protected MulticastStack(MulticastOptions options, ProtocolType protocol)
            : base(options, protocol)
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

