using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Specifies whether the property to which the attribute is applied supports device filtering. This class cannot be inherited.</summary>
	// Token: 0x0200015D RID: 349
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class FilterableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class.</summary>
		/// <param name="filterable">true to indicate that the property to which the attribute is applied supports device filtering; otherwise, false.</param>
		// Token: 0x06000F0A RID: 3850 RVA: 0x000288BA File Offset: 0x00026ABA
		public FilterableAttribute(bool filterable)
		{
			this._filterable = filterable;
		}

		/// <summary>Gets a value indicating whether the property to which the <see cref="T:System.Web.UI.FilterableAttribute" /> attribute is applied supports device filtering.</summary>
		/// <returns>true to indicate that the property to which the attribute is applied supports device filtering; otherwise, false.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000288C9 File Offset: 0x00026AC9
		public bool Filterable
		{
			get
			{
				return this._filterable;
			}
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the specified object.</summary>
		/// <returns>true if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
		// Token: 0x06000F0C RID: 3852 RVA: 0x000288D4 File Offset: 0x00026AD4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FilterableAttribute filterableAttribute = obj as FilterableAttribute;
			return filterableAttribute != null && filterableAttribute.Filterable == this._filterable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F0D RID: 3853 RVA: 0x00028901 File Offset: 0x00026B01
		public override int GetHashCode()
		{
			return this._filterable.GetHashCode();
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the <see cref="F:System.Web.UI.FilterableAttribute.Default" /> attribute.</summary>
		/// <returns>true if the current instance of <see cref="T:System.Web.UI.FilterableAttribute" /> is equal to <see cref="F:System.Web.UI.FilterableAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x06000F0E RID: 3854 RVA: 0x0002890E File Offset: 0x00026B0E
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FilterableAttribute.Default);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> supports device filtering.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> contained in the <paramref name="instance" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="instance">The <see cref="T:System.Object" /> to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is null.</exception>
		// Token: 0x06000F0F RID: 3855 RVA: 0x0002891B File Offset: 0x00026B1B
		public static bool IsObjectFilterable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return FilterableAttribute.IsTypeFilterable(instance.GetType());
		}

		/// <summary>Determines whether a property supports device filtering.</summary>
		/// <returns>true if the property represented by the <see cref="T:System.ComponentModel.PropertyDescriptor" /> object contained in the <paramref name="propertyDescriptor" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="propertyDescriptor">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the properties of the property to test.</param>
		// Token: 0x06000F10 RID: 3856 RVA: 0x00028938 File Offset: 0x00026B38
		public static bool IsPropertyFilterable(PropertyDescriptor propertyDescriptor)
		{
			FilterableAttribute filterableAttribute = (FilterableAttribute)propertyDescriptor.Attributes[typeof(FilterableAttribute)];
			return filterableAttribute == null || filterableAttribute.Filterable;
		}

		/// <summary>Determines whether the specified data type supports device filtering.</summary>
		/// <returns>true if the data type contained in the <paramref name="type" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		// Token: 0x06000F11 RID: 3857 RVA: 0x0002896C File Offset: 0x00026B6C
		public static bool IsTypeFilterable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = FilterableAttribute._filterableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			FilterableAttribute filterableAttribute = (FilterableAttribute)TypeDescriptor.GetAttributes(type)[typeof(FilterableAttribute)];
			obj = filterableAttribute != null && filterableAttribute.Filterable;
			FilterableAttribute._filterableTypes[type] = obj;
			return (bool)obj;
		}

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property supports device filtering. This field is read-only.</summary>
		// Token: 0x0400123E RID: 4670
		public static readonly FilterableAttribute Yes = new FilterableAttribute(true);

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property does not support device filtering. This field is read-only.</summary>
		// Token: 0x0400123F RID: 4671
		public static readonly FilterableAttribute No = new FilterableAttribute(false);

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object with default property settings. This field is read-only.</summary>
		// Token: 0x04001240 RID: 4672
		public static readonly FilterableAttribute Default = FilterableAttribute.Yes;

		// Token: 0x04001241 RID: 4673
		private bool _filterable;

		// Token: 0x04001242 RID: 4674
		private static Hashtable _filterableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
