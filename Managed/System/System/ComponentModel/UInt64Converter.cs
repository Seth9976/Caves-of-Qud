using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020003FC RID: 1020
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0007201D File Offset: 0x0007021D
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00072029 File Offset: 0x00070229
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00072037 File Offset: 0x00070237
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00072048 File Offset: 0x00070248
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
