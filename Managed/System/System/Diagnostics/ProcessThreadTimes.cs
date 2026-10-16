using System;

namespace System.Diagnostics
{
	// Token: 0x02000246 RID: 582
	internal class ProcessThreadTimes
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004DCEE File Offset: 0x0004BEEE
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004DCFB File Offset: 0x0004BEFB
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004DD08 File Offset: 0x0004BF08
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004DD15 File Offset: 0x0004BF15
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004DD22 File Offset: 0x0004BF22
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x04000A90 RID: 2704
		internal long create;

		// Token: 0x04000A91 RID: 2705
		internal long exit;

		// Token: 0x04000A92 RID: 2706
		internal long kernel;

		// Token: 0x04000A93 RID: 2707
		internal long user;
	}
}
