using System;

namespace System.Net.NetworkInformation
{
	/// <summary>References one or more methods to be called when the availability of the network changes.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains data about the event.</param>
	// Token: 0x020006F6 RID: 1782
	// (Invoke) Token: 0x06003948 RID: 14664
	public delegate void NetworkAvailabilityChangedEventHandler(object sender, NetworkAvailabilityEventArgs e);
}
