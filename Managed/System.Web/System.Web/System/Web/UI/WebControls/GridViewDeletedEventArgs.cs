using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleted" /> event.</summary>
	// Token: 0x020003A2 RID: 930
	public class GridViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x060025E7 RID: 9703 RVA: 0x00061BF3 File Offset: 0x0005FDF3
		public GridViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00061C10 File Offset: 0x0005FE10
		internal GridViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x00061C29 File Offset: 0x0005FE29
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x00061C31 File Offset: 0x0005FE31
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x00061C39 File Offset: 0x0005FE39
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x00061C41 File Offset: 0x0005FE41
		public bool ExceptionHandled
		{
			get
			{
				return this.exceptionHandled;
			}
			set
			{
				this.exceptionHandled = value;
			}
		}

		/// <summary>Gets an ordered dictionary of key field name/value pairs for the deleted record.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x00061C4A File Offset: 0x0005FE4A
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the deleted record.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x00061C52 File Offset: 0x0005FE52
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001A30 RID: 6704
		private int rowsAffected;

		// Token: 0x04001A31 RID: 6705
		private Exception e;

		// Token: 0x04001A32 RID: 6706
		private bool exceptionHandled;

		// Token: 0x04001A33 RID: 6707
		private IOrderedDictionary keys;

		// Token: 0x04001A34 RID: 6708
		private IOrderedDictionary values;
	}
}
