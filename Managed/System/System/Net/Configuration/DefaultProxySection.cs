using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x0200076A RID: 1898
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000CCE90 File Offset: 0x000CB090
		static DefaultProxySection()
		{
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.enabledProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
			DefaultProxySection.properties.Add(DefaultProxySection.useDefaultCredentialsProp);
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server. </returns>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000CCF7E File Offset: 0x000CB17E
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>true if a Web proxy will be used; otherwise, false.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000CCF90 File Offset: 0x000CB190
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x000CCFA2 File Offset: 0x000CB1A2
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[DefaultProxySection.enabledProp];
			}
			set
			{
				base[DefaultProxySection.enabledProp] = value;
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ModuleElement" />. The type information for a custom Web proxy implementation.</returns>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000CCFB5 File Offset: 0x000CB1B5
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[DefaultProxySection.moduleProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement" />. The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000CCFC7 File Offset: 0x000CB1C7
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>true if default credentials are to be used; otherwise, false.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000CCFD9 File Offset: 0x000CB1D9
		// (set) Token: 0x06003BCA RID: 15306 RVA: 0x000CCFEB File Offset: 0x000CB1EB
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = "False")]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[DefaultProxySection.useDefaultCredentialsProp];
			}
			set
			{
				base[DefaultProxySection.useDefaultCredentialsProp] = value;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000CCFFE File Offset: 0x000CB1FE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x040023A9 RID: 9129
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023AA RID: 9130
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x040023AB RID: 9131
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x040023AC RID: 9132
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x040023AD RID: 9133
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x040023AE RID: 9134
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
