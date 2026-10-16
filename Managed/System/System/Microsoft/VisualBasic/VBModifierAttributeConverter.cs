using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200013B RID: 315
	internal abstract class VBModifierAttributeConverter : TypeConverter
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060007C0 RID: 1984
		protected abstract object[] Values { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007C1 RID: 1985
		protected abstract string[] Names { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007C2 RID: 1986
		protected abstract object DefaultValue { get; }

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001844F File Offset: 0x0001664F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00018470 File Offset: 0x00016670
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000184B8 File Offset: 0x000166B8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				object[] values = this.Values;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].Equals(value))
					{
						return this.Names[i];
					}
				}
				return "(unknown)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00018527 File Offset: 0x00016727
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
