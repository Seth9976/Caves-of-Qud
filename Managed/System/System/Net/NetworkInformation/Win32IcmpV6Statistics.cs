using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000739 RID: 1849
	internal class Win32IcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06003AEB RID: 15083 RVA: 0x000CB728 File Offset: 0x000C9928
		public Win32IcmpV6Statistics(Win32_MIB_ICMP_EX info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000CB748 File Offset: 0x000C9948
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[1]);
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000CB758 File Offset: 0x000C9958
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[1]);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000CB768 File Offset: 0x000C9968
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[129]);
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000CB77C File Offset: 0x000C997C
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[129]);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000CB790 File Offset: 0x000C9990
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[128]);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000CB7A4 File Offset: 0x000C99A4
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[128]);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000CB7B8 File Offset: 0x000C99B8
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000CB7C6 File Offset: 0x000C99C6
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000CB7D4 File Offset: 0x000C99D4
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[130]);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000CB7E8 File Offset: 0x000C99E8
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[130]);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000CB7FC File Offset: 0x000C99FC
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[132]);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000CB810 File Offset: 0x000C9A10
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[132]);
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000CB824 File Offset: 0x000C9A24
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[131]);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000CB838 File Offset: 0x000C9A38
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[131]);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000CB84C File Offset: 0x000C9A4C
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000CB85A File Offset: 0x000C9A5A
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000CB868 File Offset: 0x000C9A68
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[136]);
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000CB87C File Offset: 0x000C9A7C
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[136]);
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000CB890 File Offset: 0x000C9A90
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[135]);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000CB8A4 File Offset: 0x000C9AA4
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[135]);
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000CB8B8 File Offset: 0x000C9AB8
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[2]);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000CB8C8 File Offset: 0x000C9AC8
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[2]);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000CB8D8 File Offset: 0x000C9AD8
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[4]);
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000CB8E8 File Offset: 0x000C9AE8
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[4]);
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000CB8F8 File Offset: 0x000C9AF8
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[137]);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000CB90C File Offset: 0x000C9B0C
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[137]);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000CB920 File Offset: 0x000C9B20
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[134]);
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000CB934 File Offset: 0x000C9B34
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[134]);
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000CB948 File Offset: 0x000C9B48
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[133]);
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000CB95C File Offset: 0x000C9B5C
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[133]);
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x000CB970 File Offset: 0x000C9B70
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[3]);
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000CB980 File Offset: 0x000C9B80
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[3]);
			}
		}

		// Token: 0x040022C1 RID: 8897
		private Win32_MIBICMPSTATS_EX iin;

		// Token: 0x040022C2 RID: 8898
		private Win32_MIBICMPSTATS_EX iout;
	}
}
