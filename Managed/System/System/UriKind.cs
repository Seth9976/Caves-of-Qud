using System;

namespace System
{
	/// <summary>Defines the kinds of <see cref="T:System.Uri" />s for the <see cref="M:System.Uri.IsWellFormedUriString(System.String,System.UriKind)" /> and several <see cref="Overload:System.Uri.#ctor" /> methods.</summary>
	// Token: 0x02000159 RID: 345
	public enum UriKind
	{
		/// <summary>The kind of the Uri is indeterminate.</summary>
		// Token: 0x04000630 RID: 1584
		RelativeOrAbsolute,
		/// <summary>The Uri is an absolute Uri.</summary>
		// Token: 0x04000631 RID: 1585
		Absolute,
		/// <summary>The Uri is a relative Uri.</summary>
		// Token: 0x04000632 RID: 1586
		Relative
	}
}
