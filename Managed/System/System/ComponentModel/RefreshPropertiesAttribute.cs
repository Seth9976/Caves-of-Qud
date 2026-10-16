using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the property grid should refresh when the associated property value changes. This class cannot be inherited.</summary>
	// Token: 0x0200043A RID: 1082
	[AttributeUsage(AttributeTargets.All)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshPropertiesAttribute" /> class.</summary>
		/// <param name="refresh">A <see cref="T:System.ComponentModel.RefreshProperties" /> value indicating the nature of the refresh.</param>
		// Token: 0x0600236F RID: 9071 RVA: 0x00080CF8 File Offset: 0x0007EEF8
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.refresh = refresh;
		}

		/// <summary>Gets the refresh properties for the member.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.RefreshProperties" /> that indicates the current refresh properties for the member.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x00080D07 File Offset: 0x0007EF07
		public RefreshProperties RefreshProperties
		{
			get
			{
				return this.refresh;
			}
		}

		/// <summary>Overrides the object's <see cref="Overload:System.Object.Equals" /> method.</summary>
		/// <returns>true if the specified object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test for equality. </param>
		// Token: 0x06002371 RID: 9073 RVA: 0x00080D0F File Offset: 0x0007EF0F
		public override bool Equals(object value)
		{
			return value is RefreshPropertiesAttribute && ((RefreshPropertiesAttribute)value).RefreshProperties == this.refresh;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for the object that the attribute belongs to.</returns>
		// Token: 0x06002372 RID: 9074 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06002373 RID: 9075 RVA: 0x00080D2E File Offset: 0x0007EF2E
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RefreshPropertiesAttribute.Default);
		}

		/// <summary>Indicates that all properties are queried again and refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010C4 RID: 4292
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		/// <summary>Indicates that all properties are repainted if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010C5 RID: 4293
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);

		/// <summary>Indicates that no other properties are refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010C6 RID: 4294
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);

		// Token: 0x040010C7 RID: 4295
		private RefreshProperties refresh;
	}
}
