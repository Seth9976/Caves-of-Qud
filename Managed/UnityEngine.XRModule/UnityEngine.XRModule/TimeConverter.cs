using System;

namespace UnityEngine.XR
{
	// Token: 0x02000011 RID: 17
	internal static class TimeConverter
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003858 File Offset: 0x00001A58
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003870 File Offset: 0x00001A70
		public static long LocalDateTimeToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - TimeConverter.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000038A0 File Offset: 0x00001AA0
		public static DateTime UnixTimeMillisecondsToLocalDateTime(long unixTimeInMilliseconds)
		{
			return TimeConverter.s_Epoch.AddMilliseconds((double)unixTimeInMilliseconds).ToLocalTime();
		}

		// Token: 0x0400009C RID: 156
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
