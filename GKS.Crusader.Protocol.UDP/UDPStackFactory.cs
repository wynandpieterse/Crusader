namespace GKS.Crusader.Protocols.UDP
{
    using System;

    using GKS;
    using GKS.Crusader;
    using GKS.Crusader.Protocols;
    using GKS.Crusader.Protocols.UDP;
    using GKS.Crusader.Protocols.UDP.Internal;
    using GKS.Crusader.Protocols.UDP.Internal.Client;
    using GKS.Crusader.Protocols.UDP.Internal.Multicast;
    using GKS.Crusader.Protocols.UDP.Internal.Server;

    public static class UDPStackFactory
    {
        public static IUDPClientStack Create(UDPClientOptions options)
        {
            return new UDPClientStack(options);
        }

        public static IUDPMulticastStack Create(UDPMulticastOptions options)
        {
            return new UDPMulticastStack(options);
        }

        public static IUDPServerStack Create(UDPServerOptions options)
        {
            return new UDPServerStack(options);
        }
    }
}
