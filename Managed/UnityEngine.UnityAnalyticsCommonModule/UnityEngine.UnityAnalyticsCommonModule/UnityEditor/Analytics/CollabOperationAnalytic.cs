using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CollabOperationAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000227F File Offset: 0x0000047F
		public CollabOperationAnalytic()
			: base("collabOperation", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002298 File Offset: 0x00000498
		[RequiredByNativeCode]
		internal static CollabOperationAnalytic CreateCollabOperationAnalytic()
		{
			return new CollabOperationAnalytic();
		}

		// Token: 0x04000032 RID: 50
		public string category;

		// Token: 0x04000033 RID: 51
		public string operation;

		// Token: 0x04000034 RID: 52
		public string result;

		// Token: 0x04000035 RID: 53
		public long start_ts;

		// Token: 0x04000036 RID: 54
		public long duration;
	}
}
