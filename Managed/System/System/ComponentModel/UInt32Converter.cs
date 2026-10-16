using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020003FB RID: 1019
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x00071FD1 File Offset: 0x000701D1
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00071FDD File Offset: 0x000701DD
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00071FEB File Offset: 0x000701EB
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00071FFC File Offset: 0x000701FC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
