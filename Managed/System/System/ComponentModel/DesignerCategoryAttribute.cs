using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x0200036B RID: 875
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x00068129 File Offset: 0x00066329
		public DesignerCategoryAttribute()
		{
			this.Category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006813C File Offset: 0x0006633C
		public DesignerCategoryAttribute(string category)
		{
			this.Category = category;
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0006814B File Offset: 0x0006634B
		public string Category { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001CFB RID: 7419 RVA: 0x00068154 File Offset: 0x00066354
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.Category == this.Category;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CFC RID: 7420 RVA: 0x00068184 File Offset: 0x00066384
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001CFD RID: 7421 RVA: 0x00068191 File Offset: 0x00066391
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000681A8 File Offset: 0x000663A8
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.Category;
			}
		}

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x04000ECA RID: 3786
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This static field is read-only.</summary>
		// Token: 0x04000ECB RID: 3787
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		/// <summary>Specifies that a component marked with this category use a form designer. This static field is read-only.</summary>
		// Token: 0x04000ECC RID: 3788
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This static field is read-only.</summary>
		// Token: 0x04000ECD RID: 3789
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
