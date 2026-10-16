using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web server controls and their members use to indicate whether their rendering can be affected by themes and control skins. This class cannot be inherited.</summary>
	// Token: 0x02000191 RID: 401
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ThemeableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a type or member that is affected by themes and control skins.</summary>
		/// <param name="themeable">true to initialize the <see cref="T:System.Web.UI.ThemeableAttribute" /> to represent a type or member that can be affected by themes; otherwise, false.</param>
		// Token: 0x06000F8B RID: 3979 RVA: 0x00028D28 File Offset: 0x00026F28
		public ThemeableAttribute(bool themeable)
		{
			this._themeable = themeable;
		}

		/// <summary>Gets a value indicating whether the current control or member of a control can be affected by themes and control skins defined for the Web application.</summary>
		/// <returns>true if the current type or member can be affected by themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00028D37 File Offset: 0x00026F37
		public bool Themeable
		{
			get
			{
				return this._themeable;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is the same instance as the current instance, or if the instances are different, but the attribute values are equivalent; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06000F8D RID: 3981 RVA: 0x00028D40 File Offset: 0x00026F40
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ThemeableAttribute themeableAttribute = obj as ThemeableAttribute;
			return themeableAttribute != null && themeableAttribute.Themeable == this._themeable;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ThemeableAttribute" /> type. </summary>
		/// <returns>A hash code for the current <see cref="T:System.Web.UI.ThemeableAttribute" />.</returns>
		// Token: 0x06000F8E RID: 3982 RVA: 0x00028D6D File Offset: 0x00026F6D
		public override int GetHashCode()
		{
			return this._themeable.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class.</summary>
		/// <returns>true if the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the class; otherwise, false.</returns>
		// Token: 0x06000F8F RID: 3983 RVA: 0x00028D7A File Offset: 0x00026F7A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ThemeableAttribute.Default);
		}

		/// <summary>Returns a value indicating whether the object passed to the method supports themes. </summary>
		/// <returns>true if the object supports themes and control skins; otherwise, false.</returns>
		/// <param name="instance">The object to test for themes support.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is null.</exception>
		// Token: 0x06000F90 RID: 3984 RVA: 0x00028D87 File Offset: 0x00026F87
		public static bool IsObjectThemeable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return ThemeableAttribute.IsTypeThemeable(instance.GetType());
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Type" /> passed to the method supports themes.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> supports themes and control skins; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to test for themes support.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		// Token: 0x06000F91 RID: 3985 RVA: 0x00028DA4 File Offset: 0x00026FA4
		public static bool IsTypeThemeable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = ThemeableAttribute._themeableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			ThemeableAttribute themeableAttribute = Attribute.GetCustomAttribute(type, typeof(ThemeableAttribute)) as ThemeableAttribute;
			obj = themeableAttribute != null && themeableAttribute.Themeable;
			ThemeableAttribute._themeableTypes[type] = obj;
			return (bool)obj;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is affected by themes and control skins.</summary>
		// Token: 0x0400132A RID: 4906
		public static readonly ThemeableAttribute Yes = new ThemeableAttribute(true);

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is not affected by themes and control skins.</summary>
		// Token: 0x0400132B RID: 4907
		public static readonly ThemeableAttribute No = new ThemeableAttribute(false);

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance that represents the application-defined default value of the attribute.</summary>
		// Token: 0x0400132C RID: 4908
		public static readonly ThemeableAttribute Default = ThemeableAttribute.Yes;

		// Token: 0x0400132D RID: 4909
		private bool _themeable;

		// Token: 0x0400132E RID: 4910
		private static Hashtable _themeableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
