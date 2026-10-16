using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class BatchRendererGroupRuntimeAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0000BE1F File Offset: 0x0000A01F
		private BatchRendererGroupRuntimeAnalytic()
			: base("brgPlayerUsage", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000BE38 File Offset: 0x0000A038
		[RequiredByNativeCode]
		public static BatchRendererGroupRuntimeAnalytic CreateBatchRendererGroupRuntimeAnalytic()
		{
			return new BatchRendererGroupRuntimeAnalytic();
		}

		// Token: 0x040003BF RID: 959
		private int brgRuntimeStatus;
	}
}
