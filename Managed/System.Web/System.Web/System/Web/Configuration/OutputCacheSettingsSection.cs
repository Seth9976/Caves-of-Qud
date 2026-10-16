using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache settings for application pages . This class cannot be inherited.</summary>
	// Token: 0x020005BD RID: 1469
	public sealed class OutputCacheSettingsSection : ConfigurationSection
	{
		// Token: 0x06003F07 RID: 16135 RVA: 0x000A4AFC File Offset: 0x000A2CFC
		static OutputCacheSettingsSection()
		{
			OutputCacheSettingsSection.properties.Add(OutputCacheSettingsSection.outputCacheProfilesProp);
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects</returns>
		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x000A4B38 File Offset: 0x000A2D38
		[ConfigurationProperty("outputCacheProfiles")]
		public OutputCacheProfileCollection OutputCacheProfiles
		{
			get
			{
				return (OutputCacheProfileCollection)base[OutputCacheSettingsSection.outputCacheProfilesProp];
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06003F09 RID: 16137 RVA: 0x000A4B4A File Offset: 0x000A2D4A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSettingsSection.properties;
			}
		}

		// Token: 0x0400226E RID: 8814
		private static ConfigurationProperty outputCacheProfilesProp = new ConfigurationProperty("outputCacheProfiles", typeof(OutputCacheProfileCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400226F RID: 8815
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
