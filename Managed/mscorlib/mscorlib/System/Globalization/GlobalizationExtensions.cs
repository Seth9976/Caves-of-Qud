using System;

namespace System.Globalization
{
	// Token: 0x02000965 RID: 2405
	public static class GlobalizationExtensions
	{
		// Token: 0x06005542 RID: 21826 RVA: 0x0011DEE8 File Offset: 0x0011C0E8
		public static StringComparer GetStringComparer(this CompareInfo compareInfo, CompareOptions options)
		{
			if (compareInfo == null)
			{
				throw new ArgumentNullException("compareInfo");
			}
			if (options == CompareOptions.Ordinal)
			{
				return StringComparer.Ordinal;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return StringComparer.OrdinalIgnoreCase;
			}
			return new CultureAwareComparer(compareInfo, options);
		}
	}
}
