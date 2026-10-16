using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020003ED RID: 1005
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000717C4 File Offset: 0x0006F9C4
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000717D0 File Offset: 0x0006F9D0
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000717DE File Offset: 0x0006F9DE
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000717F0 File Offset: 0x0006F9F0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
