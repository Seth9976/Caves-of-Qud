using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x020003A1 RID: 929
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class.</summary>
		// Token: 0x06001E55 RID: 7765 RVA: 0x0006BB17 File Offset: 0x00069D17
		public InheritanceAttribute()
		{
			this.InheritanceLevel = InheritanceAttribute.Default.InheritanceLevel;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to. </param>
		// Token: 0x06001E56 RID: 7766 RVA: 0x0006BB2F File Offset: 0x00069D2F
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.InheritanceLevel = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0006BB3E File Offset: 0x00069D3E
		public InheritanceLevel InheritanceLevel { get; }

		/// <summary>Override to test for equality.</summary>
		/// <returns>true if the object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test. </param>
		// Token: 0x06001E58 RID: 7768 RVA: 0x0006BB46 File Offset: 0x00069D46
		public override bool Equals(object value)
		{
			return value == this || (value is InheritanceAttribute && ((InheritanceAttribute)value).InheritanceLevel == this.InheritanceLevel);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06001E59 RID: 7769 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06001E5A RID: 7770 RVA: 0x0006BB6B File Offset: 0x00069D6B
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001E5B RID: 7771 RVA: 0x0006BB78 File Offset: 0x00069D78
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x04000F3F RID: 3903
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x04000F40 RID: 3904
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x04000F41 RID: 3905
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x04000F42 RID: 3906
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;
	}
}
