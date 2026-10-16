using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x02000424 RID: 1060
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class TypeConverter
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000778AE File Offset: 0x00075AAE
		private static bool UseCompatibleTypeConversion
		{
			get
			{
				return TypeConverter.useCompatibleTypeConversion;
			}
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x06002265 RID: 8805 RVA: 0x000778B7 File Offset: 0x00075AB7
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x06002266 RID: 8806 RVA: 0x000778C1 File Offset: 0x00075AC1
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x06002267 RID: 8807 RVA: 0x000778D8 File Offset: 0x00075AD8
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x06002268 RID: 8808 RVA: 0x000778E2 File Offset: 0x00075AE2
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converts the given value to the type of this converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002269 RID: 8809 RVA: 0x000778F4 File Offset: 0x00075AF4
		public object ConvertFrom(object value)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, value);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600226A RID: 8810 RVA: 0x00077904 File Offset: 0x00075B04
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			throw this.GetConvertFromException(value);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600226B RID: 8811 RVA: 0x00077929 File Offset: 0x00075B29
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromString(null, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture and the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600226C RID: 8812 RVA: 0x00077938 File Offset: 0x00075B38
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the specified text to an object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The text representation of the object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The string cannot be converted into the appropriate object. </exception>
		// Token: 0x0600226D RID: 8813 RVA: 0x00077947 File Offset: 0x00075B47
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(null, null, text);
		}

		/// <summary>Converts the given text to an object, using the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600226E RID: 8814 RVA: 0x00077952 File Offset: 0x00075B52
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFrom(context, CultureInfo.CurrentCulture, text);
		}

		/// <summary>Converts the given text to an object, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600226F RID: 8815 RVA: 0x00077961 File Offset: 0x00075B61
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		/// <summary>Converts the given value object to the specified type, using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002270 RID: 8816 RVA: 0x0007796C File Offset: 0x00075B6C
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002271 RID: 8817 RVA: 0x00077978 File Offset: 0x00075B78
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				throw this.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (culture != null && culture != CultureInfo.CurrentCulture)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(null, culture);
				}
			}
			return value.ToString();
		}

		/// <summary>Converts the specified value to a culture-invariant string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002272 RID: 8818 RVA: 0x000779E4 File Offset: 0x00075BE4
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToString(null, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a culture-invariant string representation, using the specified context.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002273 RID: 8819 RVA: 0x000779F3 File Offset: 0x00075BF3
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return this.ConvertToString(context, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a string representation.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002274 RID: 8820 RVA: 0x00077A02 File Offset: 0x00075C02
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the given context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002275 RID: 8821 RVA: 0x00077A20 File Offset: 0x00075C20
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06002276 RID: 8822 RVA: 0x00077A3E File Offset: 0x00075C3E
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		/// <summary>Re-creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that represents a dictionary of new property values. </param>
		// Token: 0x06002277 RID: 8823 RVA: 0x00077A58 File Offset: 0x00075C58
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06002278 RID: 8824 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x06002279 RID: 8825 RVA: 0x00077A64 File Offset: 0x00075C64
		protected Exception GetConvertFromException(object value)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("(null)");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("{0} cannot convert from {1}.", new object[]
			{
				base.GetType().Name,
				text
			}));
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type the conversion was trying to convert to. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x0600227A RID: 8826 RVA: 0x00077AB4 File Offset: 0x00075CB4
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("(null)");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("'{0}' is unable to convert '{1}' to '{2}'.", new object[]
			{
				base.GetType().Name,
				text,
				destinationType.FullName
			}));
		}

		/// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		// Token: 0x0600227B RID: 8827 RVA: 0x00077B0D File Offset: 0x00075D0D
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x0600227C RID: 8828 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x0600227D RID: 8829 RVA: 0x00077B16 File Offset: 0x00075D16
		public PropertyDescriptorCollection GetProperties(object value)
		{
			return this.GetProperties(null, value);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x0600227E RID: 8830 RVA: 0x00077B20 File Offset: 0x00075D20
		public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value)
		{
			return this.GetProperties(context, value, new Attribute[] { BrowsableAttribute.Yes });
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x0600227F RID: 8831 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Returns whether this object supports properties.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		// Token: 0x06002280 RID: 8832 RVA: 0x00077B38 File Offset: 0x00075D38
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06002281 RID: 8833 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		// Token: 0x06002282 RID: 8834 RVA: 0x00077B41 File Offset: 0x00075D41
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
		// Token: 0x06002283 RID: 8835 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		// Token: 0x06002284 RID: 8836 RVA: 0x00077B4A File Offset: 0x00075D4A
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06002285 RID: 8837 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		// Token: 0x06002286 RID: 8838 RVA: 0x00077B53 File Offset: 0x00075D53
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06002287 RID: 8839 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether the given value object is valid for this type.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="value">The object to test for validity. </param>
		// Token: 0x06002288 RID: 8840 RVA: 0x00077B5C File Offset: 0x00075D5C
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x06002289 RID: 8841 RVA: 0x00077B68 File Offset: 0x00075D68
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (TypeConverter.UseCompatibleTypeConversion)
			{
				return true;
			}
			bool flag = true;
			try
			{
				if (value == null || this.CanConvertFrom(context, value.GetType()))
				{
					this.ConvertFrom(context, CultureInfo.InvariantCulture, value);
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Sorts a collection of properties.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted properties.</returns>
		/// <param name="props">A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that has the properties to sort. </param>
		/// <param name="names">An array of names in the order you want the properties to appear in the collection. </param>
		// Token: 0x0600228A RID: 8842 RVA: 0x00077BBC File Offset: 0x00075DBC
		protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
		{
			props.Sort(names);
			return props;
		}

		// Token: 0x0400108F RID: 4239
		private const string s_UseCompatibleTypeConverterBehavior = "UseCompatibleTypeConverterBehavior";

		// Token: 0x04001090 RID: 4240
		private static volatile bool useCompatibleTypeConversion;

		/// <summary>Represents an abstract class that provides properties for objects that do not have properties.</summary>
		// Token: 0x02000425 RID: 1061
		protected abstract class SimplePropertyDescriptor : PropertyDescriptor
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			// Token: 0x0600228C RID: 8844 RVA: 0x00077BC7 File Offset: 0x00075DC7
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType)
				: this(componentType, name, propertyType, new Attribute[0])
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			/// <param name="attributes">An <see cref="T:System.Attribute" /> array with the attributes to associate with the property. </param>
			// Token: 0x0600228D RID: 8845 RVA: 0x00077BD8 File Offset: 0x00075DD8
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes)
				: base(name, attributes)
			{
				this.componentType = componentType;
				this.propertyType = propertyType;
			}

			/// <summary>Gets the type of component to which this property description binds.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of component to which this property binds.</returns>
			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x0600228E RID: 8846 RVA: 0x00077BF1 File Offset: 0x00075DF1
			public override Type ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			/// <summary>Gets a value indicating whether this property is read-only.</summary>
			/// <returns>true if the property is read-only; false if the property is read/write.</returns>
			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x0600228F RID: 8847 RVA: 0x00077BF9 File Offset: 0x00075DF9
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes.Contains(ReadOnlyAttribute.Yes);
				}
			}

			/// <summary>Gets the type of the property.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06002290 RID: 8848 RVA: 0x00077C0B File Offset: 0x00075E0B
			public override Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			/// <summary>Returns whether resetting the component changes the value of the component.</summary>
			/// <returns>true if resetting the component changes the value of the component; otherwise, false.</returns>
			/// <param name="component">The component to test for reset capability. </param>
			// Token: 0x06002291 RID: 8849 RVA: 0x00077C14 File Offset: 0x00075E14
			public override bool CanResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				return defaultValueAttribute != null && defaultValueAttribute.Value.Equals(this.GetValue(component));
			}

			/// <summary>Resets the value for this property of the component.</summary>
			/// <param name="component">The component with the property value to be reset. </param>
			// Token: 0x06002292 RID: 8850 RVA: 0x00077C54 File Offset: 0x00075E54
			public override void ResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					this.SetValue(component, defaultValueAttribute.Value);
				}
			}

			/// <summary>Returns whether the value of this property can persist.</summary>
			/// <returns>true if the value of the property can persist; otherwise, false.</returns>
			/// <param name="component">The component with the property that is to be examined for persistence. </param>
			// Token: 0x06002293 RID: 8851 RVA: 0x00003062 File Offset: 0x00001262
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04001091 RID: 4241
			private Type componentType;

			// Token: 0x04001092 RID: 4242
			private Type propertyType;
		}

		/// <summary>Represents a collection of values.</summary>
		// Token: 0x02000426 RID: 1062
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> class.</summary>
			/// <param name="values">An <see cref="T:System.Collections.ICollection" /> that represents the objects to put into the collection. </param>
			// Token: 0x06002294 RID: 8852 RVA: 0x00077C8C File Offset: 0x00075E8C
			public StandardValuesCollection(ICollection values)
			{
				if (values == null)
				{
					values = new object[0];
				}
				Array array = values as Array;
				if (array != null)
				{
					this.valueArray = array;
				}
				this.values = values;
			}

			/// <summary>Gets the number of objects in the collection.</summary>
			/// <returns>The number of objects in the collection.</returns>
			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x06002295 RID: 8853 RVA: 0x00077CC2 File Offset: 0x00075EC2
			public int Count
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.Length;
					}
					return this.values.Count;
				}
			}

			/// <summary>Gets the object at the specified index number.</summary>
			/// <returns>The <see cref="T:System.Object" /> with the specified index.</returns>
			/// <param name="index">The zero-based index of the <see cref="T:System.Object" /> to get from the collection. </param>
			// Token: 0x1700071F RID: 1823
			public object this[int index]
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.GetValue(index);
					}
					IList list = this.values as IList;
					if (list != null)
					{
						return list[index];
					}
					this.valueArray = new object[this.values.Count];
					this.values.CopyTo(this.valueArray, 0);
					return this.valueArray.GetValue(index);
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> that represents the array to copy to. </param>
			/// <param name="index">The index to start from. </param>
			// Token: 0x06002297 RID: 8855 RVA: 0x00077D51 File Offset: 0x00075F51
			public void CopyTo(Array array, int index)
			{
				this.values.CopyTo(array, index);
			}

			/// <summary>Returns an enumerator for this collection.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06002298 RID: 8856 RVA: 0x00077D60 File Offset: 0x00075F60
			public IEnumerator GetEnumerator()
			{
				return this.values.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06002299 RID: 8857 RVA: 0x00077D6D File Offset: 0x00075F6D
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x0600229A RID: 8858 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>null in all cases.</returns>
			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x0600229B RID: 8859 RVA: 0x00002F6A File Offset: 0x0000116A
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">The array to copy to. </param>
			/// <param name="index">The index in the array where copying should begin. </param>
			// Token: 0x0600229C RID: 8860 RVA: 0x00077D75 File Offset: 0x00075F75
			void ICollection.CopyTo(Array array, int index)
			{
				this.CopyTo(array, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x0600229D RID: 8861 RVA: 0x00077D7F File Offset: 0x00075F7F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04001093 RID: 4243
			private ICollection values;

			// Token: 0x04001094 RID: 4244
			private Array valueArray;
		}
	}
}
