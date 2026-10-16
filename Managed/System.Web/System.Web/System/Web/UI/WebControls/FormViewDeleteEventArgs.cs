using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleting" /> event.</summary>
	// Token: 0x02000398 RID: 920
	public class FormViewDeleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewDeleteEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row being deleted.</param>
		// Token: 0x060024C8 RID: 9416 RVA: 0x0005DF63 File Offset: 0x0005C163
		public FormViewDeleteEventArgs(int rowIndex)
		{
			this.rowIndex = rowIndex;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0005DF72 File Offset: 0x0005C172
		internal FormViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
			: this(index)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the index of the record being deleted from the data source.</summary>
		/// <returns>The index of the record being deleted from the data source.</returns>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0005DF89 File Offset: 0x0005C189
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets an ordered dictionary of key field name/value pairs for the record to delete.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the key field name/value pairs for the record to delete.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x0005DF91 File Offset: 0x0005C191
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0005DF99 File Offset: 0x0005C199
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019D3 RID: 6611
		private int rowIndex;

		// Token: 0x040019D4 RID: 6612
		private IOrderedDictionary keys;

		// Token: 0x040019D5 RID: 6613
		private IOrderedDictionary values;
	}
}
