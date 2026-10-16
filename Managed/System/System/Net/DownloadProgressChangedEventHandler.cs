using System;

namespace System.Net
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> containing event data.</param>
	// Token: 0x020005B7 RID: 1463
	// (Invoke) Token: 0x06002FAB RID: 12203
	public delegate void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e);
}
