using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
	// Token: 0x020003B9 RID: 953
	public class LinkButtonControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white space literals are allowed.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x06002750 RID: 10064 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
