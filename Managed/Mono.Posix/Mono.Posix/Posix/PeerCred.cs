using System;
using System.Net.Sockets;

namespace Mono.Posix
{
	// Token: 0x02000096 RID: 150
	[Obsolete("Use Mono.Unix.PeerCred")]
	public class PeerCred
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001018C File Offset: 0x0000E38C
		public PeerCred(Socket sock)
		{
			if (sock.AddressFamily != AddressFamily.Unix)
			{
				throw new ArgumentException("Only Unix sockets are supported", "sock");
			}
			this.data = (PeerCredData)sock.GetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)10001);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public int ProcessID
		{
			get
			{
				return this.data.pid;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000101D5 File Offset: 0x0000E3D5
		public int UserID
		{
			get
			{
				return this.data.uid;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000101E2 File Offset: 0x0000E3E2
		public int GroupID
		{
			get
			{
				return this.data.gid;
			}
		}

		// Token: 0x040004F9 RID: 1273
		private const int so_peercred = 10001;

		// Token: 0x040004FA RID: 1274
		private PeerCredData data;
	}
}
