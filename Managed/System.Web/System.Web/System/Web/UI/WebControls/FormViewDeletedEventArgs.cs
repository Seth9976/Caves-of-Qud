using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleted" /> event.</summary>
	// Token: 0x02000399 RID: 921
	public class FormViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x060024CD RID: 9421 RVA: 0x0005DFA1 File Offset: 0x0005C1A1
		public FormViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0005DFB7 File Offset: 0x0005C1B7
		internal FormViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x0005DFD0 File Offset: 0x0005C1D0
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0005DFD8 File Offset: 0x0005C1D8
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x0005DFE0 File Offset: 0x0005C1E0
		// (set) Token: 0x060024D2 RID: 9426 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
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
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x0005DFF1 File Offset: 0x0005C1F1
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the deleted record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0005DFF9 File Offset: 0x0005C1F9
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019D6 RID: 6614
		private int rowsAffected;

		// Token: 0x040019D7 RID: 6615
		private Exception e;

		// Token: 0x040019D8 RID: 6616
		private bool exceptionHandled;

		// Token: 0x040019D9 RID: 6617
		private IOrderedDictionary keys;

		// Token: 0x040019DA RID: 6618
		private IOrderedDictionary values;
	}
}
