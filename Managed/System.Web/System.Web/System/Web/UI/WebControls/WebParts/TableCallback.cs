using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>References the method to call when retrieving table data from a provider.</summary>
	/// <param name="tableData">The data to retrieve from the provider.</param>
	// Token: 0x0200046B RID: 1131
	// (Invoke) Token: 0x060033CB RID: 13259
	public delegate void TableCallback(ICollection tableData);
}
