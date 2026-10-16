using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value has changed.</summary>
	// Token: 0x02000404 RID: 1028
	public interface INotifyPropertyChanged
	{
		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600214D RID: 8525
		// (remove) Token: 0x0600214E RID: 8526
		event PropertyChangedEventHandler PropertyChanged;
	}
}
