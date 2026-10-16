using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Configures anonymous identification for users that are not authenticated. This class cannot be inherited.</summary>
	// Token: 0x0200057A RID: 1402
	public sealed class AnonymousIdentificationSection : ConfigurationSection
	{
		// Token: 0x06003B2B RID: 15147 RVA: 0x0009C7DC File Offset: 0x0009A9DC
		static AnonymousIdentificationSection()
		{
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.enabledProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookielessProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieNameProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieTimeoutProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookiePathProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieRequireSSLProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieSlidingExpirationProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.cookieProtectionProp);
			AnonymousIdentificationSection.properties.Add(AnonymousIdentificationSection.domainProp);
		}

		/// <summary>Gets or sets a value indicating whether to use cookies.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />. </returns>
		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x0009C9F0 File Offset: 0x0009ABF0
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x0009CA02 File Offset: 0x0009AC02
		[ConfigurationProperty("cookieless", DefaultValue = "UseCookies")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[AnonymousIdentificationSection.cookielessProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookielessProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the cookie. The default value is ".ASPXANONYMOUS".</returns>
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x0009CA15 File Offset: 0x0009AC15
		// (set) Token: 0x06003B2F RID: 15151 RVA: 0x0009CA27 File Offset: 0x0009AC27
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookieName", DefaultValue = ".ASPXANONYMOUS")]
		public string CookieName
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.cookieNameProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the path where the cookie is stored.</summary>
		/// <returns>The path of the HTTP cookie to use for the user's anonymous identification. The default value is a slash (/), which represents the Web application root.</returns>
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x0009CA35 File Offset: 0x0009AC35
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x0009CA47 File Offset: 0x0009AC47
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("cookiePath", DefaultValue = "/")]
		public string CookiePath
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.cookiePathProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookiePathProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> values. The default value is <see cref="F:System.Web.Security.CookieProtection.All" />.</returns>
		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x0009CA55 File Offset: 0x0009AC55
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x0009CA67 File Offset: 0x0009AC67
		[ConfigurationProperty("cookieProtection", DefaultValue = "Validation")]
		public CookieProtection CookieProtection
		{
			get
			{
				return (CookieProtection)base[AnonymousIdentificationSection.cookieProtectionProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieProtectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
		/// <returns>true if an SSL connection is required; otherwise, false. The default is false.</returns>
		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x0009CA7A File Offset: 0x0009AC7A
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x0009CA8C File Offset: 0x0009AC8C
		[ConfigurationProperty("cookieRequireSSL", DefaultValue = "False")]
		public bool CookieRequireSSL
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.cookieRequireSSLProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieRequireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets whether the cookie time-out value is reset on each request.</summary>
		/// <returns>true if the sliding expiration is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x0009CA9F File Offset: 0x0009AC9F
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x0009CAB1 File Offset: 0x0009ACB1
		[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = "True")]
		public bool CookieSlidingExpiration
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.cookieSlidingExpirationProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieSlidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time, in minutes, after which the authentication expires.</summary>
		/// <returns>The amount of time, in minutes, after which the authentication expires. The default value is 100000.</returns>
		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x0009CAC4 File Offset: 0x0009ACC4
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x0009CAD6 File Offset: 0x0009ACD6
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("cookieTimeout", DefaultValue = "69.10:40:00")]
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		public TimeSpan CookieTimeout
		{
			get
			{
				return (TimeSpan)base[AnonymousIdentificationSection.cookieTimeoutProp];
			}
			set
			{
				base[AnonymousIdentificationSection.cookieTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie domain.</summary>
		/// <returns>The name of the cookie domain. The default is an empty string ("").</returns>
		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x0009CAE9 File Offset: 0x0009ACE9
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x0009CAFB File Offset: 0x0009ACFB
		[ConfigurationProperty("domain")]
		public string Domain
		{
			get
			{
				return (string)base[AnonymousIdentificationSection.domainProp];
			}
			set
			{
				base[AnonymousIdentificationSection.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether anonymous identification is enabled. </summary>
		/// <returns>true if anonymous identification is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x0009CB09 File Offset: 0x0009AD09
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x0009CB1B File Offset: 0x0009AD1B
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[AnonymousIdentificationSection.enabledProp];
			}
			set
			{
				base[AnonymousIdentificationSection.enabledProp] = value;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x0009CB2E File Offset: 0x0009AD2E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AnonymousIdentificationSection.properties;
			}
		}

		// Token: 0x04002075 RID: 8309
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002076 RID: 8310
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04002077 RID: 8311
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseCookies, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002078 RID: 8312
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXANONYMOUS", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002079 RID: 8313
		private static ConfigurationProperty cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), new TimeSpan(69, 10, 40, 0), new TimeSpanMinutesOrInfiniteConverter(), PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400207A RID: 8314
		private static ConfigurationProperty cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400207B RID: 8315
		private static ConfigurationProperty cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);

		// Token: 0x0400207C RID: 8316
		private static ConfigurationProperty cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);

		// Token: 0x0400207D RID: 8317
		private static ConfigurationProperty cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.Validation, new GenericEnumConverter(typeof(CookieProtection)), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400207E RID: 8318
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), null);
	}
}
