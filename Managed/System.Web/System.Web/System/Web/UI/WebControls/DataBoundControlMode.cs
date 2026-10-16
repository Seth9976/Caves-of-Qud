using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different data-entry modes for a data-bound control or a particular field in ASP.NET Dynamic Data.</summary>
	// Token: 0x02000288 RID: 648
	public enum DataBoundControlMode
	{
		/// <summary>Represents the display mode, which prevents the user from modifying the values of a record or a data field.</summary>
		// Token: 0x0400168B RID: 5771
		ReadOnly,
		/// <summary>Represents the edit mode, which enables users to update the values of an existing record or data field. </summary>
		// Token: 0x0400168C RID: 5772
		Edit,
		/// <summary>Represents the insert mode, which enables users to enter values for a new record or data field.</summary>
		// Token: 0x0400168D RID: 5773
		Insert
	}
}
