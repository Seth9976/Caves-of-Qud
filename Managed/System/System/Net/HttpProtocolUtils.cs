using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005FB RID: 1531
	internal class HttpProtocolUtils
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x0000219B File Offset: 0x0000039B
		private HttpProtocolUtils()
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000A6DD8 File Offset: 0x000A4FD8
		internal static DateTime string2date(string S)
		{
			DateTime dateTime;
			if (HttpDateParse.ParseHttpDate(S, out dateTime))
			{
				return dateTime;
			}
			throw new ProtocolViolationException(SR.GetString("The value of the date string in the header is invalid."));
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000A6E00 File Offset: 0x000A5000
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", dateTimeFormatInfo);
		}
	}
}
