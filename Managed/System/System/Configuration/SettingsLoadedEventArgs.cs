using System;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	// Token: 0x020001CB RID: 459
	public class SettingsLoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> class. </summary>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object from which settings are loaded.</param>
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00031E25 File Offset: 0x00030025
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>Gets the settings provider used to store configuration settings.</summary>
		/// <returns>A settings provider.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00031E34 File Offset: 0x00030034
		public SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x040007BB RID: 1979
		private SettingsProvider provider;
	}
}
