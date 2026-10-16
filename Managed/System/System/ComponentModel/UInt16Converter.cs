using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020003FA RID: 1018
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00071F85 File Offset: 0x00070185
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00071F91 File Offset: 0x00070191
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00071F9F File Offset: 0x0007019F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00071FB0 File Offset: 0x000701B0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
