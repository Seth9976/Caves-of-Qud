using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures properties for cookies used by a Web application.</summary>
	// Token: 0x020005A6 RID: 1446
	public sealed class HttpCookiesSection : ConfigurationSection
	{
		// Token: 0x06003DD4 RID: 15828 RVA: 0x000A1ED4 File Offset: 0x000A00D4
		static HttpCookiesSection()
		{
			HttpCookiesSection.properties.Add(HttpCookiesSection.domainProp);
			HttpCookiesSection.properties.Add(HttpCookiesSection.httpOnlyCookiesProp);
			HttpCookiesSection.properties.Add(HttpCookiesSection.requireSSLProp);
		}

		/// <summary>Gets or sets the cookie domain name.</summary>
		/// <returns>The cookie domain name. </returns>
		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x000A1F74 File Offset: 0x000A0174
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x000A1F86 File Offset: 0x000A0186
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[HttpCookiesSection.domainProp];
			}
			set
			{
				base[HttpCookiesSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the support for the browser's HttpOnly cookie is enabled.</summary>
		/// <returns>true if support for the HttpOnly cookie is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000A1F94 File Offset: 0x000A0194
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x000A1FA6 File Offset: 0x000A01A6
		[ConfigurationProperty("httpOnlyCookies", DefaultValue = "False")]
		public bool HttpOnlyCookies
		{
			get
			{
				return (bool)base[HttpCookiesSection.httpOnlyCookiesProp];
			}
			set
			{
				base[HttpCookiesSection.httpOnlyCookiesProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether Secure Sockets Layer (SSL) communication is required.</summary>
		/// <returns>true if SSL is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000A1FB9 File Offset: 0x000A01B9
		// (set) Token: 0x06003DDA RID: 15834 RVA: 0x000A1FCB File Offset: 0x000A01CB
		[ConfigurationProperty("requireSSL", DefaultValue = "False")]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[HttpCookiesSection.requireSSLProp];
			}
			set
			{
				base[HttpCookiesSection.requireSSLProp] = value;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000A1FDE File Offset: 0x000A01DE
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCookiesSection.properties;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000A1FE8 File Offset: 0x000A01E8
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SameSiteMode SameSite
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SameSiteMode.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040021FC RID: 8700
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x040021FD RID: 8701
		private static ConfigurationProperty httpOnlyCookiesProp = new ConfigurationProperty("httpOnlyCookies", typeof(bool), false);

		// Token: 0x040021FE RID: 8702
		private static ConfigurationProperty requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);

		// Token: 0x040021FF RID: 8703
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
