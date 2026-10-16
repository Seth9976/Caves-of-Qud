using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Specifies the kind of border that surrounds a Web Parts control.</summary>
	// Token: 0x02000468 RID: 1128
	public enum PartChromeType
	{
		/// <summary>A border setting inherited from the part control's containing zone.</summary>
		// Token: 0x04001CDB RID: 7387
		Default,
		/// <summary>A title bar and a border.</summary>
		// Token: 0x04001CDC RID: 7388
		TitleAndBorder,
		/// <summary>No border and no title bar.</summary>
		// Token: 0x04001CDD RID: 7389
		None,
		/// <summary>A title bar only, without a border.</summary>
		// Token: 0x04001CDE RID: 7390
		TitleOnly,
		/// <summary>A border only, without a title bar.</summary>
		// Token: 0x04001CDF RID: 7391
		BorderOnly
	}
}
