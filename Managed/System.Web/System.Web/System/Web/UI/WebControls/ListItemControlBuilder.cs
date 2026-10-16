using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.ListItem" /> control.</summary>
	// Token: 0x020003BE RID: 958
	public class ListItemControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether white spaces in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are represented by <see cref="T:System.Web.UI.LiteralControl" /> objects.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x060027D8 RID: 10200 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Determines whether HTML entities in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are converted to their equivalent characters when the text is parsed.</summary>
		/// <returns>true for all cases.</returns>
		// Token: 0x060027D9 RID: 10201 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
