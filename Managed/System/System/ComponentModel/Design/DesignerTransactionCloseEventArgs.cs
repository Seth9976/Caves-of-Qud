using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x0200044C RID: 1100
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x060023C1 RID: 9153 RVA: 0x00081287 File Offset: 0x0007F487
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit)
			: this(commit, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class. </summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">true if this is the last transaction to close; otherwise, false.</param>
		// Token: 0x060023C2 RID: 9154 RVA: 0x00081291 File Offset: 0x0007F491
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.TransactionCommitted = commit;
			this.LastTransaction = lastTransaction;
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>true if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, false.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000812A7 File Offset: 0x0007F4A7
		public bool TransactionCommitted { get; }

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>true, if this is the last transaction to close; otherwise, false. </returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000812AF File Offset: 0x0007F4AF
		public bool LastTransaction { get; }
	}
}
