using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI
{
	// Token: 0x020001E6 RID: 486
	internal class MinimizableAttributeTypeConverter : TypeConverter
	{
		// Token: 0x0600138E RID: 5006 RVA: 0x00033095 File Offset: 0x00031295
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000330B4 File Offset: 0x000312B4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is string && destinationType == typeof(bool))
			{
				return value != null;
			}
			if (value is bool && destinationType == typeof(string))
			{
				return ((bool)value).ToString(culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0003311C File Offset: 0x0003131C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text2;
			if (value != null)
			{
				Type type = value.GetType();
				if (type == typeof(string))
				{
					string text = value as string;
					if (string.IsNullOrEmpty(text) || string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return false;
					}
					return true;
				}
				else
				{
					text2 = type.FullName;
				}
			}
			else
			{
				text2 = "null";
			}
			throw new NotSupportedException(string.Format("MinimizableAttributeTypeConverter cannot convert from {0}", text2));
		}
	}
}
