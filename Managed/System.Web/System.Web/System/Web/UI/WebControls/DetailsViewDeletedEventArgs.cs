using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleted" /> event.</summary>
	// Token: 0x02000384 RID: 900
	public class DetailsViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x0600235C RID: 9052 RVA: 0x0005A2E9 File Offset: 0x000584E9
		public DetailsViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0005A306 File Offset: 0x00058506
		internal DetailsViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0005A31F File Offset: 0x0005851F
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0005A327 File Offset: 0x00058527
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x0005A32F File Offset: 0x0005852F
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x0005A337 File Offset: 0x00058537
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

		/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x0005A340 File Offset: 0x00058540
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x0005A348 File Offset: 0x00058548
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001970 RID: 6512
		private int rowsAffected;

		// Token: 0x04001971 RID: 6513
		private Exception e;

		// Token: 0x04001972 RID: 6514
		private bool exceptionHandled;

		// Token: 0x04001973 RID: 6515
		private IOrderedDictionary keys;

		// Token: 0x04001974 RID: 6516
		private IOrderedDictionary values;
	}
}
