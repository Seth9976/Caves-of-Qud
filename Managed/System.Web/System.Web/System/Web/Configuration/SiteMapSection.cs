using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the infrastructure for configuring, storing, and rendering site navigation. This class cannot be inherited.</summary>
	// Token: 0x020005D4 RID: 1492
	public sealed class SiteMapSection : ConfigurationSection
	{
		// Token: 0x06004086 RID: 16518 RVA: 0x000A7D64 File Offset: 0x000A5F64
		static SiteMapSection()
		{
			SiteMapSection.properties.Add(SiteMapSection.defaultProviderProp);
			SiteMapSection.properties.Add(SiteMapSection.enabledProp);
			SiteMapSection.properties.Add(SiteMapSection.providersProp);
		}

		/// <summary>Gets or sets the name of the default navigation provider. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.SiteMapSection.Providers" /> property or a <see cref="F:System.String.Empty" /> field. The default is "AspNetXmlSiteMapProvider".</returns>
		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000A7DFE File Offset: 0x000A5FFE
		// (set) Token: 0x06004088 RID: 16520 RVA: 0x000A7E10 File Offset: 0x000A6010
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetXmlSiteMapProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base["defaultProvider"];
			}
			set
			{
				base["defaultProvider"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET site map feature is enabled.</summary>
		/// <returns>true if the ASP.NET site map feature is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x000A7E1E File Offset: 0x000A601E
		// (set) Token: 0x0600408A RID: 16522 RVA: 0x000A7E30 File Offset: 0x000A6030
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the providers subsection of the siteMap section of the configuration file.</returns>
		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000A7E43 File Offset: 0x000A6043
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000A7E58 File Offset: 0x000A6058
		internal SiteMapProviderCollection ProvidersInternal
		{
			get
			{
				if (this.providers == null)
				{
					SiteMapProviderCollection siteMapProviderCollection = new SiteMapProviderCollection();
					ProvidersHelper.InstantiateProviders(this.Providers, siteMapProviderCollection, typeof(SiteMapProvider));
					this.providers = siteMapProviderCollection;
				}
				return this.providers;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000A7E96 File Offset: 0x000A6096
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SiteMapSection.properties;
			}
		}

		// Token: 0x04002306 RID: 8966
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetXmlSiteMapProvider");

		// Token: 0x04002307 RID: 8967
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002308 RID: 8968
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x04002309 RID: 8969
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400230A RID: 8970
		private SiteMapProviderCollection providers;
	}
}
