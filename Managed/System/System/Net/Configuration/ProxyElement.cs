using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000775 RID: 1909
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06003C0D RID: 15373 RVA: 0x000CD8A4 File Offset: 0x000CBAA4
		static ProxyElement()
		{
			ProxyElement.properties.Add(ProxyElement.autoDetectProp);
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Gets or sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000CD997 File Offset: 0x000CBB97
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000CD9A9 File Offset: 0x000CBBA9
		[ConfigurationProperty("autoDetect", DefaultValue = "Unspecified")]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[ProxyElement.autoDetectProp];
			}
			set
			{
				base[ProxyElement.autoDetectProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement.BypassOnLocalValues" />.Avalue that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000CD9BC File Offset: 0x000CBBBC
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000CD9CE File Offset: 0x000CBBCE
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[ProxyElement.bypassOnLocalProp];
			}
			set
			{
				base[ProxyElement.bypassOnLocalProp] = value;
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a URI.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000CD9E1 File Offset: 0x000CBBE1
		// (set) Token: 0x06003C14 RID: 15380 RVA: 0x000CD9F3 File Offset: 0x000CBBF3
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[ProxyElement.proxyAddressProp];
			}
			set
			{
				base[ProxyElement.proxyAddressProp] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000CDA01 File Offset: 0x000CBC01
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x000CDA13 File Offset: 0x000CBC13
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[ProxyElement.scriptLocationProp];
			}
			set
			{
				base[ProxyElement.scriptLocationProp] = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>true if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, false.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000CDA21 File Offset: 0x000CBC21
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x000CDA33 File Offset: 0x000CBC33
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[ProxyElement.useSystemDefaultProp];
			}
			set
			{
				base[ProxyElement.useSystemDefaultProp] = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000CDA46 File Offset: 0x000CBC46
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x040023C1 RID: 9153
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023C2 RID: 9154
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x040023C3 RID: 9155
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x040023C4 RID: 9156
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(Uri), null);

		// Token: 0x040023C5 RID: 9157
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(Uri), null);

		// Token: 0x040023C6 RID: 9158
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000776 RID: 1910
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040023C8 RID: 9160
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x040023C9 RID: 9161
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x040023CA RID: 9162
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000777 RID: 1911
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x040023CC RID: 9164
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x040023CD RID: 9165
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x040023CE RID: 9166
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x02000778 RID: 1912
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040023D0 RID: 9168
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x040023D1 RID: 9169
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x040023D2 RID: 9170
			False = 0
		}
	}
}
