namespace GKS.Crusader.Core.Internal.Server
{
	using System;
	using System.Net;

	public abstract class ServerOptions : Options
	{
		public EndPoint ListenOn { get; set; }

		protected ServerOptions()
			: base()
		{
			return;
		}
	}
}

