using System;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event raised when a property is changed on a component.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
	// Token: 0x02000407 RID: 1031
	// (Invoke) Token: 0x06002154 RID: 8532
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
