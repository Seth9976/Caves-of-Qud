using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Allows you to specify the personalization provider and set personalization authorizations. This class cannot be inherited.</summary>
	// Token: 0x020005F0 RID: 1520
	public sealed class WebPartsPersonalization : ConfigurationElement
	{
		// Token: 0x06004203 RID: 16899 RVA: 0x000AAA30 File Offset: 0x000A8C30
		static WebPartsPersonalization()
		{
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.authorizationProp);
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.defaultProviderProp);
			WebPartsPersonalization.properties.Add(WebPartsPersonalization.providersProp);
		}

		/// <summary>Gets an <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</returns>
		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000AAAE9 File Offset: 0x000A8CE9
		[ConfigurationProperty("authorization")]
		public WebPartsPersonalizationAuthorization Authorization
		{
			get
			{
				return (WebPartsPersonalizationAuthorization)base[WebPartsPersonalization.authorizationProp];
			}
		}

		/// <summary>Gets or sets the name of the default Web Parts personalization provider.</summary>
		/// <returns>The name of the default Web Parts personalization provider.</returns>
		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x000AAAFB File Offset: 0x000A8CFB
		// (set) Token: 0x06004206 RID: 16902 RVA: 0x000AAB0D File Offset: 0x000A8D0D
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlPersonalizationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[WebPartsPersonalization.defaultProviderProp];
			}
			set
			{
				base[WebPartsPersonalization.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</returns>
		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x000AAB1B File Offset: 0x000A8D1B
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[WebPartsPersonalization.providersProp];
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x000AAB2D File Offset: 0x000A8D2D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalization.properties;
			}
		}

		// Token: 0x04002366 RID: 9062
		private static ConfigurationProperty authorizationProp = new ConfigurationProperty("authorization", typeof(WebPartsPersonalizationAuthorization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002367 RID: 9063
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlPersonalizationProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002368 RID: 9064
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002369 RID: 9065
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
