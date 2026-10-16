using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001B RID: 27
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SendGameBuildAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000239F File Offset: 0x0000059F
		public SendGameBuildAnalytic()
			: base("navigation_gamebuild_info", 1, SendEventOptions.kAppendBuildGuid, "")
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000023B8 File Offset: 0x000005B8
		[RequiredByNativeCode]
		internal static SendGameBuildAnalytic CreateSendGameBuildAnalytic()
		{
			return new SendGameBuildAnalytic();
		}

		// Token: 0x04000049 RID: 73
		private int navmesh_count;
	}
}
