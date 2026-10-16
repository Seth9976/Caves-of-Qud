using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020006AB RID: 1707
	internal class MonoHttpDate
	{
		// Token: 0x06003687 RID: 13959 RVA: 0x000BF780 File Offset: 0x000BD980
		internal static DateTime Parse(string dateStr)
		{
			return DateTime.ParseExact(dateStr, MonoHttpDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
		}

		// Token: 0x04001FF2 RID: 8178
		private static readonly string rfc1123_date = "r";

		// Token: 0x04001FF3 RID: 8179
		private static readonly string rfc850_date = "dddd, dd-MMM-yy HH:mm:ss G\\MT";

		// Token: 0x04001FF4 RID: 8180
		private static readonly string asctime_date = "ddd MMM d HH:mm:ss yyyy";

		// Token: 0x04001FF5 RID: 8181
		private static readonly string[] formats = new string[]
		{
			MonoHttpDate.rfc1123_date,
			MonoHttpDate.rfc850_date,
			MonoHttpDate.asctime_date
		};
	}
}
