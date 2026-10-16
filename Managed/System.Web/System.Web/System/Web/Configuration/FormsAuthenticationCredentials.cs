using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures user credentials for ASP.NET applications that use form-based authentication.</summary>
	// Token: 0x0200059A RID: 1434
	public sealed class FormsAuthenticationCredentials : ConfigurationElement
	{
		// Token: 0x06003CD4 RID: 15572 RVA: 0x0009F7CC File Offset: 0x0009D9CC
		static FormsAuthenticationCredentials()
		{
			FormsAuthenticationCredentials.properties.Add(FormsAuthenticationCredentials.passwordFormatProp);
			FormsAuthenticationCredentials.properties.Add(FormsAuthenticationCredentials.usersProp);
		}

		/// <summary>Gets or sets the password format.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> values.</returns>
		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x0009F856 File Offset: 0x0009DA56
		// (set) Token: 0x06003CD7 RID: 15575 RVA: 0x0009F868 File Offset: 0x0009DA68
		[ConfigurationProperty("passwordFormat", DefaultValue = "SHA1")]
		public FormsAuthPasswordFormat PasswordFormat
		{
			get
			{
				return (FormsAuthPasswordFormat)base[FormsAuthenticationCredentials.passwordFormatProp];
			}
			set
			{
				base[FormsAuthenticationCredentials.passwordFormatProp] = value;
			}
		}

		/// <summary>Gets the users' names and password credentials.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> that contains the users' names and password credentials.</returns>
		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x0009F87B File Offset: 0x0009DA7B
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public FormsAuthenticationUserCollection Users
		{
			get
			{
				return (FormsAuthenticationUserCollection)base[FormsAuthenticationCredentials.usersProp];
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x0009F88D File Offset: 0x0009DA8D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationCredentials.properties;
			}
		}

		// Token: 0x040020EA RID: 8426
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020EB RID: 8427
		private static ConfigurationProperty passwordFormatProp = new ConfigurationProperty("passwordFormat", typeof(FormsAuthPasswordFormat), FormsAuthPasswordFormat.SHA1, new GenericEnumConverter(typeof(FormsAuthPasswordFormat)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020EC RID: 8428
		private static ConfigurationProperty usersProp = new ConfigurationProperty("", typeof(FormsAuthenticationUserCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
