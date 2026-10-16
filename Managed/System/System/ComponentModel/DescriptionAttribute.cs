using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x02000369 RID: 873
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x00067FE7 File Offset: 0x000661E7
		public DescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text. </param>
		// Token: 0x06001CEA RID: 7402 RVA: 0x00067FF4 File Offset: 0x000661F4
		public DescriptionAttribute(string description)
		{
			this.DescriptionValue = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x00068003 File Offset: 0x00066203
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0006800B File Offset: 0x0006620B
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00068013 File Offset: 0x00066213
		protected string DescriptionValue { get; set; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001CEE RID: 7406 RVA: 0x0006801C File Offset: 0x0006621C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0006804C File Offset: 0x0006624C
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>true, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, false.</returns>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x00068059 File Offset: 0x00066259
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This static field is read-only.</summary>
		// Token: 0x04000EC4 RID: 3780
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
