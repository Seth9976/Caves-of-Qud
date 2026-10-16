using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200038A RID: 906
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0006991A File Offset: 0x00067B1A
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00069926 File Offset: 0x00067B26
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00069934 File Offset: 0x00067B34
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00069944 File Offset: 0x00067B44
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
