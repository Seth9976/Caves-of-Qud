using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020003FE RID: 1022
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x00072135 File Offset: 0x00070335
		// (set) Token: 0x0600213E RID: 8510 RVA: 0x0007213D File Offset: 0x0007033D
		public bool Cancel { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x0600213F RID: 8511 RVA: 0x0000C759 File Offset: 0x0000A959
		public CancelEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x06002140 RID: 8512 RVA: 0x00072146 File Offset: 0x00070346
		public CancelEventArgs(bool cancel)
		{
			this.Cancel = cancel;
		}
	}
}
