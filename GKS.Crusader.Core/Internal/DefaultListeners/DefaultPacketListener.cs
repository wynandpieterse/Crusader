namespace GKS.Crusader.Core
{
	using System;

	public class DefaultPacketListener : IPacketListener
	{
		public void Handle (IChannel channel, byte[] buffer, int offset, int length)
		{
			return;
		}
	}
}

