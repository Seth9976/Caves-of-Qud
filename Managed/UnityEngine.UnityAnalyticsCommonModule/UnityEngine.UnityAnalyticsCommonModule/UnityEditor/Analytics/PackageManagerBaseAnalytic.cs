using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001C RID: 28
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerBaseAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000023CF File Offset: 0x000005CF
		public PackageManagerBaseAnalytic(string eventName)
			: base(eventName, 1, SendEventOptions.kAppendNone, "packageManager")
		{
		}

		// Token: 0x0400004A RID: 74
		public long start_ts;

		// Token: 0x0400004B RID: 75
		public long duration;

		// Token: 0x0400004C RID: 76
		public bool blocking;

		// Token: 0x0400004D RID: 77
		public string package_id;

		// Token: 0x0400004E RID: 78
		public int status_code;

		// Token: 0x0400004F RID: 79
		public string error_message;
	}
}
