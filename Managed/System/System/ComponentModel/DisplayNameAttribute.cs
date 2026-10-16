using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments. </summary>
	// Token: 0x0200036E RID: 878
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x06001D06 RID: 7430 RVA: 0x00068296 File Offset: 0x00066496
		public DisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x06001D07 RID: 7431 RVA: 0x000682A3 File Offset: 0x000664A3
		public DisplayNameAttribute(string displayName)
		{
			this.DisplayNameValue = displayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000682B2 File Offset: 0x000664B2
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000682BA File Offset: 0x000664BA
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x000682C2 File Offset: 0x000664C2
		protected string DisplayNameValue { get; set; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		// Token: 0x06001D0B RID: 7435 RVA: 0x000682CC File Offset: 0x000664CC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x06001D0C RID: 7436 RVA: 0x000682FC File Offset: 0x000664FC
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001D0D RID: 7437 RVA: 0x00068309 File Offset: 0x00066509
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x04000ED8 RID: 3800
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();
	}
}
