using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Exposes the common properties of data-bound-controls that display multiple rows. </summary>
	// Token: 0x020002CF RID: 719
	public interface IDataBoundListControl : IDataBoundControl
	{
		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that represent the data key value of each row in a data-bound control.</summary>
		/// <returns>A collection of data-key objects that contains the data-key value of each row in a data-bound control.</returns>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001B2E RID: 6958
		DataKeyArray DataKeys { get; }

		/// <summary>Gets the object that contains the data-key value for the selected row in a data-bound control.</summary>
		/// <returns>The object that contains the data-key value for the selected row in a data-bound control.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001B2F RID: 6959
		DataKey SelectedDataKey { get; }

		/// <summary>Gets or sets the index of the selected row in a data-bound control.</summary>
		/// <returns>The index of the selected row in a data-bound control.</returns>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001B30 RID: 6960
		// (set) Token: 0x06001B31 RID: 6961
		int SelectedIndex { get; set; }

		/// <summary>Gets or sets the names of the data fields whose values are appended to the <see cref="P:System.Web.UI.Control.ClientID" /> property value to uniquely identify each instance of a data-bound control.</summary>
		/// <returns>An array of data field names.</returns>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001B32 RID: 6962
		// (set) Token: 0x06001B33 RID: 6963
		string[] ClientIDRowSuffix { get; set; }

		/// <summary>Gets or sets a value that indicates whether the selection of a row is based on index or on data-key values.</summary>
		/// <returns>true if the row selection is based on data-key values; otherwise, false.</returns>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001B34 RID: 6964
		// (set) Token: 0x06001B35 RID: 6965
		bool EnablePersistedSelection { get; set; }
	}
}
