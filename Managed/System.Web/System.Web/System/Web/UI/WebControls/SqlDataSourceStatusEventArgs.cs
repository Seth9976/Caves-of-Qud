using System;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for an event that is raised by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control after a data operation has completed.</summary>
	// Token: 0x0200030F RID: 783
	public class SqlDataSourceStatusEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> class, using the specified output parameters, return value, and number of rows affected by the database operation.</summary>
		/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> that represents the database query, command, or stored procedure that is submitted to the database by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control. </param>
		/// <param name="affectedRows">The number of rows affected by the database operation, if applicable. </param>
		/// <param name="exception">An <see cref="T:System.Exception" /> thrown by the database operation, if applicable.</param>
		// Token: 0x06001BCE RID: 7118 RVA: 0x000438AF File Offset: 0x00041AAF
		public SqlDataSourceStatusEventArgs(DbCommand command, int affectedRows, Exception exception)
		{
			this._command = command;
			this._affectedRows = affectedRows;
			this._exception = exception;
		}

		/// <summary>Gets the number of rows affected by a database operation.</summary>
		/// <returns>The number of rows affected by a database operation. The default value is -1.</returns>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x000438CC File Offset: 0x00041ACC
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		/// <summary>Gets the database command submitted to the database.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbCommand" /> object that represents the database command submitted to the database.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000438D4 File Offset: 0x00041AD4
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Gets a wrapper for any exceptions thrown by the database during a data operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the database in its <see cref="P:System.Exception.InnerException" /> property.</returns>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000438DC File Offset: 0x00041ADC
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception thrown by the database has been handled.</summary>
		/// <returns>true if an exception thrown by the database has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control; otherwise, false.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000438E4 File Offset: 0x00041AE4
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x000438EC File Offset: 0x00041AEC
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x04001764 RID: 5988
		private DbCommand _command;

		// Token: 0x04001765 RID: 5989
		private Exception _exception;

		// Token: 0x04001766 RID: 5990
		private bool _exceptionHandled;

		// Token: 0x04001767 RID: 5991
		private int _affectedRows;
	}
}
