using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the authentication for a Web application. This class cannot be inherited.</summary>
	// Token: 0x0200057E RID: 1406
	public sealed class AuthenticationSection : ConfigurationSection
	{
		// Token: 0x06003B53 RID: 15187 RVA: 0x0009CCAC File Offset: 0x0009AEAC
		static AuthenticationSection()
		{
			AuthenticationSection.properties.Add(AuthenticationSection.formsProp);
			AuthenticationSection.properties.Add(AuthenticationSection.passportProp);
			AuthenticationSection.properties.Add(AuthenticationSection.modeProp);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x0009CD66 File Offset: 0x0009AF66
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property.</summary>
		/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property that contains information used during forms-based authentication.</returns>
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x0009CD6F File Offset: 0x0009AF6F
		[ConfigurationProperty("forms")]
		public FormsAuthenticationConfiguration Forms
		{
			get
			{
				return (FormsAuthenticationConfiguration)base[AuthenticationSection.formsProp];
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property.</summary>
		/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property that contains information used during passport-based authentication.</returns>
		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x0009CD81 File Offset: 0x0009AF81
		[ConfigurationProperty("passport")]
		[Obsolete("This property is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		public PassportAuthentication Passport
		{
			get
			{
				return (PassportAuthentication)base[AuthenticationSection.passportProp];
			}
		}

		/// <summary>Gets or sets the authentication modality.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.AuthenticationMode" /> values.</returns>
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x0009CD93 File Offset: 0x0009AF93
		// (set) Token: 0x06003B59 RID: 15193 RVA: 0x0009CDA5 File Offset: 0x0009AFA5
		[ConfigurationProperty("mode", DefaultValue = "Windows")]
		public AuthenticationMode Mode
		{
			get
			{
				return (AuthenticationMode)base[AuthenticationSection.modeProp];
			}
			set
			{
				base[AuthenticationSection.modeProp] = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x0009CDB8 File Offset: 0x0009AFB8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationSection.properties;
			}
		}

		// Token: 0x04002082 RID: 8322
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002083 RID: 8323
		private static ConfigurationProperty formsProp = new ConfigurationProperty("forms", typeof(FormsAuthenticationConfiguration), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002084 RID: 8324
		private static ConfigurationProperty passportProp = new ConfigurationProperty("passport", typeof(PassportAuthentication), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002085 RID: 8325
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(AuthenticationMode), AuthenticationMode.Windows, new GenericEnumConverter(typeof(AuthenticationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
	}
}
