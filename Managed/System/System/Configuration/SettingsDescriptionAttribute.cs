using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C8 RID: 456
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00031DE0 File Offset: 0x0002FFE0
		public SettingsDescriptionAttribute(string description)
		{
			this.desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00031DEF File Offset: 0x0002FFEF
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x040007B8 RID: 1976
		private string desc;
	}
}
