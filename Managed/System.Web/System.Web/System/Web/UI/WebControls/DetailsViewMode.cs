using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x020002A0 RID: 672
	public enum DetailsViewMode
	{
		/// <summary>A display mode that prevents the user from modifying the values of a record.</summary>
		// Token: 0x040016B2 RID: 5810
		ReadOnly,
		/// <summary>An editing mode that allows the user to update the values of an existing record.</summary>
		// Token: 0x040016B3 RID: 5811
		Edit,
		/// <summary>An inserting mode that allows the user to enter the values for a new record.</summary>
		// Token: 0x040016B4 RID: 5812
		Insert
	}
}
