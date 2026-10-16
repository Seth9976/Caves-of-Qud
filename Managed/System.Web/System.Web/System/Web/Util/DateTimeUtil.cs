using System;

namespace System.Web.Util
{
	// Token: 0x0200010F RID: 271
	internal sealed class DateTimeUtil
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00002050 File Offset: 0x00000250
		private DateTimeUtil()
		{
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000254A4 File Offset: 0x000236A4
		internal static DateTime FromFileTimeToUtc(long filetime)
		{
			return new DateTime(filetime + 504911232000000000L, DateTimeKind.Utc);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000254B7 File Offset: 0x000236B7
		internal static DateTime ConvertToUniversalTime(DateTime localTime)
		{
			if (localTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (localTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return localTime.ToUniversalTime();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000254E6 File Offset: 0x000236E6
		internal static DateTime ConvertToLocalTime(DateTime utcTime)
		{
			if (utcTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (utcTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return utcTime.ToLocalTime();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00025518 File Offset: 0x00023718
		internal static TimeSpan GetTimeoutFromTimeUnit(int timeoutValue, TimeUnit timeoutUnit)
		{
			switch (timeoutUnit)
			{
			case TimeUnit.Days:
				return new TimeSpan(timeoutValue, 0, 0, 0);
			case TimeUnit.Hours:
				return new TimeSpan(timeoutValue, 0, 0);
			case TimeUnit.Minutes:
				return new TimeSpan(0, timeoutValue, 0);
			case TimeUnit.Seconds:
				return new TimeSpan(0, 0, timeoutValue);
			case TimeUnit.Milliseconds:
				return new TimeSpan(0, 0, 0, 0, timeoutValue);
			}
			throw new ArgumentException(global::SR.GetString("Invalid value for '{0}' parameter.", new object[] { "timeoutUnit" }));
		}

		// Token: 0x040011A6 RID: 4518
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040011A7 RID: 4519
		private static readonly DateTime MinValuePlusOneDay = DateTime.MinValue.AddDays(1.0);

		// Token: 0x040011A8 RID: 4520
		private static readonly DateTime MaxValueMinusOneDay = DateTime.MaxValue.AddDays(-1.0);
	}
}
