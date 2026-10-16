using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the role management infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x020005CD RID: 1485
	public sealed class RoleManagerSection : ConfigurationSection
	{
		// Token: 0x06004003 RID: 16387 RVA: 0x000A6C48 File Offset: 0x000A4E48
		static RoleManagerSection()
		{
			RoleManagerSection.properties.Add(RoleManagerSection.cacheRolesInCookieProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieNameProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookiePathProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieProtectionProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieRequireSSLProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieSlidingExpirationProp);
			RoleManagerSection.properties.Add(RoleManagerSection.cookieTimeoutProp);
			RoleManagerSection.properties.Add(RoleManagerSection.createPersistentCookieProp);
			RoleManagerSection.properties.Add(RoleManagerSection.defaultProviderProp);
			RoleManagerSection.properties.Add(RoleManagerSection.domainProp);
			RoleManagerSection.properties.Add(RoleManagerSection.enabledProp);
			RoleManagerSection.properties.Add(RoleManagerSection.maxCachedResultsProp);
			RoleManagerSection.properties.Add(RoleManagerSection.providersProp);
		}

		/// <summary>Gets or sets a value indicating whether the current user's roles are cached in a cookie.</summary>
		/// <returns>true if the current user's roles are cached in a cookie; otherwise, false. The default is false.</returns>
		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000A6EC4 File Offset: 0x000A50C4
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x000A6ED6 File Offset: 0x000A50D6
		[ConfigurationProperty("cacheRolesInCookie", DefaultValue = false)]
		public bool CacheRolesInCookie
		{
			get
			{
				return (bool)base[RoleManagerSection.cacheRolesInCookieProp];
			}
			set
			{
				base[RoleManagerSection.cacheRolesInCookieProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the cookie that is used to cache role names.</summary>
		/// <returns>The name of the cookie used to cache role names. The default is ".ASPXROLES".</returns>
		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000A6EE9 File Offset: 0x000A50E9
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x000A6EFB File Offset: 0x000A50FB
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXROLES")]
		public string CookieName
		{
			get
			{
				return (string)base[RoleManagerSection.cookieNameProp];
			}
			set
			{
				base[RoleManagerSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the virtual path of the cookie that is used to cache role names.</summary>
		/// <returns>The path of the cookie used to store role names. The default is "/".</returns>
		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x000A6F09 File Offset: 0x000A5109
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x000A6F1B File Offset: 0x000A511B
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		public string CookiePath
		{
			get
			{
				return (string)base[RoleManagerSection.cookiePathProp];
			}
			set
			{
				base[RoleManagerSection.cookiePathProp] = value;
			}
		}

		/// <summary>Gets or sets the type of security that is used to protect the cookie that caches role names.</summary>
		/// <returns>The type of security protection used within the cookie where role names are cached. The default is All.</returns>
		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000A6F29 File Offset: 0x000A5129
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x000A6F3B File Offset: 0x000A513B
		[ConfigurationProperty("cookieProtection", DefaultValue = "All")]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[RoleManagerSection.cookieProtectionProp];
			}
			set
			{
				base[RoleManagerSection.cookieProtectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.</summary>
		/// <returns>true if an SSL connection is needed in order to return to the server the cookie where role names are cached; otherwise, false. The default is false.</returns>
		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000A6F4E File Offset: 0x000A514E
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000A6F60 File Offset: 0x000A5160
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = false)]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[RoleManagerSection.cookieRequireSSLProp];
			}
			set
			{
				base[RoleManagerSection.cookieRequireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically. </summary>
		/// <returns>true if the role names cookie expiration date and time will be reset periodically; otherwise, false. The default is true.</returns>
		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000A6F73 File Offset: 0x000A5173
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x000A6F85 File Offset: 0x000A5185
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = true)]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[RoleManagerSection.cookieSlidingExpirationProp];
			}
			set
			{
				base[RoleManagerSection.cookieSlidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the number of minutes before the cookie that is used to cache role names expires.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the number of minutes before the cookie used to cache role names expires. The default is 30, in minutes.</returns>
		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000A6F98 File Offset: 0x000A5198
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000A6FAA File Offset: 0x000A51AA
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("cookieTimeout", DefaultValue = "00:30:00")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[RoleManagerSection.cookieTimeoutProp];
			}
			set
			{
				base[RoleManagerSection.cookieTimeoutProp] = value;
			}
		}

		/// <summary>Indicates whether a session-based cookie or a persistent cookie is used to cache role names. </summary>
		/// <returns>true to make the role names cookie persistent across browser sessions; otherwise, false. The default is false.</returns>
		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000A6FBD File Offset: 0x000A51BD
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000A6FCF File Offset: 0x000A51CF
		[ConfigurationProperty("createPersistentCookie", DefaultValue = false)]
		public bool CreatePersistentCookie
		{
			get
			{
				return (bool)base[RoleManagerSection.createPersistentCookieProp];
			}
			set
			{
				base[RoleManagerSection.createPersistentCookieProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.RoleManagerSection.Providers" />. The default is "AspNetSqlRoleProvider".</returns>
		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000A6FE2 File Offset: 0x000A51E2
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000A6FF4 File Offset: 0x000A51F4
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlRoleProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[RoleManagerSection.defaultProviderProp];
			}
			set
			{
				base[RoleManagerSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the domain that is associated with the cookie that is used to cache role names. </summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the cookie used to cache role names. The default is an empty string ("").</returns>
		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000A7002 File Offset: 0x000A5202
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000A7014 File Offset: 0x000A5214
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[RoleManagerSection.domainProp];
			}
			set
			{
				base[RoleManagerSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET role management feature is enabled. </summary>
		/// <returns>true if the ASP.NET role management feature is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000A7022 File Offset: 0x000A5222
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000A7034 File Offset: 0x000A5234
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[RoleManagerSection.enabledProp];
			}
			set
			{
				base[RoleManagerSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of roles that ASP.NET caches in the role cookie. </summary>
		/// <returns>A value indicating the maximum number of roles ASP.NET caches in the role cookie. The default is 25.</returns>
		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000A7047 File Offset: 0x000A5247
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x000A7059 File Offset: 0x000A5259
		[ConfigurationProperty("maxCachedResults", DefaultValue = 25)]
		public int MaxCachedResults
		{
			get
			{
				return (int)base[RoleManagerSection.maxCachedResultsProp];
			}
			set
			{
				base[RoleManagerSection.maxCachedResultsProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> elements.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the providers subsection of the roleManager section of the configuration file.</returns>
		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000A706C File Offset: 0x000A526C
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[RoleManagerSection.providersProp];
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x000A707E File Offset: 0x000A527E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RoleManagerSection.properties;
			}
		}

		// Token: 0x040022D3 RID: 8915
		private static ConfigurationProperty cacheRolesInCookieProp = new ConfigurationProperty("cacheRolesInCookie", typeof(bool), false);

		// Token: 0x040022D4 RID: 8916
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXROLES");

		// Token: 0x040022D5 RID: 8917
		private static ConfigurationProperty cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/");

		// Token: 0x040022D6 RID: 8918
		private static ConfigurationProperty cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.All);

		// Token: 0x040022D7 RID: 8919
		private static ConfigurationProperty cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);

		// Token: 0x040022D8 RID: 8920
		private static ConfigurationProperty cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);

		// Token: 0x040022D9 RID: 8921
		private static ConfigurationProperty cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022DA RID: 8922
		private static ConfigurationProperty createPersistentCookieProp = new ConfigurationProperty("createPersistentCookie", typeof(bool), false);

		// Token: 0x040022DB RID: 8923
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlRoleProvider");

		// Token: 0x040022DC RID: 8924
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x040022DD RID: 8925
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x040022DE RID: 8926
		private static ConfigurationProperty maxCachedResultsProp = new ConfigurationProperty("maxCachedResults", typeof(int), 25);

		// Token: 0x040022DF RID: 8927
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x040022E0 RID: 8928
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
