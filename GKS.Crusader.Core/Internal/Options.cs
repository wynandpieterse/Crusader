namespace GKS.Crusader.Core.Internal
{
	using System;

	public abstract class Options
	{
		public IConnectionListener ConnectionListener { get; set; }
		public IPacketListener RawPacketListener { get; set; }

		protected Options()
		{
			ConnectionListener = new DefaultConnectionListener ();
			RawPacketListener = new DefaultPacketListener ();

			return;
		}
	}
}

