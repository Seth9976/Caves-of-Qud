using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the user's credentials for Web applications that use forms-based authentication. </summary>
	// Token: 0x0200059B RID: 1435
	public sealed class FormsAuthenticationUser : ConfigurationElement
	{
		// Token: 0x06003CDA RID: 15578 RVA: 0x0009F894 File Offset: 0x0009DA94
		static FormsAuthenticationUser()
		{
			FormsAuthenticationUser.properties.Add(FormsAuthenticationUser.nameProp);
			FormsAuthenticationUser.properties.Add(FormsAuthenticationUser.passwordProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> class using the passed parameters.</summary>
		/// <param name="name">User's name.</param>
		/// <param name="password">User's password.</param>
		// Token: 0x06003CDB RID: 15579 RVA: 0x0009F911 File Offset: 0x0009DB11
		public FormsAuthenticationUser(string name, string password)
		{
			this.Name = name;
			this.Password = password;
		}

		/// <summary>Gets or sets the logon user name.</summary>
		/// <returns>The logon user name required by the application.</returns>
		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x0009F927 File Offset: 0x0009DB27
		// (set) Token: 0x06003CDD RID: 15581 RVA: 0x0009F939 File Offset: 0x0009DB39
		[StringValidator]
		[TypeConverter(typeof(LowerCaseStringConverter))]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[FormsAuthenticationUser.nameProp];
			}
			set
			{
				base[FormsAuthenticationUser.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the user's password.</summary>
		/// <returns>The user's password required by the application.</returns>
		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x0009F947 File Offset: 0x0009DB47
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x0009F959 File Offset: 0x0009DB59
		[StringValidator]
		[ConfigurationProperty("password", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Password
		{
			get
			{
				return (string)base[FormsAuthenticationUser.passwordProp];
			}
			set
			{
				base[FormsAuthenticationUser.passwordProp] = value;
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x0009F967 File Offset: 0x0009DB67
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUser.properties;
			}
		}

		// Token: 0x040020ED RID: 8429
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", new LowerCaseStringConverter(), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020EE RID: 8430
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020EF RID: 8431
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
