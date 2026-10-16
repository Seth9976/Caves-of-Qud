using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the result of retrieving a value from a value provider. </summary>
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class ValueProviderResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class.</summary>
		// Token: 0x060039E1 RID: 14817 RVA: 0x00002050 File Offset: 0x00000250
		protected ValueProviderResult()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class by using the specified raw value, attempted value, and culture information.</summary>
		/// <param name="rawValue">The raw value.</param>
		/// <param name="attemptedValue">The attempted value.</param>
		/// <param name="culture">The culture information.</param>
		// Token: 0x060039E2 RID: 14818 RVA: 0x0009AA21 File Offset: 0x00098C21
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		/// <summary>Gets or sets the raw value that is converted to a string for display.</summary>
		/// <returns>A string representation of the raw value.</returns>
		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x0009AA3E File Offset: 0x00098C3E
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x0009AA46 File Offset: 0x00098C46
		public string AttemptedValue { get; protected set; }

		/// <summary>Gets or sets the culture.</summary>
		/// <returns>The culture.</returns>
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x0009AA4F File Offset: 0x00098C4F
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x0009AA6A File Offset: 0x00098C6A
		public CultureInfo Culture
		{
			get
			{
				if (this._instanceCulture == null)
				{
					this._instanceCulture = ValueProviderResult._staticCulture;
				}
				return this._instanceCulture;
			}
			protected set
			{
				this._instanceCulture = value;
			}
		}

		/// <summary>Gets or sets the raw value that is supplied by the value provider.</summary>
		/// <returns>The raw value.</returns>
		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x0009AA73 File Offset: 0x00098C73
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x0009AA7B File Offset: 0x00098C7B
		public object RawValue { get; protected set; }

		// Token: 0x060039E9 RID: 14825 RVA: 0x0009AA84 File Offset: 0x00098C84
		private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && text.Trim().Length == 0)
			{
				return null;
			}
			TypeConverter typeConverter = TypeDescriptor.GetConverter(destinationType);
			bool flag = typeConverter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				typeConverter = TypeDescriptor.GetConverter(value.GetType());
			}
			if (flag || typeConverter.CanConvertTo(destinationType))
			{
				object obj;
				try
				{
					obj = (flag ? typeConverter.ConvertFrom(null, culture, value) : typeConverter.ConvertTo(null, culture, value, destinationType));
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed. See the inner exception for more information."), value.GetType().FullName, destinationType.FullName), ex);
				}
				return obj;
			}
			if (destinationType.IsEnum && value is int)
			{
				return Enum.ToObject(destinationType, (int)value);
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				return ValueProviderResult.ConvertSimpleType(culture, value, underlyingType);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed because no type converter can convert between these types."), value.GetType().FullName, destinationType.FullName));
		}

		/// <summary>Converts a value that is encapsulated by this result to the specified type.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
		// Token: 0x060039EA RID: 14826 RVA: 0x0009ABA8 File Offset: 0x00098DA8
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		/// <summary>Converts the value that is encapsulated by this result to the specified type by using the specified culture information.</summary>
		/// <returns>The converted value.</returns>
		/// <param name="type">The type.</param>
		/// <param name="culture">The culture information.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
		// Token: 0x060039EB RID: 14827 RVA: 0x0009ABB2 File Offset: 0x00098DB2
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return ValueProviderResult.UnwrapPossibleArrayType(culture ?? this.Culture, this.RawValue, type);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x0009ABE0 File Offset: 0x00098DE0
		private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			Array array = value as Array;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (array != null)
				{
					IList list = Array.CreateInstance(elementType, array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						list[i] = ValueProviderResult.ConvertSimpleType(culture, array.GetValue(i), elementType);
					}
					return list;
				}
				object obj = ValueProviderResult.ConvertSimpleType(culture, value, elementType);
				Array array2 = Array.CreateInstance(elementType, 1);
				((IList)array2)[0] = obj;
				return array2;
			}
			else
			{
				if (array == null)
				{
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				if (array.Length > 0)
				{
					value = array.GetValue(0);
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				return null;
			}
		}

		// Token: 0x04001F54 RID: 8020
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x04001F55 RID: 8021
		private CultureInfo _instanceCulture;
	}
}
