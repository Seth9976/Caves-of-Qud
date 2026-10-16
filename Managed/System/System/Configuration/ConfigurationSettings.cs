using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Provides runtime versions 1.0 and 1.1 support for reading configuration sections and common configuration settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A9 RID: 425
	public sealed class ConfigurationSettings
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0000219B File Offset: 0x0000039B
		private ConfigurationSettings()
		{
		}

		/// <summary>Returns the <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.NoteThe <see cref="T:System.Configuration.ConfigurationSettings" /> class provides backward compatibility only. You should use the <see cref="T:System.Configuration.ConfigurationManager" /> class or <see cref="T:System.Web.Configuration.WebConfigurationManager" /> class instead.</returns>
		/// <param name="sectionName">A configuration name and path, such as "system.net/settings".</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">Unable to retrieve the requested section.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B1A RID: 2842 RVA: 0x0002DC16 File Offset: 0x0002BE16
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		/// <summary>Gets a read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section of the configuration file.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section from the configuration file.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002F32C File Offset: 0x0002D52C
		[Obsolete("This property is obsolete.  Please use System.Configuration.ConfigurationManager.AppSettings")]
		public static NameValueCollection AppSettings
		{
			get
			{
				object obj = ConfigurationManager.GetSection("appSettings");
				if (obj == null)
				{
					obj = new NameValueCollection();
				}
				return (NameValueCollection)obj;
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002F354 File Offset: 0x0002D554
		internal static IConfigurationSystem ChangeConfigurationSystem(IConfigurationSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationSettings.lockobj;
			IConfigurationSystem configurationSystem2;
			lock (obj)
			{
				IConfigurationSystem configurationSystem = ConfigurationSettings.config;
				ConfigurationSettings.config = newSystem;
				configurationSystem2 = configurationSystem;
			}
			return configurationSystem2;
		}

		// Token: 0x04000764 RID: 1892
		private static IConfigurationSystem config = DefaultConfig.GetInstance();

		// Token: 0x04000765 RID: 1893
		private static object lockobj = new object();
	}
}
