using System;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000214 RID: 532
	[TimeSource("FastLocal")]
	public class FastLocalTimeSource : CachedTimeSource
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0007068B File Offset: 0x0006E88B
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.Now;
			}
		}
	}
}
