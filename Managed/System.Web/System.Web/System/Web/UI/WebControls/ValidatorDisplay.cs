using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the display behavior of error messages in validation controls.</summary>
	// Token: 0x02000322 RID: 802
	public enum ValidatorDisplay
	{
		/// <summary>Validator content never displayed inline.</summary>
		// Token: 0x040017C7 RID: 6087
		None,
		/// <summary>Validator content physically part of the page layout.</summary>
		// Token: 0x040017C8 RID: 6088
		Static,
		/// <summary>Validator content dynamically added to the page when validation fails.</summary>
		// Token: 0x040017C9 RID: 6089
		Dynamic
	}
}
