using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler. </summary>
	// Token: 0x020003B0 RID: 944
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of false.</summary>
		// Token: 0x06001EDE RID: 7902 RVA: 0x0006CC66 File Offset: 0x0006AE66
		public HandledEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x06001EDF RID: 7903 RVA: 0x0006CC6F File Offset: 0x0006AE6F
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.Handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>true if the event has been completely handled; otherwise, false.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0006CC7E File Offset: 0x0006AE7E
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x0006CC86 File Offset: 0x0006AE86
		public bool Handled { get; set; }
	}
}
