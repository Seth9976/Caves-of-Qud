using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x02000378 RID: 888
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">true if a property should be localized; otherwise, false. </param>
		// Token: 0x06001D35 RID: 7477 RVA: 0x00068586 File Offset: 0x00066786
		public LocalizableAttribute(bool isLocalizable)
		{
			this.IsLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>true if a property should be localized; otherwise, false.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00068595 File Offset: 0x00066795
		public bool IsLocalizable { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001D37 RID: 7479 RVA: 0x000685A0 File Offset: 0x000667A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			bool? flag = ((localizableAttribute != null) ? new bool?(localizableAttribute.IsLocalizable) : null);
			bool isLocalizable = this.IsLocalizable;
			return (flag.GetValueOrDefault() == isLocalizable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06001D38 RID: 7480 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001D39 RID: 7481 RVA: 0x000685EC File Offset: 0x000667EC
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		/// <summary>Specifies that a property should be localized. This static field is read-only.</summary>
		// Token: 0x04000EE5 RID: 3813
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This static field is read-only.</summary>
		// Token: 0x04000EE6 RID: 3814
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000EE7 RID: 3815
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
