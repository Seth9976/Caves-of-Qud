using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserted" /> event.</summary>
	// Token: 0x0200039B RID: 923
	public class FormViewInsertedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewInsertedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the insert operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the insert operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x060024D9 RID: 9433 RVA: 0x0005E036 File Offset: 0x0005C236
		public FormViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0005E04C File Offset: 0x0005C24C
		internal FormViewInsertedEventArgs(int affectedRows, Exception e, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the insert operation.</summary>
		/// <returns>The number of rows affected by the insert operation.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0005E05D File Offset: 0x0005C25D
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the insert operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the insert operation.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0005E065 File Offset: 0x0005C265
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the insert operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x0005E06D File Offset: 0x0005C26D
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x0005E075 File Offset: 0x0005C275
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FormView" /> control should remain in insert mode after an insert operation.</summary>
		/// <returns>true to remain in insert mode after an insert operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x0005E07E File Offset: 0x0005C27E
		// (set) Token: 0x060024E0 RID: 9440 RVA: 0x0005E086 File Offset: 0x0005C286
		public bool KeepInInsertMode
		{
			get
			{
				return this.keepInsertedMode;
			}
			set
			{
				this.keepInsertedMode = value;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the inserted record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains a dictionary of key field name/value pairs for the inserted record.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0005E08F File Offset: 0x0005C28F
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019DD RID: 6621
		private int rowsAffected;

		// Token: 0x040019DE RID: 6622
		private Exception e;

		// Token: 0x040019DF RID: 6623
		private bool exceptionHandled;

		// Token: 0x040019E0 RID: 6624
		private bool keepInsertedMode;

		// Token: 0x040019E1 RID: 6625
		private IOrderedDictionary values;
	}
}
