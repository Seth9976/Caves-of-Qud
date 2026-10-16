using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.Configuration
{
	/// <summary>Provides support to convert an object to a lowercase string. This class cannot be inherited.</summary>
	// Token: 0x020005B0 RID: 1456
	public sealed class LowerCaseStringConverter : TypeConverter
	{
		/// <summary>Determines whether an object can be converted to a lowercase string based on the specified parameters.</summary>
		/// <returns>true if the parameters describe an object that can be converted to a lowercase string object; otherwise, false.</returns>
		/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
		/// <param name="type">The type of object to convert.</param>
		// Token: 0x06003E72 RID: 15986 RVA: 0x00004110 File Offset: 0x00002310
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether an object can be converted to a lowercase string based on the specified parameters.</summary>
		/// <returns>true if the parameters describe an object that can be converted to a lowercase string object; otherwise, false.</returns>
		/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
		/// <param name="type">The type of object to convert.</param>
		// Token: 0x06003E73 RID: 15987 RVA: 0x00004110 File Offset: 0x00002310
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Converts an object from its original value to a lowercase string based on the specified parameters.</summary>
		/// <returns>A lowercase string object.</returns>
		/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
		/// <param name="ci">An object that implements the <see cref="T:System.Globalization.CultureInfo" /> class.</param>
		/// <param name="data">The object to convert.</param>
		// Token: 0x06003E74 RID: 15988 RVA: 0x000A34E3 File Offset: 0x000A16E3
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).ToLowerInvariant();
		}

		/// <summary>Converts an object to a lowercase string based on the specified parameters.</summary>
		/// <returns>A lowercase string object.</returns>
		/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
		/// <param name="ci">An object that implements the <see cref="T:System.Globalization.CultureInfo" /> class.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="type">The type of object to convert.</param>
		// Token: 0x06003E75 RID: 15989 RVA: 0x000A34F0 File Offset: 0x000A16F0
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return "";
			}
			if (!(value is string))
			{
				throw new ArgumentException("value");
			}
			return ((string)value).ToLowerInvariant();
		}
	}
}
