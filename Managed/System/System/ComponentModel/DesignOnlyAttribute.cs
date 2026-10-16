using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x0200036A RID: 874
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">true if a property can be set only at design time; false if the property can be set at design time and at run time. </param>
		// Token: 0x06001CF2 RID: 7410 RVA: 0x00068072 File Offset: 0x00066272
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.IsDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>true if a property can be set only at design time; otherwise, false.</returns>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00068081 File Offset: 0x00066281
		public bool IsDesignOnly { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001CF4 RID: 7412 RVA: 0x0006808C File Offset: 0x0006628C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			bool? flag = ((designOnlyAttribute != null) ? new bool?(designOnlyAttribute.IsDesignOnly) : null);
			bool isDesignOnly = this.IsDesignOnly;
			return (flag.GetValueOrDefault() == isDesignOnly) & (flag != null);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000680D8 File Offset: 0x000662D8
		public override int GetHashCode()
		{
			return this.IsDesignOnly.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x000680F3 File Offset: 0x000662F3
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		/// <summary>Specifies that a property can be set only at design time. This static field is read-only.</summary>
		// Token: 0x04000EC7 RID: 3783
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This static field is read-only.</summary>
		// Token: 0x04000EC8 RID: 3784
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000EC9 RID: 3785
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
