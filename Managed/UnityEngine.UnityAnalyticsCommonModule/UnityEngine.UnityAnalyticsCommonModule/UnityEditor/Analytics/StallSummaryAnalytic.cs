using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000028 RID: 40
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StallSummaryAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000025AF File Offset: 0x000007AF
		public StallSummaryAnalytic()
			: base("editorStallSummary", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000025C8 File Offset: 0x000007C8
		[RequiredByNativeCode]
		internal static StallSummaryAnalytic CreateStallSummaryAnalytic()
		{
			return new StallSummaryAnalytic();
		}

		// Token: 0x04000069 RID: 105
		public double Duration;
	}
}
