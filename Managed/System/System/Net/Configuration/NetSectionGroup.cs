using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Gets the section group information for the networking namespaces. This class cannot be inherited.</summary>
	// Token: 0x02000773 RID: 1907
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.NetSectionGroup" /> class. </summary>
		// Token: 0x06003BFF RID: 15359 RVA: 0x0002E9E5 File Offset: 0x0002CBE5
		[MonoTODO]
		public NetSectionGroup()
		{
		}

		/// <summary>Gets the configuration section containing the authentication modules registered for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> object.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000CD79C File Offset: 0x000CB99C
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		/// <summary>Gets the configuration section containing the connection management settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> object.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x000CD7B3 File Offset: 0x000CB9B3
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		/// <summary>Gets the configuration section containing the default Web proxy server settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.DefaultProxySection" /> object.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000CD7CA File Offset: 0x000CB9CA
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		/// <summary>Gets the configuration section containing the SMTP client e-mail settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> object.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x000CD7E1 File Offset: 0x000CB9E1
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		/// <summary>Gets the configuration section containing the cache configuration settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.RequestCachingSection" /> object.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000CD7F8 File Offset: 0x000CB9F8
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		/// <summary>Gets the configuration section containing the network settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SettingsSection" /> object.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x000CD80F File Offset: 0x000CBA0F
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		/// <summary>Gets the configuration section containing the modules registered for use with the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> object.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000CD826 File Offset: 0x000CBA26
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}

		/// <summary>Gets the System.Net configuration section group from the specified configuration file.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.NetSectionGroup" /> that represents the System.Net settings in <paramref name="config" />.</returns>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents a configuration file.</param>
		// Token: 0x06003C07 RID: 15367 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			throw new NotImplementedException();
		}
	}
}
