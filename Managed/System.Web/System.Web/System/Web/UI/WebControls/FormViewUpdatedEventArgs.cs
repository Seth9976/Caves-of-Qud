using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdated" /> event.</summary>
	// Token: 0x0200039F RID: 927
	public class FormViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the update operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the update operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x060024F1 RID: 9457 RVA: 0x0005E1DA File Offset: 0x0005C3DA
		public FormViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0005E1FE File Offset: 0x0005C3FE
		internal FormViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.oldValues = oldValues;
			this.newValues = newValues;
		}

		/// <summary>Gets the number of rows affected by the update operation.</summary>
		/// <returns>The number of rows affected by the update operation.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x0005E21F File Offset: 0x0005C41F
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object that represents the exception that was raised during the update operation.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0005E227 File Offset: 0x0005C427
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x0005E22F File Offset: 0x0005C42F
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x0005E237 File Offset: 0x0005C437
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FormView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true to remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x0005E240 File Offset: 0x0005C440
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x0005E248 File Offset: 0x0005C448
		public bool KeepInEditMode
		{
			get
			{
				return this.keepEditMode;
			}
			set
			{
				this.keepEditMode = value;
			}
		}

		/// <summary>Gets a dictionary that contains the original key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the updated record.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x0005E251 File Offset: 0x0005C451
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x0005E259 File Offset: 0x0005C459
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x0005E261 File Offset: 0x0005C461
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x040019EA RID: 6634
		private int rowsAffected;

		// Token: 0x040019EB RID: 6635
		private Exception e;

		// Token: 0x040019EC RID: 6636
		private bool exceptionHandled;

		// Token: 0x040019ED RID: 6637
		private bool keepEditMode;

		// Token: 0x040019EE RID: 6638
		private IOrderedDictionary keys;

		// Token: 0x040019EF RID: 6639
		private IOrderedDictionary oldValues;

		// Token: 0x040019F0 RID: 6640
		private IOrderedDictionary newValues;
	}
}
