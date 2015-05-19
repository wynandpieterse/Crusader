namespace GKS.Crusader.Core
{
	using System;

	public class DefaultConnectionListener : IConnectionListener
	{
		public void HandleConnected (IChannel channel)
		{
			return;
		}

		public void HandleDisconnected (IChannel channel)
		{
			return;
		}

		public void HandleException (IChannel channel, Exception exception)
		{
			return;
		}
	}
}

