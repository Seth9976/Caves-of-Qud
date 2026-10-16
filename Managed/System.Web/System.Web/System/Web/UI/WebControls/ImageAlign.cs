using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the alignment of an image in relation to the text of a Web page.</summary>
	// Token: 0x020002D5 RID: 725
	public enum ImageAlign
	{
		/// <summary>The alignment is not set.</summary>
		// Token: 0x040016EF RID: 5871
		NotSet,
		/// <summary>The image is aligned on the left edge of the Web page with text wrapping on the right.</summary>
		// Token: 0x040016F0 RID: 5872
		Left,
		/// <summary>The image is aligned on the right edge of the Web page with text wrapping on the left.</summary>
		// Token: 0x040016F1 RID: 5873
		Right,
		/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016F2 RID: 5874
		Baseline,
		/// <summary>The upper edge of the image is aligned with the upper edge of the highest element on the same line.</summary>
		// Token: 0x040016F3 RID: 5875
		Top,
		/// <summary>The middle of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016F4 RID: 5876
		Middle,
		/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016F5 RID: 5877
		Bottom,
		/// <summary>The lower edge of the image is aligned with the lower edge of the largest element on the same line.</summary>
		// Token: 0x040016F6 RID: 5878
		AbsBottom,
		/// <summary>The middle of the image is aligned with the middle of the largest element on the same line.</summary>
		// Token: 0x040016F7 RID: 5879
		AbsMiddle,
		/// <summary>The upper edge of the image is aligned with the upper edge of the highest text on the same line.</summary>
		// Token: 0x040016F8 RID: 5880
		TextTop
	}
}
