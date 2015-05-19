namespace GKS.Crusader.Core
{
	using System;

	public interface IChannelListener
	{
		void HandleConnected(IChannel channel);
		void HandleDisconnected(IChannel channel);
		void HandleExceptioned(IChannel channel, Exception exception);
	}
}

