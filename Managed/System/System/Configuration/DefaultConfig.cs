using System;
using System.Runtime.CompilerServices;

namespace System.Configuration
{
	// Token: 0x020001AA RID: 426
	internal class DefaultConfig : IConfigurationSystem
	{
		// Token: 0x06000B1E RID: 2846 RVA: 0x0000219B File Offset: 0x0000039B
		private DefaultConfig()
		{
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002F3BE File Offset: 0x0002D5BE
		public static DefaultConfig GetInstance()
		{
			return DefaultConfig.instance;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002F3C5 File Offset: 0x0002D5C5
		[Obsolete("This method is obsolete.  Please use System.Configuration.ConfigurationManager.GetConfig")]
		public object GetConfig(string sectionName)
		{
			this.Init();
			return this.config.GetConfig(sectionName);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		public void Init()
		{
			lock (this)
			{
				if (this.config == null)
				{
					ConfigurationData configurationData = new ConfigurationData();
					if (!configurationData.LoadString(DefaultConfig.GetBundledMachineConfig()) && !configurationData.Load(DefaultConfig.GetMachineConfigPath()))
					{
						throw new ConfigurationException("Cannot find " + DefaultConfig.GetMachineConfigPath());
					}
					string appConfigPath = DefaultConfig.GetAppConfigPath();
					if (appConfigPath == null)
					{
						this.config = configurationData;
					}
					else
					{
						ConfigurationData configurationData2 = new ConfigurationData(configurationData);
						if (configurationData2.Load(appConfigPath))
						{
							this.config = configurationData2;
						}
						else
						{
							this.config = configurationData;
						}
					}
				}
			}
		}

		// Token: 0x06000B22 RID: 2850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002F488 File Offset: 0x0002D688
		internal static string GetBundledMachineConfig()
		{
			return DefaultConfig.get_bundled_machine_config();
		}

		// Token: 0x06000B24 RID: 2852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_machine_config_path();

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002F48F File Offset: 0x0002D68F
		internal static string GetMachineConfigPath()
		{
			return DefaultConfig.get_machine_config_path();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002F498 File Offset: 0x0002D698
		private static string GetAppConfigPath()
		{
			string configurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			if (configurationFile == null || configurationFile.Length == 0)
			{
				return null;
			}
			return configurationFile;
		}

		// Token: 0x04000766 RID: 1894
		private static readonly DefaultConfig instance = new DefaultConfig();

		// Token: 0x04000767 RID: 1895
		private ConfigurationData config;
	}
}
