namespace GKS.Crusader.Core.Internal
{
	using System;

	public abstract class Options
	{
		public IChannelListener ChannelListener { get; set; }
		public IPacketListener PacketListener { get; set; }

		protected Options()
		{
			ChannelListener = new DefaultChannelListener ();
			PacketListener = new DefaultPacketListener ();

			return;
		}
	}
}

