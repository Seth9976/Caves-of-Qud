using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000707 RID: 1799
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x0600399C RID: 14748 RVA: 0x000C8E54 File Offset: 0x000C7054
		public SystemTcpConnectionInformation(IPEndPoint local, IPEndPoint remote, TcpState state)
		{
			this.localEndPoint = local;
			this.remoteEndPoint = remote;
			this.state = state;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000C8E71 File Offset: 0x000C7071
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000C8E79 File Offset: 0x000C7079
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x000C8E81 File Offset: 0x000C7081
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x040021ED RID: 8685
		private IPEndPoint localEndPoint;

		// Token: 0x040021EE RID: 8686
		private IPEndPoint remoteEndPoint;

		// Token: 0x040021EF RID: 8687
		private TcpState state;
	}
}
