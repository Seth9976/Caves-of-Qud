using System;
using System.Net.Sockets;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x02000279 RID: 633
	internal sealed class SocketProxy : ISocket, IDisposable
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x00079315 File Offset: 0x00077515
		internal SocketProxy(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.socket = new Socket(addressFamily, socketType, protocolType);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0007932B File Offset: 0x0007752B
		public Socket UnderlyingSocket
		{
			get
			{
				return this.socket;
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00079333 File Offset: 0x00077533
		public void Close()
		{
			this.socket.Close();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00079340 File Offset: 0x00077540
		public bool ConnectAsync(SocketAsyncEventArgs args)
		{
			return this.socket.ConnectAsync(args);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0007934E File Offset: 0x0007754E
		public bool SendAsync(SocketAsyncEventArgs args)
		{
			return this.socket.SendAsync(args);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0007935C File Offset: 0x0007755C
		public bool SendToAsync(SocketAsyncEventArgs args)
		{
			return this.socket.SendToAsync(args);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0007936A File Offset: 0x0007756A
		public void Dispose()
		{
			((IDisposable)this.socket).Dispose();
		}

		// Token: 0x04000DE7 RID: 3559
		private readonly Socket socket;
	}
}
