using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration
{
	// Token: 0x02000148 RID: 328
	internal class TypeAndNameConverter : TypeConverter
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x00043DD2 File Offset: 0x00041FD2
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00043DF0 File Offset: 0x00041FF0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return new TypeAndName((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00043E10 File Offset: 0x00042010
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			TypeAndName typeAndName = (TypeAndName)value;
			if (typeAndName.name != null)
			{
				return typeAndName.name;
			}
			return typeAndName.type.AssemblyQualifiedName;
		}
	}
}
