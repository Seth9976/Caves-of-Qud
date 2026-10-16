using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C0 RID: 448
	public class SettingChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x000314B2 File Offset: 0x0002F6B2
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel)
			: base(cancel)
		{
			this.settingName = settingName;
			this.settingClass = settingClass;
			this.settingKey = settingKey;
			this.newValue = newValue;
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x000314D9 File Offset: 0x0002F6D9
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x000314E1 File Offset: 0x0002F6E1
		public string SettingClass
		{
			get
			{
				return this.settingClass;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000314E9 File Offset: 0x0002F6E9
		public string SettingKey
		{
			get
			{
				return this.settingKey;
			}
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x000314F1 File Offset: 0x0002F6F1
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x040007A8 RID: 1960
		private string settingName;

		// Token: 0x040007A9 RID: 1961
		private string settingClass;

		// Token: 0x040007AA RID: 1962
		private string settingKey;

		// Token: 0x040007AB RID: 1963
		private object newValue;
	}
}
