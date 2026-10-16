using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the bullet styles you can apply to list items in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. </summary>
	// Token: 0x02000338 RID: 824
	public enum BulletStyle
	{
		/// <summary>The bullet style is not set. The browser that renders the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control will determine the bullet style to display.</summary>
		// Token: 0x0400181F RID: 6175
		NotSet,
		/// <summary>The bullet style is a number (1, 2, 3, ...).</summary>
		// Token: 0x04001820 RID: 6176
		Numbered,
		/// <summary>The bullet style is a lowercase letter (a, b, c, ...).</summary>
		// Token: 0x04001821 RID: 6177
		LowerAlpha,
		/// <summary>The bullet style is an uppercase letter (A, B, C, ...).</summary>
		// Token: 0x04001822 RID: 6178
		UpperAlpha,
		/// <summary>The bullet style is a lowercase Roman numeral (i, ii, iii, ...).</summary>
		// Token: 0x04001823 RID: 6179
		LowerRoman,
		/// <summary>The bullet style is an uppercase Roman numeral (I, II, III, ...).</summary>
		// Token: 0x04001824 RID: 6180
		UpperRoman,
		/// <summary>The bullet style is a filled circle shape.</summary>
		// Token: 0x04001825 RID: 6181
		Disc,
		/// <summary>The bullet style is an empty circle shape.</summary>
		// Token: 0x04001826 RID: 6182
		Circle,
		/// <summary>The bullet style is a filled square shape.</summary>
		// Token: 0x04001827 RID: 6183
		Square,
		/// <summary>The bullet style is a custom image.</summary>
		// Token: 0x04001828 RID: 6184
		CustomImage
	}
}
