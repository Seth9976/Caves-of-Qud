using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents the personalization attribute. This class cannot be inherited.</summary>
	/// <exception cref="T:System.Web.HttpException">The property is a read-only or write-only public property.- or -The property is a private or protected read/write property.- or -The property has index parameters.</exception>
	// Token: 0x02000482 RID: 1154
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PersonalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class. </summary>
		// Token: 0x06003440 RID: 13376 RVA: 0x00088326 File Offset: 0x00086526
		public PersonalizableAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
		/// <param name="isPersonalizable">A Boolean value indicating whether the property can be personalized.</param>
		// Token: 0x06003441 RID: 13377 RVA: 0x0008832F File Offset: 0x0008652F
		public PersonalizableAttribute(bool isPersonalizable)
		{
			this.isPersonalizable = isPersonalizable;
			this.scope = PersonalizationScope.User;
			this.isSensitive = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
		// Token: 0x06003442 RID: 13378 RVA: 0x0008834C File Offset: 0x0008654C
		public PersonalizableAttribute(PersonalizationScope scope)
			: this(scope, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameters.</summary>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
		/// <param name="isSensitive">A Boolean value indicating whether the property information is considered sensitive.</param>
		// Token: 0x06003443 RID: 13379 RVA: 0x00088356 File Offset: 0x00086556
		public PersonalizableAttribute(PersonalizationScope scope, bool isSensitive)
		{
			this.isPersonalizable = true;
			this.scope = scope;
			this.isSensitive = isSensitive;
		}

		/// <summary>Gets the setting that indicates whether the attribute can be personalized, as established by one of the constructors.</summary>
		/// <returns>true if the property can be personalized; otherwise, false.</returns>
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x00088373 File Offset: 0x00086573
		public bool IsPersonalizable
		{
			get
			{
				return this.isPersonalizable;
			}
		}

		/// <summary>Gets the setting that indicates whether the attribute is sensitive, as established by one of the constructors.</summary>
		/// <returns>true if the property is sensitive; otherwise, false.</returns>
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x0008837B File Offset: 0x0008657B
		public bool IsSensitive
		{
			get
			{
				return this.isSensitive;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value for the class instance, as set by one of the constructors.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value.</returns>
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x00088383 File Offset: 0x00086583
		public PersonalizationScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		/// <summary>When overridden, returns a Boolean evaluation of the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and another <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> instance supplied as a parameter.</summary>
		/// <returns>true if the values are equal; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
		// Token: 0x06003447 RID: 13383 RVA: 0x0008838C File Offset: 0x0008658C
		public override bool Equals(object obj)
		{
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return personalizableAttribute != null && (this.isPersonalizable == personalizableAttribute.IsPersonalizable && this.isSensitive == personalizableAttribute.IsSensitive) && this.scope == personalizableAttribute.Scope;
		}

		/// <summary>When overridden, returns a hash code of the attribute.</summary>
		/// <returns>A hash code in the form of an integer.</returns>
		// Token: 0x06003448 RID: 13384 RVA: 0x000883D1 File Offset: 0x000865D1
		public override int GetHashCode()
		{
			return this.isPersonalizable.GetHashCode() ^ this.isSensitive.GetHashCode() ^ this.scope.GetHashCode();
		}

		/// <summary>Returns a collection of <see cref="T:System.Reflection.PropertyInfo" /> objects for the properties that match the parameter type and are marked as personalizable.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of personalizable properties.</returns>
		/// <param name="type">The type on which to look for Personalizable properties.</param>
		/// <exception cref="T:System.Web.HttpException">A public property on the type is marked as personalizable but is read-only.</exception>
		// Token: 0x06003449 RID: 13385 RVA: 0x000883FC File Offset: 0x000865FC
		public static ICollection GetPersonalizableProperties(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			PropertyInfo[] properties = type.GetProperties();
			if (properties == null || properties.Length == 0)
			{
				return new PropertyInfo[0];
			}
			List<PropertyInfo> list = null;
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (PersonalizableAttribute.PropertyQualifies(propertyInfo))
				{
					if (list == null)
					{
						list = new List<PropertyInfo>();
					}
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00088464 File Offset: 0x00086664
		private static bool PropertyQualifies(PropertyInfo pi)
		{
			object[] customAttributes = pi.GetCustomAttributes(false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return false;
			}
			object[] array = customAttributes;
			int i = 0;
			while (i < array.Length)
			{
				PersonalizableAttribute personalizableAttribute = array[i] as PersonalizableAttribute;
				if (personalizableAttribute != null && personalizableAttribute.IsPersonalizable)
				{
					if (pi.GetSetMethod(false) == null)
					{
						throw new HttpException("A public property on the type is marked as personalizable but is read-only.");
					}
					return true;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		/// <summary>When overridden, returns a value that indicates whether the attribute instance equals the value of the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field.</summary>
		/// <returns>true if the attribute instance equals the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field; otherwise, false.</returns>
		// Token: 0x0600344B RID: 13387 RVA: 0x000884C3 File Offset: 0x000866C3
		public override bool IsDefaultAttribute()
		{
			return object.Equals(this, PersonalizableAttribute.Default);
		}

		/// <summary>Returns a value that indicates whether the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and the specified <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> property value.</summary>
		/// <returns>true if the two attributes have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> value; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
		// Token: 0x0600344C RID: 13388 RVA: 0x000884D0 File Offset: 0x000866D0
		public override bool Match(object obj)
		{
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return obj != null && this.isPersonalizable == personalizableAttribute.IsPersonalizable;
		}

		/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
		// Token: 0x04001D0B RID: 7435
		public static readonly PersonalizableAttribute Default = new PersonalizableAttribute(false);

		/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
		// Token: 0x04001D0C RID: 7436
		public static readonly PersonalizableAttribute NotPersonalizable = PersonalizableAttribute.Default;

		/// <summary>Returns an attribute instance that indicates support for personalization. This field is read-only.</summary>
		// Token: 0x04001D0D RID: 7437
		public static readonly PersonalizableAttribute Personalizable = new PersonalizableAttribute(PersonalizationScope.User, false);

		/// <summary>Returns an attribute instance that indicates support for personalization with a shared scope. This field is read-only.</summary>
		// Token: 0x04001D0E RID: 7438
		public static readonly PersonalizableAttribute SharedPersonalizable = new PersonalizableAttribute(PersonalizationScope.Shared, false);

		/// <summary>Returns an attribute instance that indicates support for personalization in <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope. This field is read-only.</summary>
		// Token: 0x04001D0F RID: 7439
		public static readonly PersonalizableAttribute UserPersonalizable = new PersonalizableAttribute(PersonalizationScope.User, false);

		// Token: 0x04001D10 RID: 7440
		private bool isPersonalizable;

		// Token: 0x04001D11 RID: 7441
		private bool isSensitive;

		// Token: 0x04001D12 RID: 7442
		private PersonalizationScope scope;
	}
}
