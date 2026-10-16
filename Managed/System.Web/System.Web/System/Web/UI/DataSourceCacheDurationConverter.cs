using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from data source control cache duration representations.</summary>
	// Token: 0x020001BE RID: 446
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataSourceCacheDurationConverter : Int32Converter
	{
		/// <summary>Determines whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> can convert an object in the given source type to an <see cref="T:System.Int32" /> object.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents a type that the converter converts.</param>
		// Token: 0x06001210 RID: 4624 RVA: 0x0002FEED File Offset: 0x0002E0ED
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance can convert an object to the given destination type.</summary>
		/// <returns>true if this converter can perform the operation; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" />  that represents the type to which you want to convert.</param>
		// Token: 0x06001211 RID: 4625 RVA: 0x0002FF0B File Offset: 0x0002E10B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to an <see cref="T:System.Int32" /> object.</summary>
		/// <returns>An object that represents the converted value. If null is passed in the value parameter, null is returned.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		// Token: 0x06001212 RID: 4626 RVA: 0x0002FF2C File Offset: 0x0002E12C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text != null && (text.Length == 0 || string.Compare("infinite", text, StringComparison.OrdinalIgnoreCase) == 0))
			{
				return 0;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <returns>An object that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		// Token: 0x06001213 RID: 4627 RVA: 0x0002FF6E File Offset: 0x0002E16E
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return string.Empty;
				}
				if (value is int && (int)value == 0)
				{
					return "Infinite";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard values for the data type the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains 0 for cache duration, which represents "Infinite".</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		// Token: 0x06001214 RID: 4628 RVA: 0x0002FFAD File Offset: 0x0002E1AD
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(DataSourceCacheDurationConverter.standardValues);
		}

		/// <summary>Specifies whether the collection of standard values returned from the <see cref="Overload:System.Web.UI.DataSourceCacheDurationConverter.GetStandardValues" /> method is an exclusive list, using the specified context.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06001215 RID: 4629 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Specifies whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		// Token: 0x06001216 RID: 4630 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04001415 RID: 5141
		private static readonly List<int> standardValues = new List<int> { 0 };
	}
}
