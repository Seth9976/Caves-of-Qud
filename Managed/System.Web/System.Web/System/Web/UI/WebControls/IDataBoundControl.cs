using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines properties that are shared by data-bound controls.</summary>
	// Token: 0x020002CD RID: 717
	public interface IDataBoundControl
	{
		/// <summary>Gets or sets the ID of the data source control from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The ID of the data source control that contains the list of data items that the data-bound control retrieves.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001B23 RID: 6947
		// (set) Token: 0x06001B24 RID: 6948
		string DataSourceID { get; set; }

		/// <summary>Gets the data source object from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The data source object that contains the list of data items that the data-bound control retrieves. </returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001B25 RID: 6949
		IDataSource DataSourceObject { get; }

		/// <summary>Gets or sets the object from which the data-bound control retrieves a list of data items.</summary>
		/// <returns>The object that contains the list of data that the data-bound control retrieves.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001B26 RID: 6950
		// (set) Token: 0x06001B27 RID: 6951
		object DataSource { get; set; }

		/// <summary>Gets or sets an array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</summary>
		/// <returns>An array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001B28 RID: 6952
		// (set) Token: 0x06001B29 RID: 6953
		string[] DataKeyNames { get; set; }

		/// <summary>Gets or sets the name of the list of data that the data-bound control binds to when the data source contains more than one list of data items.</summary>
		/// <returns>The name of the list of data that the data-bound control binds to.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001B2A RID: 6954
		// (set) Token: 0x06001B2B RID: 6955
		string DataMember { get; set; }
	}
}
