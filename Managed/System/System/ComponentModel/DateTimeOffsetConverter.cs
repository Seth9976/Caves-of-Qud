using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.DateTimeOffset" /> structures to and from various other representations.</summary>
	// Token: 0x02000414 RID: 1044
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DateTimeOffsetConverter : TypeConverter
	{
		/// <summary>Returns a value that indicates whether an object of the specified source type can be converted to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>true if the specified type can be converted to a <see cref="T:System.DateTimeOffset" />; otherwise, false.</returns>
		/// <param name="context">The date format context.</param>
		/// <param name="sourceType">The source type to check.</param>
		// Token: 0x060021AD RID: 8621 RVA: 0x00069E9B File Offset: 0x0006809B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.DateTimeOffset" /> can be converted to an object of the specified type.</summary>
		/// <returns>true if a <see cref="T:System.DateTimeOffset" /> can be converted to the specified type; otherwise, false.</returns>
		/// <param name="context">The date format context.</param>
		/// <param name="destinationType">The destination type to check.</param>
		// Token: 0x060021AE RID: 8622 RVA: 0x00069EB9 File Offset: 0x000680B9
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that represents the specified object.</returns>
		/// <param name="context">The date format context.</param>
		/// <param name="culture">The date culture.</param>
		/// <param name="value">The object to be converted.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060021AF RID: 8623 RVA: 0x00072FCC File Offset: 0x000711CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return DateTimeOffset.MinValue;
				}
				try
				{
					DateTimeFormatInfo dateTimeFormatInfo = null;
					if (culture != null)
					{
						dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					}
					if (dateTimeFormatInfo != null)
					{
						return DateTimeOffset.Parse(text, dateTimeFormatInfo);
					}
					return DateTimeOffset.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.GetString("{0} is not a valid value for {1}.", new object[]
					{
						(string)value,
						"DateTimeOffset"
					}), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts a <see cref="T:System.DateTimeOffset" /> to an object of the specified type.</summary>
		/// <returns>An object of the specified type that represents the <see cref="T:System.DateTimeOffset" />. </returns>
		/// <param name="context">The date format context.</param>
		/// <param name="culture">The date culture.</param>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <param name="destinationType">The type to convert to.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060021B0 RID: 8624 RVA: 0x00073084 File Offset: 0x00071284
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)) || !(value is DateTimeOffset))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					if (dateTimeOffset.Ticks == 0L)
					{
						ConstructorInfo constructor = typeof(DateTimeOffset).GetConstructor(new Type[] { typeof(long) });
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[] { dateTimeOffset.Ticks });
						}
					}
					ConstructorInfo constructor2 = typeof(DateTimeOffset).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(TimeSpan)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[] { dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, dateTimeOffset.Millisecond, dateTimeOffset.Offset });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTimeOffset dateTimeOffset2 = (DateTimeOffset)value;
			if (dateTimeOffset2 == DateTimeOffset.MinValue)
			{
				return string.Empty;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
			if (culture != CultureInfo.InvariantCulture)
			{
				string text;
				if (dateTimeOffset2.TimeOfDay.TotalSeconds == 0.0)
				{
					text = dateTimeFormatInfo.ShortDatePattern + " zzz";
				}
				else
				{
					text = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern + " zzz";
				}
				return dateTimeOffset2.ToString(text, CultureInfo.CurrentCulture);
			}
			if (dateTimeOffset2.TimeOfDay.TotalSeconds == 0.0)
			{
				return dateTimeOffset2.ToString("yyyy-MM-dd zzz", culture);
			}
			return dateTimeOffset2.ToString(culture);
		}
	}
}
