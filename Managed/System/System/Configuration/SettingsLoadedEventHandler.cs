using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	/// <param name="sender">The source of the event, typically the settings class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> object that contains the event data.</param>
	// Token: 0x020001CC RID: 460
	// (Invoke) Token: 0x06000BFC RID: 3068
	public delegate void SettingsLoadedEventHandler(object sender, SettingsLoadedEventArgs e);
}
