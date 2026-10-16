using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the identity of a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005AF RID: 1455
	public sealed class IdentitySection : ConfigurationSection
	{
		// Token: 0x06003E66 RID: 15974 RVA: 0x000A33D8 File Offset: 0x000A15D8
		static IdentitySection()
		{
			IdentitySection.properties.Add(IdentitySection.impersonateProp);
			IdentitySection.properties.Add(IdentitySection.passwordProp);
			IdentitySection.properties.Add(IdentitySection.userNameProp);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
		}

		/// <summary>Gets or sets a value indicating whether client impersonation is used on each request.</summary>
		/// <returns>true if client impersonation is used; otherwise, false. The default value is false.</returns>
		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003E6A RID: 15978 RVA: 0x000A3477 File Offset: 0x000A1677
		// (set) Token: 0x06003E6B RID: 15979 RVA: 0x000A3489 File Offset: 0x000A1689
		[ConfigurationProperty("impersonate", DefaultValue = "False")]
		public bool Impersonate
		{
			get
			{
				return (bool)base[IdentitySection.impersonateProp];
			}
			set
			{
				base[IdentitySection.impersonateProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the password to use for impersonation. </summary>
		/// <returns>The password to use for impersonation. </returns>
		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000A349C File Offset: 0x000A169C
		// (set) Token: 0x06003E6D RID: 15981 RVA: 0x000A34AE File Offset: 0x000A16AE
		[ConfigurationProperty("password", DefaultValue = "")]
		public string Password
		{
			get
			{
				return (string)base[IdentitySection.passwordProp];
			}
			set
			{
				base[IdentitySection.passwordProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the user name to use for impersonation.</summary>
		/// <returns>The user name to use for impersonation. </returns>
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000A34BC File Offset: 0x000A16BC
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x000A34CE File Offset: 0x000A16CE
		[ConfigurationProperty("userName", DefaultValue = "")]
		public string UserName
		{
			get
			{
				return (string)base[IdentitySection.userNameProp];
			}
			set
			{
				base[IdentitySection.userNameProp] = value;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000A34DC File Offset: 0x000A16DC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdentitySection.properties;
			}
		}

		// Token: 0x0400222F RID: 8751
		private static ConfigurationProperty impersonateProp = new ConfigurationProperty("impersonate", typeof(bool), false);

		// Token: 0x04002230 RID: 8752
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "");

		// Token: 0x04002231 RID: 8753
		private static ConfigurationProperty userNameProp = new ConfigurationProperty("userName", typeof(string), "");

		// Token: 0x04002232 RID: 8754
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
