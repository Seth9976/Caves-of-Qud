using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x020003C4 RID: 964
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0006CD41 File Offset: 0x0006AF41
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0006CD4D File Offset: 0x0006AF4D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0006CD5B File Offset: 0x0006AF5B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0006CD6C File Offset: 0x0006AF6C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
