using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the types of buttons to display for navigating between pages of content in a paginated control.</summary>
	// Token: 0x020002F5 RID: 757
	public enum PagerButtons
	{
		/// <summary>A set of pagination controls consisting of Previous and Next buttons.</summary>
		// Token: 0x04001731 RID: 5937
		NextPrevious,
		/// <summary>A set of pagination controls consisting of numbered link buttons to access pages directly.</summary>
		// Token: 0x04001732 RID: 5938
		Numeric,
		/// <summary>A set of pagination controls consisting of Previous, Next, First, and Last buttons.</summary>
		// Token: 0x04001733 RID: 5939
		NextPreviousFirstLast,
		/// <summary>A set of pagination controls consisting of numbered and First and Last link buttons.</summary>
		// Token: 0x04001734 RID: 5940
		NumericFirstLast
	}
}
