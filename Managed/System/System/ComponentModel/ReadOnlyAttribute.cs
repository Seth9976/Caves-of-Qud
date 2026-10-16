using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">true to show that the property this attribute is bound to is read-only; false to show that the property is read/write. </param>
		// Token: 0x06001D41 RID: 7489 RVA: 0x000686B7 File Offset: 0x000668B7
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.IsReadOnly = isReadOnly;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>true if the property this attribute is bound to is read-only; false if the property is read/write.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x000686C6 File Offset: 0x000668C6
		public bool IsReadOnly { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06001D43 RID: 7491 RVA: 0x000686D0 File Offset: 0x000668D0
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			bool? flag = ((readOnlyAttribute != null) ? new bool?(readOnlyAttribute.IsReadOnly) : null);
			bool isReadOnly = this.IsReadOnly;
			return (flag.GetValueOrDefault() == isReadOnly) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x06001D44 RID: 7492 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001D45 RID: 7493 RVA: 0x0006871C File Offset: 0x0006691C
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This static field is read-only.</summary>
		// Token: 0x04000EEC RID: 3820
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This static field is read-only.</summary>
		// Token: 0x04000EED RID: 3821
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This static field is read-only.</summary>
		// Token: 0x04000EEE RID: 3822
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
