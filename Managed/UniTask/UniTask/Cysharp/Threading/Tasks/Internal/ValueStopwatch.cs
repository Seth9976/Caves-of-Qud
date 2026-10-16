using System;
using System.Diagnostics;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200011A RID: 282
	internal readonly struct ValueStopwatch
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0000EE3E File Offset: 0x0000D03E
		public static ValueStopwatch StartNew()
		{
			return new ValueStopwatch(Stopwatch.GetTimestamp());
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000EE4A File Offset: 0x0000D04A
		private ValueStopwatch(long startTimestamp)
		{
			this.startTimestamp = startTimestamp;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000EE53 File Offset: 0x0000D053
		public TimeSpan Elapsed
		{
			get
			{
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000EE60 File Offset: 0x0000D060
		public bool IsInvalid
		{
			get
			{
				return this.startTimestamp == 0L;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public long ElapsedTicks
		{
			get
			{
				if (this.startTimestamp == 0L)
				{
					throw new InvalidOperationException("Detected invalid initialization(use 'default'), only to create from StartNew().");
				}
				return (long)((double)(Stopwatch.GetTimestamp() - this.startTimestamp) * ValueStopwatch.TimestampToTicks);
			}
		}

		// Token: 0x04000143 RID: 323
		private static readonly double TimestampToTicks = 10000000.0 / (double)Stopwatch.Frequency;

		// Token: 0x04000144 RID: 324
		private readonly long startTimestamp;
	}
}
