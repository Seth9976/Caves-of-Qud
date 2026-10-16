using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002E4 RID: 740
	[VisibleToOtherModules]
	internal class SystemClock
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x00046784 File Offset: 0x00044984
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0004679C File Offset: 0x0004499C
		public static long ToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000467CC File Offset: 0x000449CC
		public static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalSeconds);
		}

		// Token: 0x04000A52 RID: 2642
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
