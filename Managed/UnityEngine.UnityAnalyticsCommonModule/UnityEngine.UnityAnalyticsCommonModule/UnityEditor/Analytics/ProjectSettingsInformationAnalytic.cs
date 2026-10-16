using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ProjectSettingsInformationAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000236F File Offset: 0x0000056F
		public ProjectSettingsInformationAnalytic()
			: base("navigation_project_settings_info", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002388 File Offset: 0x00000588
		[RequiredByNativeCode]
		internal static ProjectSettingsInformationAnalytic CreateProjectSettingsInformationAnalytic()
		{
			return new ProjectSettingsInformationAnalytic();
		}

		// Token: 0x04000047 RID: 71
		private int agent_types_count;

		// Token: 0x04000048 RID: 72
		private int areas_count;
	}
}
