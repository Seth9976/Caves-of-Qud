using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Decimal" /> objects to and from various other representations.</summary>
	// Token: 0x0200039C RID: 924
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0006B886 File Offset: 0x00069A86
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		// Token: 0x06001E34 RID: 7732 RVA: 0x0006B892 File Offset: 0x00069A92
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.Decimal" /> using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06001E35 RID: 7733 RVA: 0x0006B8B0 File Offset: 0x00069AB0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] array = new object[] { decimal.GetBits((decimal)value) };
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[] { typeof(int[]) });
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, array);
			}
			return null;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0006B943 File Offset: 0x00069B43
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0006B955 File Offset: 0x00069B55
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0006B968 File Offset: 0x00069B68
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
