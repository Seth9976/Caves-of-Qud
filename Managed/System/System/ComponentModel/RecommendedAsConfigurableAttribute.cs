using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x020003E6 RID: 998
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">true if the property this attribute is bound to can be used as an application setting; otherwise, false. </param>
		// Token: 0x060020B4 RID: 8372 RVA: 0x00070CC4 File Offset: 0x0006EEC4
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.RecommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>true if the property this attribute is bound to can be used as an application setting; otherwise, false.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x00070CD3 File Offset: 0x0006EED3
		public bool RecommendedAsConfigurable { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x060020B6 RID: 8374 RVA: 0x00070CDC File Offset: 0x0006EEDC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.RecommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x060020B7 RID: 8375 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x060020B8 RID: 8376 RVA: 0x00070D09 File Offset: 0x0006EF09
		public override bool IsDefaultAttribute()
		{
			return !this.RecommendedAsConfigurable;
		}

		/// <summary>Specifies that a property cannot be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000FF6 RID: 4086
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This static field is read-only.</summary>
		// Token: 0x04000FF7 RID: 4087
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000FF8 RID: 4088
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
