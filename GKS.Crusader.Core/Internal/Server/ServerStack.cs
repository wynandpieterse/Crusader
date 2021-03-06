﻿namespace GKS.Crusader.Core.Internal.Server
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Collections;
	using System.Collections.Concurrent;
	using System.Collections.Generic;

	public abstract class ServerStack : NetworkStack, IServerStack
	{
		private bool _disposed = false;
		private ServerOptions _options = null;
		private ConcurrentDictionary<EndPoint, IChannel> _connectedClients = null;

		protected ServerStack (ServerOptions options, ProtocolType protocol)
			: base (options, protocol)
		{
			_disposed = false;
			_options = options;
			_connectedClients = new ConcurrentDictionary<EndPoint, IChannel> ();

			return;
		}

		protected void AddClient(IChannel channel)
		{
			while (!_connectedClients.TryAdd (channel.RemoteAddress, channel)) {
			}

			return;
		}

		protected void RemoveClient(IChannel channel)
		{
			IChannel unused = null;

			while (!_connectedClients.TryRemove (channel.RemoteAddress, out unused)) {
			}

			return;
		}

        public override void HandleChannelConnected(IChannel channel)
        {
            base.HandleChannelConnected(channel);

            AddClient(channel);

            return;
        }

        public override void HandleChannelDisconnected (IChannel channel)
		{
			base.HandleChannelDisconnected (channel);

			RemoveClient (channel);

			return;
		}

        public override void HandleChannelExceptioned(IChannel channel, Exception exception)
        {
            base.HandleChannelExceptioned(channel, exception);

            RemoveClient(channel);

            return;
        }

        protected override void Dispose (bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					var connectedClients = _connectedClients.ToArray ();

					foreach (var client in connectedClients) {
						client.Value.Dispose ();
					}
				}

				_disposed = true;
			}

			base.Dispose (disposing);
			return;
		}
	}
}

