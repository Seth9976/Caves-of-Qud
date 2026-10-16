using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.CSharp
{
	// Token: 0x02000141 RID: 321
	internal abstract class CSharpModifierAttributeConverter : TypeConverter
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000881 RID: 2177
		protected abstract object[] Values { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000882 RID: 2178
		protected abstract string[] Names { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000883 RID: 2179
		protected abstract object DefaultValue { get; }

		// Token: 0x06000884 RID: 2180 RVA: 0x0001844F File Offset: 0x0001664F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
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

		// Token: 0x06000887 RID: 2183 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001E75F File Offset: 0x0001C95F
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
