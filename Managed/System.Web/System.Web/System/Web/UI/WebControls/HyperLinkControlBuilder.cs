using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
	// Token: 0x020003AE RID: 942
	public class HyperLinkControlBuilder : ControlBuilder
	{
		/// <summary>Gets a value that indicates whether white spaces are allowed in literals for this control.</summary>
		/// <returns>Overloaded to always returns false to indicate that white spaces are not allowed.</returns>
		// Token: 0x0600268C RID: 9868 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
