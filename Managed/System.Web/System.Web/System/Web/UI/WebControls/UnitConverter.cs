using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts from a <see cref="T:System.Web.UI.WebControls.Unit" /> object to an object of another data type and from another type to a <see cref="T:System.Web.UI.WebControls.Unit" /> object.</summary>
	// Token: 0x02000433 RID: 1075
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UnitConverter : TypeConverter
	{
		/// <summary>Returns a value indicating whether the unit converter can convert from the specified source type.</summary>
		/// <returns>true if the source type can be converted from; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that specifies the context of the object to convert. </param>
		/// <param name="sourceType">The type of the source. </param>
		// Token: 0x0600318F RID: 12687 RVA: 0x00033095 File Offset: 0x00031295
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the converter can convert a <see cref="T:System.Web.UI.WebControls.Unit" /> object to the specified type.</summary>
		/// <returns>true if the converter supports converting a <see cref="T:System.Web.UI.WebControls.Unit" /> object to the destination type; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> that represents the data type to convert to.</param>
		// Token: 0x06003190 RID: 12688 RVA: 0x0005BA70 File Offset: 0x00059C70
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Performs type conversion to the specified destination type given the specified context, object and argument list.</summary>
		/// <returns>The object resulting from conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass null for this parameter. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert to. </param>
		// Token: 0x06003191 RID: 12689 RVA: 0x00082858 File Offset: 0x00080A58
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Unit && destinationType == typeof(string))
			{
				return ((Unit)value).ToString(culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Performs type conversion from the specified context, object, and argument list.</summary>
		/// <returns>The object resulting from conversion.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass null for this parameter. </param>
		/// <param name="value">The object to convert. </param>
		// Token: 0x06003192 RID: 12690 RVA: 0x0008289B File Offset: 0x00080A9B
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.GetType() == typeof(string))
			{
				return new Unit((string)value, culture);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
