using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a type converter to convert a string of comma-separated values to and from an array of strings.</summary>
	// Token: 0x0200040B RID: 1035
	public class StringArrayConverter : TypeConverter
	{
		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.StringArrayConverter" /> can convert the specified source type to an array of strings.</summary>
		/// <returns>true if the converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> to convert.</param>
		// Token: 0x06002E8E RID: 11918 RVA: 0x00033095 File Offset: 0x00031295
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the specified comma-separated string into an array of strings.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object. If null, the current culture is used.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)">The conversion cannot be performed because <paramref name="value" /> is not a string.</exception>
		// Token: 0x06002E8F RID: 11919 RVA: 0x0007917B File Offset: 0x0007737B
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is string)
			{
				return ((string)value).Split(',', StringSplitOptions.None);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts an array of strings into a string of values separated by commas.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object. If null, the current culture is used.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert <paramref name="value" /> to.</param>
		/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)">
		///   <paramref name="destinationType" /> is not of type <see cref="T:System.String" />.</exception>
		// Token: 0x06002E90 RID: 11920 RVA: 0x000791A2 File Offset: 0x000773A2
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is string[] && destinationType == typeof(string))
			{
				return string.Join(",", (string[])value);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
