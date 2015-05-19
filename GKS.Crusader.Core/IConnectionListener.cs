namespace GKS.Crusader.Core
{
	using System;

	public interface IConnectionListener
	{
		void HandleConnected(IChannel channel);
		void HandleDisconnected(IChannel channel);
		void HandleException(IChannel channel, Exception exception);
	}
}

