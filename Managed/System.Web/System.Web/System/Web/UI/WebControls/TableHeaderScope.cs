using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the HTML scope attribute for classes that represent header cells in a table. </summary>
	// Token: 0x02000314 RID: 788
	public enum TableHeaderScope
	{
		/// <summary>The scope attribute is not rendered for the header cell.</summary>
		// Token: 0x0400176F RID: 5999
		NotSet,
		/// <summary>The object that represents a header cell of a table is rendered with the scope attribute set to "Row".</summary>
		// Token: 0x04001770 RID: 6000
		Row,
		/// <summary>The object that represents a header cell of a table is rendered with the scope attribute set to "Column".</summary>
		// Token: 0x04001771 RID: 6001
		Column
	}
}
