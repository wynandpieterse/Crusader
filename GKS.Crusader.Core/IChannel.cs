namespace GKS.Crusader.Core
{
	using System;
	using System.Net;
	using System.IO;

	public interface IChannel : IDisposable
	{
		EndPoint RemoteAddress { get; set; }
		Object UserData { get; set; }

		void Send(byte[] buffer, int offset, int length);
	}
}

