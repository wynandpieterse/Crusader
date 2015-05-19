namespace GKS.Crusader.Core.Internal.Client
{
	using System;
	using System.Net;

	public abstract class ClientOptions : Options
	{
		public EndPoint ConnectTo { get; set; }

		protected ClientOptions()
			: base()
		{
			return;
		}
	}
}

