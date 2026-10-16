using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events of a designer.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" />  that contains the event data. </param>
	// Token: 0x0200044D RID: 1101
	// (Invoke) Token: 0x060023C6 RID: 9158
	public delegate void DesignerTransactionCloseEventHandler(object sender, DesignerTransactionCloseEventArgs e);
}
