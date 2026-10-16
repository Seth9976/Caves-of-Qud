using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdated" /> event.</summary>
	// Token: 0x020003A6 RID: 934
	public class GridViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows that were affected by the update operation.</param>
		/// <param name="e">The exception that was raised when the update operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x0600260B RID: 9739 RVA: 0x00061DC0 File Offset: 0x0005FFC0
		public GridViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00061DE4 File Offset: 0x0005FFE4
		internal GridViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the number of rows that were affected by the update operation.</summary>
		/// <returns>The number of rows that were affected by the update operation.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00061E05 File Offset: 0x00060005
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>The exception that was raised during the update operation. If no exceptions were raised, this property returns null.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00061E0D File Offset: 0x0006000D
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00061E15 File Offset: 0x00060015
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x00061E1D File Offset: 0x0006001D
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.GridView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true if the control will remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00061E26 File Offset: 0x00060026
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x00061E2E File Offset: 0x0006002E
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
		/// <returns>A dictionary of key field name/value pairs for the updated record.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x00061E37 File Offset: 0x00060037
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>A dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x00061E3F File Offset: 0x0006003F
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the updated record.</summary>
		/// <returns>A dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00061E47 File Offset: 0x00060047
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001A3F RID: 6719
		private int rowsAffected;

		// Token: 0x04001A40 RID: 6720
		private Exception e;

		// Token: 0x04001A41 RID: 6721
		private bool exceptionHandled;

		// Token: 0x04001A42 RID: 6722
		private bool keepEditMode;

		// Token: 0x04001A43 RID: 6723
		private IOrderedDictionary keys;

		// Token: 0x04001A44 RID: 6724
		private IOrderedDictionary newValues;

		// Token: 0x04001A45 RID: 6725
		private IOrderedDictionary oldValues;
	}
}
