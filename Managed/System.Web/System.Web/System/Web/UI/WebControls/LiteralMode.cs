using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies how the content in a <see cref="T:System.Web.UI.WebControls.Literal" /> control is rendered.</summary>
	// Token: 0x020002DA RID: 730
	public enum LiteralMode
	{
		/// <summary>The literal control's unsupported markup-language elements are removed. If the literal control is rendered on a browser that supports HTML or XHTML, the control's contents are not modified.</summary>
		// Token: 0x04001707 RID: 5895
		Transform,
		/// <summary>The literal control's contents are not modified.</summary>
		// Token: 0x04001708 RID: 5896
		PassThrough,
		/// <summary>The literal control's contents are HTML-encoded.</summary>
		// Token: 0x04001709 RID: 5897
		Encode
	}
}
