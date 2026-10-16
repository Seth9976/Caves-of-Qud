using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x02000760 RID: 1888
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x06003B7F RID: 15231 RVA: 0x000CC488 File Offset: 0x000CA688
		static AuthenticationModulesSection()
		{
			AuthenticationModulesSection.properties.Add(AuthenticationModulesSection.authenticationModulesProp);
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000CC4BE File Offset: 0x000CA6BE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules. </returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000CC4C5 File Offset: 0x000CA6C5
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[AuthenticationModulesSection.authenticationModulesProp];
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x0400239E RID: 9118
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400239F RID: 9119
		private static ConfigurationProperty authenticationModulesProp = new ConfigurationProperty("", typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
