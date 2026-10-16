using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000755 RID: 1877
	internal class Win32UdpStatistics : UdpStatistics
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x000CBFCF File Offset: 0x000CA1CF
		public Win32UdpStatistics(Win32_MIB_UDPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000CBFDE File Offset: 0x000CA1DE
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.info.InDatagrams);
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000CBFEC File Offset: 0x000CA1EC
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.info.OutDatagrams);
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000CBFFA File Offset: 0x000CA1FA
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.NoPorts);
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000CC008 File Offset: 0x000CA208
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.info.InErrors);
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000CC016 File Offset: 0x000CA216
		public override int UdpListeners
		{
			get
			{
				return this.info.NumAddrs;
			}
		}

		// Token: 0x0400237D RID: 9085
		private Win32_MIB_UDPSTATS info;
	}
}
