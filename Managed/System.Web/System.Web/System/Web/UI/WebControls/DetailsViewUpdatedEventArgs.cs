using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdated" /> event.</summary>
	// Token: 0x0200038B RID: 907
	public class DetailsViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the update operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the update operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x06002388 RID: 9096 RVA: 0x0005A52B File Offset: 0x0005872B
		public DetailsViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0005A54F File Offset: 0x0005874F
		internal DetailsViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the number of rows affected by the update operation.</summary>
		/// <returns>The number of rows affected by the update operation.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x0005A570 File Offset: 0x00058770
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the update operation.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x0005A578 File Offset: 0x00058778
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0005A580 File Offset: 0x00058780
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x0005A588 File Offset: 0x00058788
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true to remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x0005A591 File Offset: 0x00058791
		// (set) Token: 0x0600238F RID: 9103 RVA: 0x0005A599 File Offset: 0x00058799
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

		/// <summary>Gets a dictionary that contains the key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the updated record.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0005A5A2 File Offset: 0x000587A2
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0005A5AA File Offset: 0x000587AA
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x0005A5B2 File Offset: 0x000587B2
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001985 RID: 6533
		private int rowsAffected;

		// Token: 0x04001986 RID: 6534
		private Exception e;

		// Token: 0x04001987 RID: 6535
		private bool exceptionHandled;

		// Token: 0x04001988 RID: 6536
		private bool keepEditMode;

		// Token: 0x04001989 RID: 6537
		private IOrderedDictionary keys;

		// Token: 0x0400198A RID: 6538
		private IOrderedDictionary newValues;

		// Token: 0x0400198B RID: 6539
		private IOrderedDictionary oldValues;
	}
}
