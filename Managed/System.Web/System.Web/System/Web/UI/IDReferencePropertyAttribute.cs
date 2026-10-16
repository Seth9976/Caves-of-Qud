using System;

namespace System.Web.UI
{
	/// <summary>Defines an attribute applied to properties that contain ID references. This class cannot be inherited.</summary>
	// Token: 0x02000168 RID: 360
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IDReferencePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class.</summary>
		// Token: 0x06000F1E RID: 3870 RVA: 0x000289E2 File Offset: 0x00026BE2
		public IDReferencePropertyAttribute()
			: this(typeof(Control))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class using the specified type.</summary>
		/// <param name="referencedControlType">A <see cref="T:System.Type" /> that specifies the type of the control represented by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied.</param>
		// Token: 0x06000F1F RID: 3871 RVA: 0x000289F4 File Offset: 0x00026BF4
		public IDReferencePropertyAttribute(Type referencedControlType)
		{
			this._referencedControlType = referencedControlType;
		}

		/// <summary>Gets the type of the controls allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> attribute is applied.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of control allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied. The default is <see cref="T:System.Web.UI.Control" />.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00028A03 File Offset: 0x00026C03
		public Type ReferencedControlType
		{
			get
			{
				return this._referencedControlType;
			}
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F21 RID: 3873 RVA: 0x00028A0B File Offset: 0x00026C0B
		public override int GetHashCode()
		{
			if (!(this.ReferencedControlType != null))
			{
				return 0;
			}
			return this.ReferencedControlType.GetHashCode();
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x00028A28 File Offset: 0x00026C28
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IDReferencePropertyAttribute idreferencePropertyAttribute = obj as IDReferencePropertyAttribute;
			return idreferencePropertyAttribute != null && this.ReferencedControlType == idreferencePropertyAttribute.ReferencedControlType;
		}

		// Token: 0x04001308 RID: 4872
		private Type _referencedControlType;
	}
}
