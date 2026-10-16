using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000144 RID: 324
	internal class TypeTypeConverter : TypeAndNameConverter
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x00043B5D File Offset: 0x00041D5D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00043B67 File Offset: 0x00041D67
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return ((TypeAndName)base.ConvertFrom(context, culture, value)).type;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00043B90 File Offset: 0x00041D90
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				TypeAndName typeAndName = new TypeAndName((Type)value);
				return base.ConvertTo(context, culture, typeAndName, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
