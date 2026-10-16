using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	// Token: 0x02000426 RID: 1062
	public class TextBoxControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white-space literals are allowed.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x06002FFF RID: 12287 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Determines whether the literal string of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control must be HTML decoded.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x06003000 RID: 12288 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
