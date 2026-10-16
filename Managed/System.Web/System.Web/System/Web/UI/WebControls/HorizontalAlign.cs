using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the horizontal alignment of items within a container.</summary>
	// Token: 0x020002C8 RID: 712
	[TypeConverter(typeof(HorizontalAlignConverter))]
	public enum HorizontalAlign
	{
		/// <summary>The horizontal alignment is not set.</summary>
		// Token: 0x040016E4 RID: 5860
		NotSet,
		/// <summary>The contents of a container are left justified.</summary>
		// Token: 0x040016E5 RID: 5861
		Left,
		/// <summary>The contents of a container are centered.</summary>
		// Token: 0x040016E6 RID: 5862
		Center,
		/// <summary>The contents of a container are right justified.</summary>
		// Token: 0x040016E7 RID: 5863
		Right,
		/// <summary>The contents of a container are uniformly spread out and aligned with both the left and right margins.</summary>
		// Token: 0x040016E8 RID: 5864
		Justify
	}
}
