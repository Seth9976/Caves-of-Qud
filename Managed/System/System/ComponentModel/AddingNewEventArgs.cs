using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x0200037B RID: 891
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x06001D47 RID: 7495 RVA: 0x0000C759 File Offset: 0x0000A959
		public AddingNewEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x06001D48 RID: 7496 RVA: 0x00068752 File Offset: 0x00066952
		public AddingNewEventArgs(object newObject)
		{
			this.NewObject = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection. </returns>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00068761 File Offset: 0x00066961
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x00068769 File Offset: 0x00066969
		public object NewObject { get; set; }
	}
}
