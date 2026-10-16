using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures an ASP.NET application to use the <see cref="T:System.Web.Configuration.AuthenticationMode" /> forms modality. </summary>
	// Token: 0x02000599 RID: 1433
	public sealed class FormsAuthenticationConfiguration : ConfigurationElement
	{
		// Token: 0x06003CB4 RID: 15540 RVA: 0x0009F2F0 File Offset: 0x0009D4F0
		static FormsAuthenticationConfiguration()
		{
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.cookielessProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.credentialsProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.defaultUrlProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.domainProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.enableCrossAppRedirectsProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.loginUrlProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.nameProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.pathProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.protectionProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.requireSSLProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.slidingExpirationProp);
			FormsAuthenticationConfiguration.properties.Add(FormsAuthenticationConfiguration.timeoutProp);
			FormsAuthenticationConfiguration.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(FormsAuthenticationConfiguration), new ValidatorCallback(FormsAuthenticationConfiguration.ValidateElement)));
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x0009F5F4 File Offset: 0x0009D7F4
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return FormsAuthenticationConfiguration.elementProperty;
			}
		}

		/// <summary>Gets or sets a value indicating whether forms-based authentication should use cookies.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x0009F5FB File Offset: 0x0009D7FB
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x0009F60D File Offset: 0x0009D80D
		[ConfigurationProperty("cookieless", DefaultValue = "UseDeviceProfile")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return (HttpCookieMode)base[FormsAuthenticationConfiguration.cookielessProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.cookielessProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection of user names and passwords.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection that contains the user names and passwords.</returns>
		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x0009F620 File Offset: 0x0009D820
		[ConfigurationProperty("credentials")]
		public FormsAuthenticationCredentials Credentials
		{
			get
			{
				return (FormsAuthenticationCredentials)base[FormsAuthenticationConfiguration.credentialsProp];
			}
		}

		/// <summary>Gets or sets the default URL.</summary>
		/// <returns>The URL to which to redirect the request after authentication. The default value is default.aspx.</returns>
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x0009F632 File Offset: 0x0009D832
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x0009F644 File Offset: 0x0009D844
		[ConfigurationProperty("defaultUrl", DefaultValue = "default.aspx")]
		[StringValidator(MinLength = 1)]
		public string DefaultUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.defaultUrlProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.defaultUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the domain name to be sent with forms authentication cookies.</summary>
		/// <returns>The name of the domain for the outgoing forms authentication cookies. Default is an empty string.</returns>
		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x0009F652 File Offset: 0x0009D852
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x0009F664 File Offset: 0x0009D864
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.domainProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.domainProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether authenticated users can be redirected to URLS in other applications.</summary>
		/// <returns>true if authenticated users can be redirected to URLs in other applications; otherwise false. The default is false.</returns>
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x0009F672 File Offset: 0x0009D872
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x0009F684 File Offset: 0x0009D884
		[ConfigurationProperty("enableCrossAppRedirects", DefaultValue = "False")]
		public bool EnableCrossAppRedirects
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.enableCrossAppRedirectsProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.enableCrossAppRedirectsProp] = value;
			}
		}

		/// <summary>Gets or sets the redirection URL for the request.</summary>
		/// <returns>The URL the request is redirected to when the user is not authenticated. The default value is login.aspx.</returns>
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x0009F697 File Offset: 0x0009D897
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x0009F6A9 File Offset: 0x0009D8A9
		[ConfigurationProperty("loginUrl", DefaultValue = "login.aspx")]
		[StringValidator(MinLength = 1)]
		public string LoginUrl
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.loginUrlProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.loginUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the HTTP cookie to use for request authentication.</returns>
		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x0009F6B7 File Offset: 0x0009D8B7
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x0009F6C9 File Offset: 0x0009D8C9
		[ConfigurationProperty("name", DefaultValue = ".ASPXAUTH")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.nameProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the cookie path.</summary>
		/// <returns>The path of the HTTP cookie to use for authentication. The default value is a slash (/), which represents the Web-application root.</returns>
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x0009F6D7 File Offset: 0x0009D8D7
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x0009F6E9 File Offset: 0x0009D8E9
		[ConfigurationProperty("path", DefaultValue = "/")]
		[StringValidator(MinLength = 1)]
		public string Path
		{
			get
			{
				return (string)base[FormsAuthenticationConfiguration.pathProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.pathProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.FormsProtectionEnum" /> enumeration values. The default value is All.Note   Be sure to use the default value for this property if you want both data validation and encryption to help protect the cookie. This option uses the configured data-validation algorithm based on the machineKey. Triple-DES (3DES) is used for encryption, if available and if the key is long enough (48 bytes or more). To improve the protection of your cookie, you may also want to set the <see cref="P:System.Web.Configuration.FormsAuthenticationConfiguration.RequireSSL" /> to true.</returns>
		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x0009F6F7 File Offset: 0x0009D8F7
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x0009F709 File Offset: 0x0009D909
		[ConfigurationProperty("protection", DefaultValue = "All")]
		public FormsProtectionEnum Protection
		{
			get
			{
				return (FormsProtectionEnum)base[FormsAuthenticationConfiguration.protectionProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.protectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
		/// <returns>true if an SSL connection is required; otherwise, false. The default is false.</returns>
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x0009F71C File Offset: 0x0009D91C
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x0009F72E File Offset: 0x0009D92E
		[ConfigurationProperty("requireSSL", DefaultValue = "False")]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.requireSSLProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.requireSSLProp] = value;
			}
		}

		/// <summary>Gets or sets the authentication sliding expiration.</summary>
		/// <returns>true if the sliding expiration is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x0009F741 File Offset: 0x0009D941
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x0009F753 File Offset: 0x0009D953
		[ConfigurationProperty("slidingExpiration", DefaultValue = "True")]
		public bool SlidingExpiration
		{
			get
			{
				return (bool)base[FormsAuthenticationConfiguration.slidingExpirationProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.slidingExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets the authentication time-out.</summary>
		/// <returns>The amount of time in minutes after which the authentication expires. The default value is 30 minutes.</returns>
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x0009F766 File Offset: 0x0009D966
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x0009F778 File Offset: 0x0009D978
		[ConfigurationProperty("timeout", DefaultValue = "00:30:00")]
		[TimeSpanValidator(MinValueString = "00:01:00")]
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[FormsAuthenticationConfiguration.timeoutProp];
			}
			set
			{
				base[FormsAuthenticationConfiguration.timeoutProp] = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x0009F78B File Offset: 0x0009D98B
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationConfiguration.properties;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x0009F794 File Offset: 0x0009D994
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SameSiteMode CookieSameSite
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

		/// <summary>Gets or sets a value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</summary>
		/// <returns>The ticket expiration-date compatibility mode.</returns>
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x0009F7B0 File Offset: 0x0009D9B0
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TicketCompatibilityMode.Framework20;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040020DC RID: 8412
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020DD RID: 8413
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseDeviceProfile, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020DE RID: 8414
		private static ConfigurationProperty credentialsProp = new ConfigurationProperty("credentials", typeof(FormsAuthenticationCredentials), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020DF RID: 8415
		private static ConfigurationProperty defaultUrlProp = new ConfigurationProperty("defaultUrl", typeof(string), "default.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E0 RID: 8416
		private static ConfigurationProperty domainProp = new ConfigurationProperty("domain", typeof(string), "");

		// Token: 0x040020E1 RID: 8417
		private static ConfigurationProperty enableCrossAppRedirectsProp = new ConfigurationProperty("enableCrossAppRedirects", typeof(bool), false);

		// Token: 0x040020E2 RID: 8418
		private static ConfigurationProperty loginUrlProp = new ConfigurationProperty("loginUrl", typeof(string), "login.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E3 RID: 8419
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), ".ASPXAUTH", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E4 RID: 8420
		private static ConfigurationProperty pathProp = new ConfigurationProperty("path", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E5 RID: 8421
		private static ConfigurationProperty protectionProp = new ConfigurationProperty("protection", typeof(FormsProtectionEnum), FormsProtectionEnum.All, new GenericEnumConverter(typeof(FormsProtectionEnum)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020E6 RID: 8422
		private static ConfigurationProperty requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);

		// Token: 0x040020E7 RID: 8423
		private static ConfigurationProperty slidingExpirationProp = new ConfigurationProperty("slidingExpiration", typeof(bool), true);

		// Token: 0x040020E8 RID: 8424
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040020E9 RID: 8425
		private static ConfigurationElementProperty elementProperty;
	}
}
