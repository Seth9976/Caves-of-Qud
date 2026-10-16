using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.PlaceHolder" /> control.</summary>
	// Token: 0x020003EF RID: 1007
	public class PlaceHolderControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white-space literals are allowed.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x06002C79 RID: 11385 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
