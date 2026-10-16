using System;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000215 RID: 533
	[TimeSource("FastUTC")]
	public class FastUtcTimeSource : CachedTimeSource
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0007069A File Offset: 0x0006E89A
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
