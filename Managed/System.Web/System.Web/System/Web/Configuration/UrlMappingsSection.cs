using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the urlMappings configuration section. This class cannot be inherited.</summary>
	// Token: 0x020005E5 RID: 1509
	public sealed class UrlMappingsSection : ConfigurationSection
	{
		// Token: 0x06004176 RID: 16758 RVA: 0x000A9280 File Offset: 0x000A7480
		static UrlMappingsSection()
		{
			UrlMappingsSection.properties.Add(UrlMappingsSection.enabledProp);
			UrlMappingsSection.properties.Add(UrlMappingsSection.urlMappingsProp);
		}

		/// <summary>Gets or sets a value indicating whether the mapping is enabled.</summary>
		/// <returns>true if the mapping is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000A92F1 File Offset: 0x000A74F1
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000A9303 File Offset: 0x000A7503
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool IsEnabled
		{
			get
			{
				return (bool)base[UrlMappingsSection.enabledProp];
			}
			set
			{
				base[UrlMappingsSection.enabledProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.UrlMappingCollection" /> that contains <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</returns>
		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000A9316 File Offset: 0x000A7516
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public UrlMappingCollection UrlMappings
		{
			get
			{
				return (UrlMappingCollection)base[UrlMappingsSection.urlMappingsProp];
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x000A9328 File Offset: 0x000A7528
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMappingsSection.properties;
			}
		}

		// Token: 0x04002339 RID: 9017
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400233A RID: 9018
		private static ConfigurationProperty urlMappingsProp = new ConfigurationProperty("", typeof(UrlMappingCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x0400233B RID: 9019
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
