using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x0200039F RID: 927
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to. </param>
		// Token: 0x06001E45 RID: 7749 RVA: 0x0006BA27 File Offset: 0x00069C27
		public DefaultPropertyAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0006BA36 File Offset: 0x00069C36
		public string Name { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001E47 RID: 7751 RVA: 0x0006BA40 File Offset: 0x00069C40
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E48 RID: 7752 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x04000F3D RID: 3901
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
