using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020003A7 RID: 935
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0006C131 File Offset: 0x0006A331
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0006C13D File Offset: 0x0006A33D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0006C14F File Offset: 0x0006A34F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0006C164 File Offset: 0x0006A364
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
