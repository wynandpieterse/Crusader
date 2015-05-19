namespace GKS.Crusader.Core
{
	using System;

	public class DefaultChannelListener : IChannelListener
	{
		public void HandleConnected (IChannel channel)
		{
			return;
		}

		public void HandleDisconnected (IChannel channel)
		{
			return;
		}

		public void HandleExceptioned (IChannel channel, Exception exception)
		{
			return;
		}
	}
}

