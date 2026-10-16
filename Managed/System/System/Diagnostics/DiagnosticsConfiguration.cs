using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021B RID: 539
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000453A8 File Offset: 0x000435A8
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x000453D0 File Offset: 0x000435D0
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00045404 File Offset: 0x00043604
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00045434 File Offset: 0x00043634
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0004546C File Offset: 0x0004366C
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000454A0 File Offset: 0x000436A0
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x000454D4 File Offset: 0x000436D4
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00045508 File Offset: 0x00043708
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00045530 File Offset: 0x00043730
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0004555D File Offset: 0x0004375D
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0004556C File Offset: 0x0004376C
		private static SystemDiagnosticsSection GetConfigSection()
		{
			object section = PrivilegedConfigurationManager.GetSection("system.diagnostics");
			if (section is SystemDiagnosticsSection)
			{
				return (SystemDiagnosticsSection)section;
			}
			return null;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00045594 File Offset: 0x00043794
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000455A0 File Offset: 0x000437A0
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000455AC File Offset: 0x000437AC
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000455C4 File Offset: 0x000437C4
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00045638 File Offset: 0x00043838
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						((SwitchElement)obj).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						((ListenerElement)obj2).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						((SourceElement)obj3).ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x040009BA RID: 2490
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x040009BB RID: 2491
		private static volatile InitState initState;
	}
}
