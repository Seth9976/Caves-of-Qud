using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020003C3 RID: 963
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006CCF7 File Offset: 0x0006AEF7
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0006CD03 File Offset: 0x0006AF03
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0006CD11 File Offset: 0x0006AF11
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0006CD20 File Offset: 0x0006AF20
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
