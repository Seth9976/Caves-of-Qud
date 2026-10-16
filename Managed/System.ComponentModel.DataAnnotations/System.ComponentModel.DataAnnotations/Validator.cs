using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Defines a helper class that can be used to validate objects, properties, and methods when it is included in their associated <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute" /> attributes.</summary>
	// Token: 0x02000044 RID: 68
	public static class Validator
	{
		/// <summary>Validates the property.</summary>
		/// <returns>true if the property validates; otherwise, false.</returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context that describes the property to validate.</param>
		/// <param name="validationResults">A collection to hold each failed validation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> cannot be assigned to the property.-or-<paramref name="value " />is null.</exception>
		// Token: 0x0600018D RID: 397 RVA: 0x00005618 File Offset: 0x00003818
		public static bool TryValidateProperty(object value, ValidationContext validationContext, ICollection<ValidationResult> validationResults)
		{
			Type propertyType = Validator._store.GetPropertyType(validationContext);
			Validator.EnsureValidPropertyType(validationContext.MemberName, propertyType, value);
			bool flag = true;
			bool flag2 = validationResults == null;
			IEnumerable<ValidationAttribute> propertyValidationAttributes = Validator._store.GetPropertyValidationAttributes(validationContext);
			foreach (Validator.ValidationError validationError in Validator.GetValidationErrors(value, validationContext, propertyValidationAttributes, flag2))
			{
				flag = false;
				if (validationResults != null)
				{
					validationResults.Add(validationError.ValidationResult);
				}
			}
			return flag;
		}

		/// <summary>Determines whether the specified object is valid using the validation context and validation results collection.</summary>
		/// <returns>true if the object validates; otherwise, false.</returns>
		/// <param name="instance">The object to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <param name="validationResults">A collection to hold each failed validation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600018E RID: 398 RVA: 0x000056A8 File Offset: 0x000038A8
		public static bool TryValidateObject(object instance, ValidationContext validationContext, ICollection<ValidationResult> validationResults)
		{
			return Validator.TryValidateObject(instance, validationContext, validationResults, false);
		}

		/// <summary>Determines whether the specified object is valid using the validation context, validation results collection, and a value that specifies whether to validate all properties.</summary>
		/// <returns>true if the object validates; otherwise, false.</returns>
		/// <param name="instance">The object to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <param name="validationResults">A collection to hold each failed validation.</param>
		/// <param name="validateAllProperties">true to validate all properties; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600018F RID: 399 RVA: 0x000056B4 File Offset: 0x000038B4
		public static bool TryValidateObject(object instance, ValidationContext validationContext, ICollection<ValidationResult> validationResults, bool validateAllProperties)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (validationContext != null && instance != validationContext.ObjectInstance)
			{
				throw new ArgumentException("The instance provided must match the ObjectInstance on the ValidationContext supplied.", "instance");
			}
			bool flag = true;
			bool flag2 = validationResults == null;
			foreach (Validator.ValidationError validationError in Validator.GetObjectValidationErrors(instance, validationContext, validateAllProperties, flag2))
			{
				flag = false;
				if (validationResults != null)
				{
					validationResults.Add(validationError.ValidationResult);
				}
			}
			return flag;
		}

		/// <summary>Returns a value that indicates whether the specified value is valid with the specified attributes.</summary>
		/// <returns>true if the object validates; otherwise, false.</returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <param name="validationResults">A collection to hold failed validations. </param>
		/// <param name="validationAttributes">The validation attributes.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x00005740 File Offset: 0x00003940
		public static bool TryValidateValue(object value, ValidationContext validationContext, ICollection<ValidationResult> validationResults, IEnumerable<ValidationAttribute> validationAttributes)
		{
			bool flag = true;
			bool flag2 = validationResults == null;
			foreach (Validator.ValidationError validationError in Validator.GetValidationErrors(value, validationContext, validationAttributes, flag2))
			{
				flag = false;
				if (validationResults != null)
				{
					validationResults.Add(validationError.ValidationResult);
				}
			}
			return flag;
		}

		/// <summary>Validates the property.</summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context that describes the property to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> cannot be assigned to the property.</exception>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The <paramref name="value" /> parameter is not valid.</exception>
		// Token: 0x06000191 RID: 401 RVA: 0x000057A4 File Offset: 0x000039A4
		public static void ValidateProperty(object value, ValidationContext validationContext)
		{
			Type propertyType = Validator._store.GetPropertyType(validationContext);
			Validator.EnsureValidPropertyType(validationContext.MemberName, propertyType, value);
			IEnumerable<ValidationAttribute> propertyValidationAttributes = Validator._store.GetPropertyValidationAttributes(validationContext);
			Validator.ValidationError validationError = Validator.GetValidationErrors(value, validationContext, propertyValidationAttributes, false).FirstOrDefault<Validator.ValidationError>();
			if (validationError != null)
			{
				validationError.ThrowValidationException();
			}
		}

		/// <summary>Determines whether the specified object is valid using the validation context.</summary>
		/// <param name="instance">The object to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The object is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000192 RID: 402 RVA: 0x000057EE File Offset: 0x000039EE
		public static void ValidateObject(object instance, ValidationContext validationContext)
		{
			Validator.ValidateObject(instance, validationContext, false);
		}

		/// <summary>Determines whether the specified object is valid using the validation context, and a value that specifies whether to validate all properties.</summary>
		/// <param name="instance">The object to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <param name="validateAllProperties">true to validate all properties; otherwise, false.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">
		///   <paramref name="instance" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06000193 RID: 403 RVA: 0x000057F8 File Offset: 0x000039F8
		public static void ValidateObject(object instance, ValidationContext validationContext, bool validateAllProperties)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			if (instance != validationContext.ObjectInstance)
			{
				throw new ArgumentException("The instance provided must match the ObjectInstance on the ValidationContext supplied.", "instance");
			}
			Validator.ValidationError validationError = Validator.GetObjectValidationErrors(instance, validationContext, validateAllProperties, false).FirstOrDefault<Validator.ValidationError>();
			if (validationError != null)
			{
				validationError.ThrowValidationException();
			}
		}

		/// <summary>Validates the specified attributes.</summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context that describes the object to validate.</param>
		/// <param name="validationAttributes">The validation attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="validationContext" /> parameter is null.</exception>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The <paramref name="value" /> parameter does not validate with the <paramref name="validationAttributes" /> parameter.</exception>
		// Token: 0x06000194 RID: 404 RVA: 0x00005854 File Offset: 0x00003A54
		public static void ValidateValue(object value, ValidationContext validationContext, IEnumerable<ValidationAttribute> validationAttributes)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			Validator.ValidationError validationError = Validator.GetValidationErrors(value, validationContext, validationAttributes, false).FirstOrDefault<Validator.ValidationError>();
			if (validationError != null)
			{
				validationError.ThrowValidationException();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005887 File Offset: 0x00003A87
		internal static ValidationContext CreateValidationContext(object instance, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			return new ValidationContext(instance, validationContext, validationContext.Items);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000058A4 File Offset: 0x00003AA4
		private static bool CanBeAssigned(Type destinationType, object value)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value == null)
			{
				return !destinationType.IsValueType || (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>));
			}
			return destinationType.IsAssignableFrom(value.GetType());
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000058FE File Offset: 0x00003AFE
		private static void EnsureValidPropertyType(string propertyName, Type propertyType, object value)
		{
			if (!Validator.CanBeAssigned(propertyType, value))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The value for property '{0}' must be of type '{1}'.", propertyName, propertyType), "value");
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005928 File Offset: 0x00003B28
		private static IEnumerable<Validator.ValidationError> GetObjectValidationErrors(object instance, ValidationContext validationContext, bool validateAllProperties, bool breakOnFirstError)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			List<Validator.ValidationError> list = new List<Validator.ValidationError>();
			list.AddRange(Validator.GetObjectPropertyValidationErrors(instance, validationContext, validateAllProperties, breakOnFirstError));
			if (list.Any<Validator.ValidationError>())
			{
				return list;
			}
			IEnumerable<ValidationAttribute> typeValidationAttributes = Validator._store.GetTypeValidationAttributes(validationContext);
			list.AddRange(Validator.GetValidationErrors(instance, validationContext, typeValidationAttributes, breakOnFirstError));
			if (list.Any<Validator.ValidationError>())
			{
				return list;
			}
			IValidatableObject validatableObject = instance as IValidatableObject;
			if (validatableObject != null)
			{
				foreach (ValidationResult validationResult in from r in validatableObject.Validate(validationContext)
					where r != ValidationResult.Success
					select r)
				{
					list.Add(new Validator.ValidationError(null, instance, validationResult));
				}
			}
			return list;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005A10 File Offset: 0x00003C10
		private static IEnumerable<Validator.ValidationError> GetObjectPropertyValidationErrors(object instance, ValidationContext validationContext, bool validateAllProperties, bool breakOnFirstError)
		{
			IEnumerable<KeyValuePair<ValidationContext, object>> propertyValues = Validator.GetPropertyValues(instance, validationContext);
			List<Validator.ValidationError> list = new List<Validator.ValidationError>();
			foreach (KeyValuePair<ValidationContext, object> keyValuePair in propertyValues)
			{
				IEnumerable<ValidationAttribute> propertyValidationAttributes = Validator._store.GetPropertyValidationAttributes(keyValuePair.Key);
				if (validateAllProperties)
				{
					list.AddRange(Validator.GetValidationErrors(keyValuePair.Value, keyValuePair.Key, propertyValidationAttributes, breakOnFirstError));
				}
				else
				{
					RequiredAttribute requiredAttribute = propertyValidationAttributes.FirstOrDefault<ValidationAttribute>((ValidationAttribute a) => a is RequiredAttribute) as RequiredAttribute;
					if (requiredAttribute != null)
					{
						ValidationResult validationResult = requiredAttribute.GetValidationResult(keyValuePair.Value, keyValuePair.Key);
						if (validationResult != ValidationResult.Success)
						{
							list.Add(new Validator.ValidationError(requiredAttribute, keyValuePair.Value, validationResult));
						}
					}
				}
				if (breakOnFirstError && list.Any<Validator.ValidationError>())
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005B0C File Offset: 0x00003D0C
		private static ICollection<KeyValuePair<ValidationContext, object>> GetPropertyValues(object instance, ValidationContext validationContext)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
			List<KeyValuePair<ValidationContext, object>> list = new List<KeyValuePair<ValidationContext, object>>(properties.Count);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				ValidationContext validationContext2 = Validator.CreateValidationContext(instance, validationContext);
				validationContext2.MemberName = propertyDescriptor.Name;
				if (Validator._store.GetPropertyValidationAttributes(validationContext2).Any<ValidationAttribute>())
				{
					list.Add(new KeyValuePair<ValidationContext, object>(validationContext2, propertyDescriptor.GetValue(instance)));
				}
			}
			return list;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private static IEnumerable<Validator.ValidationError> GetValidationErrors(object value, ValidationContext validationContext, IEnumerable<ValidationAttribute> attributes, bool breakOnFirstError)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			List<Validator.ValidationError> list = new List<Validator.ValidationError>();
			RequiredAttribute requiredAttribute = attributes.FirstOrDefault<ValidationAttribute>((ValidationAttribute a) => a is RequiredAttribute) as RequiredAttribute;
			Validator.ValidationError validationError;
			if (requiredAttribute != null && !Validator.TryValidate(value, validationContext, requiredAttribute, out validationError))
			{
				list.Add(validationError);
				return list;
			}
			foreach (ValidationAttribute validationAttribute in attributes)
			{
				if (validationAttribute != requiredAttribute && !Validator.TryValidate(value, validationContext, validationAttribute, out validationError))
				{
					list.Add(validationError);
					if (breakOnFirstError)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005C60 File Offset: 0x00003E60
		private static bool TryValidate(object value, ValidationContext validationContext, ValidationAttribute attribute, out Validator.ValidationError validationError)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			ValidationResult validationResult = attribute.GetValidationResult(value, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				validationError = new Validator.ValidationError(attribute, value, validationResult);
				return false;
			}
			validationError = null;
			return true;
		}

		// Token: 0x040000CF RID: 207
		private static ValidationAttributeStore _store = ValidationAttributeStore.Instance;

		// Token: 0x02000045 RID: 69
		private class ValidationError
		{
			// Token: 0x0600019E RID: 414 RVA: 0x00005CA8 File Offset: 0x00003EA8
			internal ValidationError(ValidationAttribute validationAttribute, object value, ValidationResult validationResult)
			{
				this.ValidationAttribute = validationAttribute;
				this.ValidationResult = validationResult;
				this.Value = value;
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600019F RID: 415 RVA: 0x00005CC5 File Offset: 0x00003EC5
			// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005CCD File Offset: 0x00003ECD
			internal object Value { get; set; }

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005CD6 File Offset: 0x00003ED6
			// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005CDE File Offset: 0x00003EDE
			internal ValidationAttribute ValidationAttribute { get; set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005CE7 File Offset: 0x00003EE7
			// (set) Token: 0x060001A4 RID: 420 RVA: 0x00005CEF File Offset: 0x00003EEF
			internal ValidationResult ValidationResult { get; set; }

			// Token: 0x060001A5 RID: 421 RVA: 0x00005CF8 File Offset: 0x00003EF8
			internal void ThrowValidationException()
			{
				throw new ValidationException(this.ValidationResult, this.ValidationAttribute, this.Value);
			}
		}
	}
}
