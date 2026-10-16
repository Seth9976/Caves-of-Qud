using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020003EF RID: 1007
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00071883 File Offset: 0x0006FA83
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0007188F File Offset: 0x0006FA8F
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000718A1 File Offset: 0x0006FAA1
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000718B4 File Offset: 0x0006FAB4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
