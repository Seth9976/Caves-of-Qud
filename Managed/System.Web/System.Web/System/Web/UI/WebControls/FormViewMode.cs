using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	// Token: 0x020002AE RID: 686
	public enum FormViewMode
	{
		/// <summary>A display mode that prevents the user from modifying the values of a record.</summary>
		// Token: 0x040016CE RID: 5838
		ReadOnly,
		/// <summary>An editing mode that allows the user to update the values of an existing record.</summary>
		// Token: 0x040016CF RID: 5839
		Edit,
		/// <summary>An inserting mode that allows the user to enter the values for a new record.</summary>
		// Token: 0x040016D0 RID: 5840
		Insert
	}
}
