using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selecting" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002F0 RID: 752
	public class ObjectDataSourceSelectingEventArgs : ObjectDataSourceMethodEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs" /> class.</summary>
		/// <param name="inputParameters">An <see cref="T:System.Collections.IDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that specifies which additional data-related operations the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> should perform on a results set, such as sorting the data or returning a specific subset of data. </param>
		/// <param name="executingSelectCount">true to indicate the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving the number of rows of data, in addition to the data itself; otherwise, false.</param>
		// Token: 0x06001B89 RID: 7049 RVA: 0x00043755 File Offset: 0x00041955
		public ObjectDataSourceSelectingEventArgs(IOrderedDictionary inputParameters, DataSourceSelectArguments arguments, bool executingSelectCount)
			: base(inputParameters)
		{
			this._arguments = arguments;
			this._executingSelectCount = executingSelectCount;
		}

		/// <summary>Provides a mechanism that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object can use to request data-related operations when data is retrieved.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that specifies which additional data-related operations the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> should perform on a results set.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0004376C File Offset: 0x0004196C
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving a row count during a data retrieval operation.</summary>
		/// <returns>true, if data source paging is enabled and the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving a row count; otherwise, false.</returns>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00043774 File Offset: 0x00041974
		public bool ExecutingSelectCount
		{
			get
			{
				return this._executingSelectCount;
			}
		}

		// Token: 0x04001726 RID: 5926
		private DataSourceSelectArguments _arguments;

		// Token: 0x04001727 RID: 5927
		private bool _executingSelectCount;
	}
}
