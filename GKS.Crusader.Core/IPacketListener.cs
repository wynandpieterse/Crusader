namespace GKS.Crusader.Core
{
	using System;

	public interface IPacketListener
	{
		void Handle(IChannel channel, byte[] buffer, int offset, int length);
	}
}

