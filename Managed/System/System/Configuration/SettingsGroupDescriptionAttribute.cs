using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C9 RID: 457
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
		/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00031DF7 File Offset: 0x0002FFF7
		public SettingsGroupDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>The descriptive text for the application settings properties group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00031E06 File Offset: 0x00030006
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x040007B9 RID: 1977
		private string desc;
	}
}
