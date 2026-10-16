using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005BC RID: 1468
	public sealed class OutputCacheSection : ConfigurationSection
	{
		// Token: 0x06003EF7 RID: 16119 RVA: 0x000A48B8 File Offset: 0x000A2AB8
		static OutputCacheSection()
		{
			OutputCacheSection.properties.Add(OutputCacheSection.enableFragmentCacheProp);
			OutputCacheSection.properties.Add(OutputCacheSection.enableOutputCacheProp);
			OutputCacheSection.properties.Add(OutputCacheSection.omitVaryStarProp);
			OutputCacheSection.properties.Add(OutputCacheSection.sendCacheControlHeaderProp);
			OutputCacheSection.properties.Add(OutputCacheSection.enableKernelCacheForVaryByStarProp);
			OutputCacheSection.properties.Add(OutputCacheSection.providersProp);
			OutputCacheSection.properties.Add(OutputCacheSection.defaultProviderNameProp);
		}

		/// <summary>Gets or sets a value indicating whether the fragment cache is enabled.</summary>
		/// <returns>true if the fragment cache is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x000A4A0A File Offset: 0x000A2C0A
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x000A4A1C File Offset: 0x000A2C1C
		[ConfigurationProperty("enableFragmentCache", DefaultValue = "True")]
		public bool EnableFragmentCache
		{
			get
			{
				return (bool)base[OutputCacheSection.enableFragmentCacheProp];
			}
			set
			{
				base[OutputCacheSection.enableFragmentCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the output cache is enabled.</summary>
		/// <returns>true if the output cache is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000A4A2F File Offset: 0x000A2C2F
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000A4A41 File Offset: 0x000A2C41
		[ConfigurationProperty("enableOutputCache", DefaultValue = "True")]
		public bool EnableOutputCache
		{
			get
			{
				return (bool)base[OutputCacheSection.enableOutputCacheProp];
			}
			set
			{
				base[OutputCacheSection.enableOutputCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether kernel caching is enabled.</summary>
		/// <returns>true if kernel caching is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000A4A54 File Offset: 0x000A2C54
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000A4A66 File Offset: 0x000A2C66
		[ConfigurationProperty("enableKernelCacheForVaryByStar", DefaultValue = "False")]
		public bool EnableKernelCacheForVaryByStar
		{
			get
			{
				return (bool)base[OutputCacheSection.enableKernelCacheForVaryByStarProp];
			}
			set
			{
				base[OutputCacheSection.enableKernelCacheForVaryByStarProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the vary header is enabled.</summary>
		/// <returns>true if the vary header is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000A4A79 File Offset: 0x000A2C79
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x000A4A8B File Offset: 0x000A2C8B
		[ConfigurationProperty("omitVaryStar", DefaultValue = "False")]
		public bool OmitVaryStar
		{
			get
			{
				return (bool)base[OutputCacheSection.omitVaryStarProp];
			}
			set
			{
				base[OutputCacheSection.omitVaryStarProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache-control:private header is sent by the output cache module by default.</summary>
		/// <returns>true if the sending of cache-control:private header is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x000A4A9E File Offset: 0x000A2C9E
		// (set) Token: 0x06003F01 RID: 16129 RVA: 0x000A4AB0 File Offset: 0x000A2CB0
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = "True")]
		public bool SendCacheControlHeader
		{
			get
			{
				return (bool)base[OutputCacheSection.sendCacheControlHeaderProp];
			}
			set
			{
				base[OutputCacheSection.sendCacheControlHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the ASP.NET default output-cache provider that is stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file.</summary>
		/// <returns>The name of the default provider.</returns>
		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x000A4AC3 File Offset: 0x000A2CC3
		// (set) Token: 0x06003F03 RID: 16131 RVA: 0x000A4AD5 File Offset: 0x000A2CD5
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetInternalProvider")]
		public string DefaultProviderName
		{
			get
			{
				return base[OutputCacheSection.defaultProviderNameProp] as string;
			}
			set
			{
				base[OutputCacheSection.defaultProviderNameProp] = value;
			}
		}

		/// <summary>Gets or sets the collection of output-cache providers that are stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file. </summary>
		/// <returns>The collection of providers.</returns>
		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000A4AE3 File Offset: 0x000A2CE3
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return base[OutputCacheSection.providersProp] as ProviderSettingsCollection;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000A4AF5 File Offset: 0x000A2CF5
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheSection.properties;
			}
		}

		// Token: 0x04002266 RID: 8806
		private static ConfigurationProperty enableFragmentCacheProp = new ConfigurationProperty("enableFragmentCache", typeof(bool), true);

		// Token: 0x04002267 RID: 8807
		private static ConfigurationProperty enableOutputCacheProp = new ConfigurationProperty("enableOutputCache", typeof(bool), true);

		// Token: 0x04002268 RID: 8808
		private static ConfigurationProperty omitVaryStarProp = new ConfigurationProperty("omitVaryStar", typeof(bool), false);

		// Token: 0x04002269 RID: 8809
		private static ConfigurationProperty sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);

		// Token: 0x0400226A RID: 8810
		private static ConfigurationProperty enableKernelCacheForVaryByStarProp = new ConfigurationProperty("enableKernelCacheForVaryByStar", typeof(bool), false);

		// Token: 0x0400226B RID: 8811
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x0400226C RID: 8812
		private static ConfigurationProperty defaultProviderNameProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetInternalProvider");

		// Token: 0x0400226D RID: 8813
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
