using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the layout of items in a list control.</summary>
	// Token: 0x020002FA RID: 762
	public enum RepeatLayout
	{
		/// <summary>Items are displayed in a table.</summary>
		// Token: 0x04001743 RID: 5955
		Table,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of a span element and items are separated by br elements.</summary>
		// Token: 0x04001744 RID: 5956
		Flow,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of a ul element that contains li elements.</summary>
		// Token: 0x04001745 RID: 5957
		UnorderedList,
		/// <summary>Items are displayed without a table structure. Rendered markup consists of an ol element that contains li elements.</summary>
		// Token: 0x04001746 RID: 5958
		OrderedList
	}
}
