namespace GKS.Crusader.Protocols.TCP
{
	using System;

	using GKS;
	using GKS.Crusader;
	using GKS.Crusader.Protocols;
	using GKS.Crusader.Protocols.TCP;
	using GKS.Crusader.Protocols.TCP.Internal;
	using GKS.Crusader.Protocols.TCP.Internal.Server;
	using GKS.Crusader.Protocols.TCP.Internal.Client;

	public static class TCPStackFactory
	{
		public static ITCPServerStack Create(TCPServerOptions options)
		{
			return new TCPServerStack(options);
		}

		public static ITCPClientStack Create(TCPClientOptions options)
		{
			return new TCPClientStack(options);
		}
	}
}

