using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.SessionState;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the session state for a Web application.</summary>
	// Token: 0x020005D3 RID: 1491
	public sealed class SessionStateSection : ConfigurationSection
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x000A76A8 File Offset: 0x000A58A8
		static SessionStateSection()
		{
			SessionStateSection.properties.Add(SessionStateSection.allowCustomSqlDatabaseProp);
			SessionStateSection.properties.Add(SessionStateSection.cookielessProp);
			SessionStateSection.properties.Add(SessionStateSection.cookieNameProp);
			SessionStateSection.properties.Add(SessionStateSection.customProviderProp);
			SessionStateSection.properties.Add(SessionStateSection.modeProp);
			SessionStateSection.properties.Add(SessionStateSection.partitionResolverTypeProp);
			SessionStateSection.properties.Add(SessionStateSection.providersProp);
			SessionStateSection.properties.Add(SessionStateSection.regenerateExpiredSessionIdProp);
			SessionStateSection.properties.Add(SessionStateSection.sessionIDManagerTypeProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlCommandTimeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlConnectionStringProp);
			SessionStateSection.properties.Add(SessionStateSection.stateConnectionStringProp);
			SessionStateSection.properties.Add(SessionStateSection.stateNetworkTimeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.timeoutProp);
			SessionStateSection.properties.Add(SessionStateSection.useHostingIdentityProp);
			SessionStateSection.properties.Add(SessionStateSection.compressionEnabledProp);
			SessionStateSection.properties.Add(SessionStateSection.sqlConnectionRetryIntervalProp);
			SessionStateSection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SessionStateSection), new ValidatorCallback(SessionStateSection.ValidateElement)));
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x0009D4C1 File Offset: 0x0009B6C1
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets or sets a value indicating whether the user can specify the initial catalog value in the <see cref="P:System.Web.Configuration.SessionStateSection.SqlConnectionString" /> property.</summary>
		/// <returns>true if the user is allowed to specify the catalog; otherwise, false. The default value is false.</returns>
		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000A7A69 File Offset: 0x000A5C69
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x000A7A7B File Offset: 0x000A5C7B
		[ConfigurationProperty("allowCustomSqlDatabase", DefaultValue = "False")]
		public bool AllowCustomSqlDatabase
		{
			get
			{
				return (bool)base[SessionStateSection.allowCustomSqlDatabaseProp];
			}
			set
			{
				base[SessionStateSection.allowCustomSqlDatabaseProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether cookies are used to identify client sessions.</summary>
		/// <returns>true if all requests are treated as cookieless, or false if no requests are treated as cookieless, or one of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value in ASP.NET version 2.0 is <see cref="F:System.Web.HttpCookieMode.AutoDetect" />. In earlier versions, the default value was false.</returns>
		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000A7A8E File Offset: 0x000A5C8E
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x000A7AA6 File Offset: 0x000A5CA6
		[ConfigurationProperty("cookieless")]
		public HttpCookieMode Cookieless
		{
			get
			{
				return this.ParseCookieMode((string)base[SessionStateSection.cookielessProp]);
			}
			set
			{
				base[SessionStateSection.cookielessProp] = value.ToString();
			}
		}

		/// <summary>Gets or sets the cookie name.</summary>
		/// <returns>The name of the HTTP cookie to use for session identification.</returns>
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000A7AC0 File Offset: 0x000A5CC0
		// (set) Token: 0x06004061 RID: 16481 RVA: 0x000A7AD2 File Offset: 0x000A5CD2
		[ConfigurationProperty("cookieName", DefaultValue = "ASP.NET_SessionId")]
		public string CookieName
		{
			get
			{
				return (string)base[SessionStateSection.cookieNameProp];
			}
			set
			{
				base[SessionStateSection.cookieNameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the custom provider from the <see cref="P:System.Web.Configuration.SessionStateSection.Providers" /> collection.</summary>
		/// <returns>The custom provider name.</returns>
		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000A7AE0 File Offset: 0x000A5CE0
		// (set) Token: 0x06004063 RID: 16483 RVA: 0x000A7AF2 File Offset: 0x000A5CF2
		[ConfigurationProperty("customProvider", DefaultValue = "")]
		public string CustomProvider
		{
			get
			{
				return (string)base[SessionStateSection.customProviderProp];
			}
			set
			{
				base[SessionStateSection.customProviderProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying where to store the session state.</summary>
		/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values. The default value is <see cref="F:System.Web.SessionState.SessionStateMode.InProc" />.</returns>
		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000A7B00 File Offset: 0x000A5D00
		// (set) Token: 0x06004065 RID: 16485 RVA: 0x000A7B12 File Offset: 0x000A5D12
		[ConfigurationProperty("mode", DefaultValue = "InProc")]
		public SessionStateMode Mode
		{
			get
			{
				return (SessionStateMode)base[SessionStateSection.modeProp];
			}
			set
			{
				base[SessionStateSection.modeProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying where to store the session state.</summary>
		/// <returns>A value specifying where to store the session state, or an empty string ("").</returns>
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x000A7B25 File Offset: 0x000A5D25
		// (set) Token: 0x06004067 RID: 16487 RVA: 0x000A7B37 File Offset: 0x000A5D37
		[ConfigurationProperty("partitionResolverType", DefaultValue = "")]
		public string PartitionResolverType
		{
			get
			{
				return (string)base[SessionStateSection.partitionResolverTypeProp];
			}
			set
			{
				base[SessionStateSection.partitionResolverTypeProp] = value;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Configuration.ProviderSettingsCollection" /> providers.</summary>
		/// <returns>The collection containing the <see cref="T:System.Web.Configuration.SessionStateSection" /> providers.</returns>
		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x000A7B45 File Offset: 0x000A5D45
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[SessionStateSection.providersProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether the session Id will be re-issued when an expired session ID is specified by the client.</summary>
		/// <returns>true if the session ID must be regenerated; otherwise, false. The default value is true.</returns>
		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000A7B57 File Offset: 0x000A5D57
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x000A7B69 File Offset: 0x000A5D69
		[ConfigurationProperty("regenerateExpiredSessionId", DefaultValue = "True")]
		public bool RegenerateExpiredSessionId
		{
			get
			{
				return (bool)base[SessionStateSection.regenerateExpiredSessionIdProp];
			}
			set
			{
				base[SessionStateSection.regenerateExpiredSessionIdProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the fully qualified type of session ID Manager.</summary>
		/// <returns>A fully qualified type of session ID Manager.</returns>
		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000A7B7C File Offset: 0x000A5D7C
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x000A7B8E File Offset: 0x000A5D8E
		[ConfigurationProperty("sessionIDManagerType", DefaultValue = "")]
		public string SessionIDManagerType
		{
			get
			{
				return (string)base[SessionStateSection.sessionIDManagerTypeProp];
			}
			set
			{
				base[SessionStateSection.sessionIDManagerTypeProp] = value;
			}
		}

		/// <summary>Gets or sets the duration time-out for the SQL commands using the SQL Server session state mode.</summary>
		/// <returns>The amount of time, in seconds, after which a SQL command will time out. The default is 30 seconds.</returns>
		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000A7B9C File Offset: 0x000A5D9C
		// (set) Token: 0x0600406E RID: 16494 RVA: 0x000A7BAE File Offset: 0x000A5DAE
		[ConfigurationProperty("sqlCommandTimeout", DefaultValue = "00:00:30")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan SqlCommandTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.sqlCommandTimeoutProp];
			}
			set
			{
				base[SessionStateSection.sqlCommandTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the SQL connection string.</summary>
		/// <returns>The SQL connection string. Its default value is the generic string: "data source=127.0.0.1;Integrated Security=SSPI"</returns>
		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000A7BC1 File Offset: 0x000A5DC1
		// (set) Token: 0x06004070 RID: 16496 RVA: 0x000A7BD3 File Offset: 0x000A5DD3
		[ConfigurationProperty("sqlConnectionString", DefaultValue = "data source=localhost;Integrated Security=SSPI")]
		public string SqlConnectionString
		{
			get
			{
				return (string)base[SessionStateSection.sqlConnectionStringProp];
			}
			set
			{
				base[SessionStateSection.sqlConnectionStringProp] = value;
			}
		}

		/// <summary>Gets or sets the state server connection string.</summary>
		/// <returns>The state server connection string.</returns>
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000A7BE1 File Offset: 0x000A5DE1
		// (set) Token: 0x06004072 RID: 16498 RVA: 0x000A7BF3 File Offset: 0x000A5DF3
		[ConfigurationProperty("stateConnectionString", DefaultValue = "tcpip=loopback:42424")]
		public string StateConnectionString
		{
			get
			{
				return (string)base[SessionStateSection.stateConnectionStringProp];
			}
			set
			{
				base[SessionStateSection.stateConnectionStringProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time the network connection between the Web server and the state server can remain idle. </summary>
		/// <returns>The time, in seconds, that the network connection between the Web server and the state server can remain idle before the session is abandoned. The default value is 10 seconds.</returns>
		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000A7C01 File Offset: 0x000A5E01
		// (set) Token: 0x06004074 RID: 16500 RVA: 0x000A7C13 File Offset: 0x000A5E13
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		[ConfigurationProperty("stateNetworkTimeout", DefaultValue = "00:00:10")]
		public TimeSpan StateNetworkTimeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.stateNetworkTimeoutProp];
			}
			set
			{
				base[SessionStateSection.stateNetworkTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the session time-out</summary>
		/// <returns>The session time-out, in minutes. The default value is 20 minutes.</returns>
		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000A7C26 File Offset: 0x000A5E26
		// (set) Token: 0x06004076 RID: 16502 RVA: 0x000A7C38 File Offset: 0x000A5E38
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("timeout", DefaultValue = "00:20:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.timeoutProp];
			}
			set
			{
				base[SessionStateSection.timeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the whether the session state will use client impersonation when available, or will always revert to the hosting identity.</summary>
		/// <returns>true if Web application should revert to hosting identity; otherwise, false. The default value is true.</returns>
		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x000A7C4B File Offset: 0x000A5E4B
		// (set) Token: 0x06004078 RID: 16504 RVA: 0x000A7C5D File Offset: 0x000A5E5D
		[ConfigurationProperty("useHostingIdentity", DefaultValue = "True")]
		public bool UseHostingIdentity
		{
			get
			{
				return (bool)base[SessionStateSection.useHostingIdentityProp];
			}
			set
			{
				base[SessionStateSection.useHostingIdentityProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether compression is enabled for session-state data.</summary>
		/// <returns>true if compression is enabled; otherwise false. The default is false. </returns>
		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x000A7C70 File Offset: 0x000A5E70
		// (set) Token: 0x0600407A RID: 16506 RVA: 0x000A7C82 File Offset: 0x000A5E82
		[ConfigurationProperty("compressionEnabled", DefaultValue = false)]
		public bool CompressionEnabled
		{
			get
			{
				return (bool)base[SessionStateSection.compressionEnabledProp];
			}
			set
			{
				base[SessionStateSection.compressionEnabledProp] = value;
			}
		}

		/// <summary>Gets or sets the time interval that should elapse before ASP.NET reconnects to the database.</summary>
		/// <returns>The time interval that should elapse before ASP.NET reconnects to the database.</returns>
		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x000A7C95 File Offset: 0x000A5E95
		// (set) Token: 0x0600407C RID: 16508 RVA: 0x000A7CA7 File Offset: 0x000A5EA7
		[ConfigurationProperty("sqlConnectionRetryInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan SqlConnectionRetryInterval
		{
			get
			{
				return (TimeSpan)base[SessionStateSection.sqlConnectionRetryIntervalProp];
			}
			set
			{
				base[SessionStateSection.sqlConnectionRetryIntervalProp] = value;
			}
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x000A7CBA File Offset: 0x000A5EBA
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SessionStateSection.elementProperty;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x000A7CC1 File Offset: 0x000A5EC1
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionStateSection.properties;
			}
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000A7CC8 File Offset: 0x000A5EC8
		private HttpCookieMode ParseCookieMode(string s)
		{
			if (s == "true")
			{
				return HttpCookieMode.UseUri;
			}
			if (s == "false" || s == null)
			{
				return HttpCookieMode.UseCookies;
			}
			HttpCookieMode httpCookieMode;
			try
			{
				httpCookieMode = (HttpCookieMode)Enum.Parse(typeof(HttpCookieMode), s);
			}
			catch
			{
				httpCookieMode = HttpCookieMode.UseCookies;
			}
			return httpCookieMode;
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x000A7D28 File Offset: 0x000A5F28
		// (set) Token: 0x06004082 RID: 16514 RVA: 0x000A7D36 File Offset: 0x000A5F36
		internal bool CookieLess
		{
			get
			{
				return this.Cookieless != HttpCookieMode.UseCookies;
			}
			set
			{
				this.Cookieless = (value ? HttpCookieMode.UseUri : HttpCookieMode.UseCookies);
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000A7D48 File Offset: 0x000A5F48
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		// Token: 0x040022F2 RID: 8946
		internal static readonly string DefaultSqlConnectionString = "data source=localhost;Integrated Security=SSPI";

		// Token: 0x040022F3 RID: 8947
		private static ConfigurationProperty allowCustomSqlDatabaseProp = new ConfigurationProperty("allowCustomSqlDatabase", typeof(bool), false);

		// Token: 0x040022F4 RID: 8948
		private static ConfigurationProperty cookielessProp = new ConfigurationProperty("cookieless", typeof(string), null);

		// Token: 0x040022F5 RID: 8949
		private static ConfigurationProperty cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), "ASP.NET_SessionId");

		// Token: 0x040022F6 RID: 8950
		private static ConfigurationProperty customProviderProp = new ConfigurationProperty("customProvider", typeof(string), "");

		// Token: 0x040022F7 RID: 8951
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(SessionStateMode), SessionStateMode.InProc, new GenericEnumConverter(typeof(SessionStateMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x040022F8 RID: 8952
		private static ConfigurationProperty partitionResolverTypeProp = new ConfigurationProperty("partitionResolverType", typeof(string), "");

		// Token: 0x040022F9 RID: 8953
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x040022FA RID: 8954
		private static ConfigurationProperty regenerateExpiredSessionIdProp = new ConfigurationProperty("regenerateExpiredSessionId", typeof(bool), true);

		// Token: 0x040022FB RID: 8955
		private static ConfigurationProperty sessionIDManagerTypeProp = new ConfigurationProperty("sessionIDManagerType", typeof(string), "");

		// Token: 0x040022FC RID: 8956
		private static ConfigurationProperty sqlCommandTimeoutProp = new ConfigurationProperty("sqlCommandTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x040022FD RID: 8957
		private static ConfigurationProperty sqlConnectionStringProp = new ConfigurationProperty("sqlConnectionString", typeof(string), SessionStateSection.DefaultSqlConnectionString);

		// Token: 0x040022FE RID: 8958
		private static ConfigurationProperty stateConnectionStringProp = new ConfigurationProperty("stateConnectionString", typeof(string), "tcpip=loopback:42424");

		// Token: 0x040022FF RID: 8959
		private static ConfigurationProperty stateNetworkTimeoutProp = new ConfigurationProperty("stateNetworkTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(10.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002300 RID: 8960
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(20.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002301 RID: 8961
		private static ConfigurationProperty useHostingIdentityProp = new ConfigurationProperty("useHostingIdentity", typeof(bool), true);

		// Token: 0x04002302 RID: 8962
		private static ConfigurationProperty compressionEnabledProp = new ConfigurationProperty("compressionEnabled", typeof(bool), false);

		// Token: 0x04002303 RID: 8963
		private static ConfigurationProperty sqlConnectionRetryIntervalProp = new ConfigurationProperty("sqlConnectionRetryIntervalProp", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002304 RID: 8964
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002305 RID: 8965
		private static ConfigurationElementProperty elementProperty;
	}
}
