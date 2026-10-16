using System;

namespace System.Web.UI
{
	/// <summary>Provides a way to request processing beyond record retrieval for a data retrieval operation of a data source control.</summary>
	// Token: 0x02000157 RID: 343
	[Flags]
	public enum DataSourceCapabilities
	{
		/// <summary>Represents no paging, sorting, or total row count retrieval capabilities.</summary>
		// Token: 0x0400122F RID: 4655
		None = 0,
		/// <summary>Represents the capability to sort through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
		// Token: 0x04001230 RID: 4656
		Sort = 1,
		/// <summary>Represents the capability to page through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
		// Token: 0x04001231 RID: 4657
		Page = 2,
		/// <summary>Represents the capability to retrieve a total row count of data, which corresponds to using the <see cref="F:System.Web.UI.DataSourceOperation.SelectCount" /> value. </summary>
		// Token: 0x04001232 RID: 4658
		RetrieveTotalRowCount = 4
	}
}
