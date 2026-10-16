using System;
using System.Diagnostics;

namespace Roslyn.Utilities
{
	// Token: 0x02000060 RID: 96
	internal readonly struct SharedStopwatch
	{
		// Token: 0x06000205 RID: 517 RVA: 0x000071C9 File Offset: 0x000053C9
		private SharedStopwatch(TimeSpan started)
		{
			this._started = started;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000071D2 File Offset: 0x000053D2
		public TimeSpan Elapsed
		{
			get
			{
				return SharedStopwatch.s_stopwatch.Elapsed - this._started;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000071E9 File Offset: 0x000053E9
		public static SharedStopwatch StartNew()
		{
			return new SharedStopwatch(SharedStopwatch.s_stopwatch.Elapsed);
		}

		// Token: 0x04000089 RID: 137
		private static readonly Stopwatch s_stopwatch = Stopwatch.StartNew();

		// Token: 0x0400008A RID: 138
		private readonly TimeSpan _started;
	}
}
