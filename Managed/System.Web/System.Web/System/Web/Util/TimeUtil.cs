using System;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000147 RID: 327
	internal sealed class TimeUtil
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00002050 File Offset: 0x00000250
		private TimeUtil()
		{
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00027E00 File Offset: 0x00026000
		internal static string ToUtcTimeString(DateTime dt)
		{
			return dt.ToUniversalTime().ToString("R", DateTimeFormatInfo.InvariantInfo);
		}
	}
}
