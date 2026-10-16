using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Updating" />, <see cref="E:System.Web.UI.WebControls.SqlDataSource.Deleting" /> and <see cref="E:System.Web.UI.WebControls.SqlDataSource.Inserting" /> events of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	// Token: 0x02000307 RID: 775
	public class SqlDataSourceCommandEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> class, using the specified database command object.</summary>
		/// <param name="command">An <see cref="T:System.Data.Common.DbCommand" /> object that represents the cancelable <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" />, <see cref="M:System.Web.UI.WebControls.SqlDataSource.Insert" />, or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> command. </param>
		// Token: 0x06001BBC RID: 7100 RVA: 0x00043869 File Offset: 0x00041A69
		public SqlDataSourceCommandEventArgs(DbCommand command)
		{
			this._command = command;
		}

		/// <summary>Gets the pending database command.</summary>
		/// <returns>An <see cref="T:System.Data.Common.DbCommand" /> object that represents the pending database command.</returns>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00043878 File Offset: 0x00041A78
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x0400175B RID: 5979
		private DbCommand _command;
	}
}
