using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x020003C5 RID: 965
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0006CD8D File Offset: 0x0006AF8D
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0006CD99 File Offset: 0x0006AF99
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0006CDA7 File Offset: 0x0006AFA7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0006CDB8 File Offset: 0x0006AFB8
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
