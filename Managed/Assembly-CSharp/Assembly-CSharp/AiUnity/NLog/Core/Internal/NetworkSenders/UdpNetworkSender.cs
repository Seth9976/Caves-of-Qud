using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x0200027B RID: 635
	internal class UdpNetworkSender : NetworkSender
	{
		// Token: 0x060016B9 RID: 5817 RVA: 0x00079780 File Offset: 0x00077980
		public UdpNetworkSender(string url, AddressFamily addressFamily)
			: base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x00079790 File Offset: 0x00077990
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x00079798 File Offset: 0x00077998
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x060016BC RID: 5820 RVA: 0x000797A4 File Offset: 0x000779A4
		protected internal virtual ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			SocketProxy socketProxy = new SocketProxy(addressFamily, socketType, protocolType);
			Uri uri;
			if (Uri.TryCreate(base.Address, UriKind.Absolute, out uri) && uri.Host.Equals(IPAddress.Broadcast.ToString(), StringComparison.InvariantCultureIgnoreCase))
			{
				socketProxy.UnderlyingSocket.EnableBroadcast = true;
			}
			return socketProxy;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000797EF File Offset: 0x000779EF
		protected override void DoInitialize()
		{
			this.endpoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			this.socket = this.CreateSocket(this.endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00079828 File Offset: 0x00077A28
		protected override void DoClose(AsyncContinuation continuation)
		{
			lock (this)
			{
				try
				{
					if (this.socket != null)
					{
						this.socket.Close();
					}
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
				this.socket = null;
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00079890 File Offset: 0x00077A90
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			lock (this)
			{
				SocketAsyncEventArgs e = new SocketAsyncEventArgs();
				e.SetBuffer(bytes, offset, length);
				e.UserToken = asyncContinuation;
				e.Completed += this.SocketOperationCompleted;
				e.RemoteEndPoint = this.endpoint;
				if (!this.socket.SendToAsync(e))
				{
					this.SocketOperationCompleted(this.socket, e);
				}
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00079918 File Offset: 0x00077B18
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
			Exception ex = null;
			if (e.SocketError != SocketError.Success)
			{
				ex = new IOException("Error: " + e.SocketError.ToString());
			}
			e.Dispose();
			if (asyncContinuation != null)
			{
				asyncContinuation(ex);
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0007996F File Offset: 0x00077B6F
		public override void CheckSocket()
		{
			if (this.socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x04000DF0 RID: 3568
		private ISocket socket;

		// Token: 0x04000DF1 RID: 3569
		private EndPoint endpoint;
	}
}
