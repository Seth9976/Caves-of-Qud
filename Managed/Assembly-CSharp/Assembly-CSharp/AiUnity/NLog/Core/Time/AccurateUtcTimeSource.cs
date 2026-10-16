using System;

namespace AiUnity.NLog.Core.Time
{
	// Token: 0x02000212 RID: 530
	[TimeSource("AccurateUTC")]
	public class AccurateUtcTimeSource : TimeSource
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00070627 File Offset: 0x0006E827
		public override DateTime Time
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
