using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the type of an item in a list control.</summary>
	// Token: 0x020002D8 RID: 728
	public enum ListItemType
	{
		/// <summary>A header for the list control. It is not data-bound.</summary>
		// Token: 0x040016FB RID: 5883
		Header,
		/// <summary>A footer for the list control. It is not data-bound.</summary>
		// Token: 0x040016FC RID: 5884
		Footer,
		/// <summary>An item in the list control. It is data-bound.</summary>
		// Token: 0x040016FD RID: 5885
		Item,
		/// <summary>An item in alternating (zero-based even-indexed) cells. It is data-bound.</summary>
		// Token: 0x040016FE RID: 5886
		AlternatingItem,
		/// <summary>A selected item in the list control. It is data-bound.</summary>
		// Token: 0x040016FF RID: 5887
		SelectedItem,
		/// <summary>An item in a list control currently in edit mode. It is data-bound.</summary>
		// Token: 0x04001700 RID: 5888
		EditItem,
		/// <summary>A separator between items in a list control. It is not data-bound.</summary>
		// Token: 0x04001701 RID: 5889
		Separator,
		/// <summary>A pager that displays the controls to navigate to different pages associated with the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. It is not data-bound.</summary>
		// Token: 0x04001702 RID: 5890
		Pager
	}
}
