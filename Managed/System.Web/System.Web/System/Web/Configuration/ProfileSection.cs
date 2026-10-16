using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.ProfileSection" /> class provides a way to programmatically access and modify the profile section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005C7 RID: 1479
	public sealed class ProfileSection : ConfigurationSection
	{
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000A65A4 File Offset: 0x000A47A4
		static ProfileSection()
		{
			ProfileSection.properties.Add(ProfileSection.automaticSaveEnabledProp);
			ProfileSection.properties.Add(ProfileSection.defaultProviderProp);
			ProfileSection.properties.Add(ProfileSection.enabledProp);
			ProfileSection.properties.Add(ProfileSection.inheritsProp);
			ProfileSection.properties.Add(ProfileSection.propertySettingsProp);
			ProfileSection.properties.Add(ProfileSection.providersProp);
		}

		/// <summary>Gets or sets a value that determines whether changes to user-profile information are automatically saved on page exit.</summary>
		/// <returns>true if profile information is automatically saved on page exit; otherwise, false. The default is true.</returns>
		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000A66C1 File Offset: 0x000A48C1
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x000A66D3 File Offset: 0x000A48D3
		[ConfigurationProperty("automaticSaveEnabled", DefaultValue = true)]
		public bool AutomaticSaveEnabled
		{
			get
			{
				return (bool)base[ProfileSection.automaticSaveEnabledProp];
			}
			set
			{
				base[ProfileSection.automaticSaveEnabledProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the default profile provider. </summary>
		/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> collection, or an empty string (""). The default is "AspNetSqlProfileProvider."</returns>
		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000A66E6 File Offset: 0x000A48E6
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x000A66F8 File Offset: 0x000A48F8
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlProfileProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProfileSection.defaultProviderProp];
			}
			set
			{
				base[ProfileSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET profile feature is enabled.</summary>
		/// <returns>true if the ASP.NET compilation system should generate a ProfileCommon class that can be used to access information about individual user profiles; otherwise, false. The default is true.</returns>
		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000A6706 File Offset: 0x000A4906
		// (set) Token: 0x06003FD6 RID: 16342 RVA: 0x000A6718 File Offset: 0x000A4918
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ProfileSection.enabledProp];
			}
			set
			{
				base[ProfileSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets a type reference for a custom type derived from <see cref="T:System.Web.Profile.ProfileBase" />.</summary>
		/// <returns>A valid type reference, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x000A672B File Offset: 0x000A492B
		// (set) Token: 0x06003FD8 RID: 16344 RVA: 0x000A673D File Offset: 0x000A493D
		[ConfigurationProperty("inherits", DefaultValue = "")]
		public string Inherits
		{
			get
			{
				return (string)base[ProfileSection.inheritsProp];
			}
			set
			{
				base[ProfileSection.inheritsProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object that contains all the properties defined within the properties subsection of the profile section of the configuration file.</returns>
		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000A674B File Offset: 0x000A494B
		[ConfigurationProperty("properties")]
		public RootProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (RootProfilePropertySettingsCollection)base[ProfileSection.propertySettingsProp];
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers defined within the providers subsection of the profile section of the configuration file.</returns>
		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000A675D File Offset: 0x000A495D
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProfileSection.providersProp];
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x000A676F File Offset: 0x000A496F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSection.properties;
			}
		}

		// Token: 0x040022B7 RID: 8887
		private static ConfigurationProperty automaticSaveEnabledProp = new ConfigurationProperty("automaticSaveEnabled", typeof(bool), true);

		// Token: 0x040022B8 RID: 8888
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlProfileProvider");

		// Token: 0x040022B9 RID: 8889
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x040022BA RID: 8890
		private static ConfigurationProperty inheritsProp = new ConfigurationProperty("inherits", typeof(string), "");

		// Token: 0x040022BB RID: 8891
		private static ConfigurationProperty propertySettingsProp = new ConfigurationProperty("properties", typeof(RootProfilePropertySettingsCollection));

		// Token: 0x040022BC RID: 8892
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));

		// Token: 0x040022BD RID: 8893
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
