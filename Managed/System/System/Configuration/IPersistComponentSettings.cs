using System;

namespace System.Configuration
{
	/// <summary>Defines standard functionality for controls or libraries that store and retrieve application settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B6 RID: 438
	public interface IPersistComponentSettings
	{
		/// <summary>Gets or sets a value indicating whether the control should automatically persist its application settings properties.</summary>
		/// <returns>true if the control should automatically persist its state; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B88 RID: 2952
		// (set) Token: 0x06000B89 RID: 2953
		bool SaveSettings { get; set; }

		/// <summary>Gets or sets the value of the application settings key for the current instance of the control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current instance of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B8A RID: 2954
		// (set) Token: 0x06000B8B RID: 2955
		string SettingsKey { get; set; }

		/// <summary>Reads the control's application settings into their corresponding properties and updates the control's state.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B8C RID: 2956
		void LoadComponentSettings();

		/// <summary>Resets the control's application settings properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B8D RID: 2957
		void ResetComponentSettings();

		/// <summary>Persists the control's application settings properties.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B8E RID: 2958
		void SaveComponentSettings();
	}
}
