using System;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000211 RID: 529
	[TimeSource("AccurateLocal")]
	public class AccurateLocalTimeSource : TimeSource
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00070618 File Offset: 0x0006E818
		public override DateTime Time
		{
			get
			{
				return DateTime.Now;
			}
		}
	}
}
