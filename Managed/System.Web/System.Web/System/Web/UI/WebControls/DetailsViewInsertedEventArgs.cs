using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserted" /> event.</summary>
	// Token: 0x02000386 RID: 902
	public class DetailsViewInsertedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the insert operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the insert operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x06002368 RID: 9064 RVA: 0x0005A385 File Offset: 0x00058585
		public DetailsViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepInsertedMode = false;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0005A3A9 File Offset: 0x000585A9
		internal DetailsViewInsertedEventArgs(int affectedRows, Exception e, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the insert operation.</summary>
		/// <returns>The number of rows affected by the insert operation.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x0005A3BA File Offset: 0x000585BA
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the insert operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the insert operation.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x0005A3C2 File Offset: 0x000585C2
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the insert operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x0005A3CA File Offset: 0x000585CA
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x0005A3D2 File Offset: 0x000585D2
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control should remain in insert mode after an insert operation.</summary>
		/// <returns>true to remain in insert mode after an insert operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x0005A3DB File Offset: 0x000585DB
		// (set) Token: 0x0600236F RID: 9071 RVA: 0x0005A3E3 File Offset: 0x000585E3
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
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the inserted record.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x0005A3EC File Offset: 0x000585EC
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001977 RID: 6519
		private int rowsAffected;

		// Token: 0x04001978 RID: 6520
		private Exception e;

		// Token: 0x04001979 RID: 6521
		private bool exceptionHandled;

		// Token: 0x0400197A RID: 6522
		private bool keepInsertedMode;

		// Token: 0x0400197B RID: 6523
		private IOrderedDictionary values;
	}
}
