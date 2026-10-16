using System;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Selecting" /> event of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	// Token: 0x0200030D RID: 781
	public class SqlDataSourceSelectingEventArgs : SqlDataSourceCommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> class, using the specified <see cref="T:System.Data.Common.DbCommand" /> object and any <see cref="T:System.Web.UI.DataSourceSelectArguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <param name="command">An <see cref="T:System.Data.Common.DbCommand" /> object that represents the cancelable <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> query.</param>
		/// <param name="arguments">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</param>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x00043897 File Offset: 0x00041A97
		public SqlDataSourceSelectingEventArgs(DbCommand command, DataSourceSelectArguments arguments)
			: base(command)
		{
			this._arguments = arguments;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object, or null if no <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object is specified during <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> creation.</returns>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000438A7 File Offset: 0x00041AA7
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x04001763 RID: 5987
		private DataSourceSelectArguments _arguments;
	}
}
