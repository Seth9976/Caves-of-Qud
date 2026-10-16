using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a base type converter for nonfloating-point numerical types.</summary>
	// Token: 0x02000384 RID: 900
	public abstract class BaseNumberConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BaseNumberConverter" /> class.</summary>
		// Token: 0x06001D7F RID: 7551 RVA: 0x00018534 File Offset: 0x00016734
		internal BaseNumberConverter()
		{
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0000390E File Offset: 0x00001B0E
		internal virtual bool AllowHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001D81 RID: 7553
		internal abstract Type TargetType { get; }

		// Token: 0x06001D82 RID: 7554
		internal abstract object FromString(string value, int radix);

		// Token: 0x06001D83 RID: 7555
		internal abstract object FromString(string value, NumberFormatInfo formatInfo);

		// Token: 0x06001D84 RID: 7556
		internal abstract string ToString(object value, NumberFormatInfo formatInfo);

		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which you want to convert. </param>
		// Token: 0x06001D85 RID: 7557 RVA: 0x0001844F File Offset: 0x0001664F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the converter's native type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="value" /> is not a valid value for the target type.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001D86 RID: 7558 RVA: 0x00068F90 File Offset: 0x00067190
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				try
				{
					if (this.AllowHex && text[0] == '#')
					{
						return this.FromString(text.Substring(1), 16);
					}
					if ((this.AllowHex && text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) || text.StartsWith("&h", StringComparison.OrdinalIgnoreCase))
					{
						return this.FromString(text.Substring(2), 16);
					}
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.FromString(text, numberFormatInfo);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(SR.Format("{0} is not a valid value for {1}.", text, this.TargetType.Name), "value", ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001D87 RID: 7559 RVA: 0x00069078 File Offset: 0x00067278
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && this.TargetType.IsInstanceOfType(value))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
				return this.ToString(value, numberFormatInfo);
			}
			if (destinationType.IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="t">A <see cref="T:System.Type" /> that represents the type to which you want to convert. </param>
		// Token: 0x06001D88 RID: 7560 RVA: 0x00069105 File Offset: 0x00067305
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType) || destinationType.IsPrimitive;
		}
	}
}
