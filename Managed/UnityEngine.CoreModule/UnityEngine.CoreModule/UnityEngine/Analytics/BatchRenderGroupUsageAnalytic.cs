using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000474 RID: 1140
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class BatchRenderGroupUsageAnalytic : AnalyticsEventBase
	{
		// Token: 0x06002CF4 RID: 11508 RVA: 0x000550FF File Offset: 0x000532FF
		public BatchRenderGroupUsageAnalytic()
			: base("brgUsageEvent", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x00055118 File Offset: 0x00053318
		[RequiredByNativeCode]
		public static BatchRenderGroupUsageAnalytic CreateBatchRenderGroupUsageAnalytic()
		{
			return new BatchRenderGroupUsageAnalytic();
		}

		// Token: 0x04000C68 RID: 3176
		public int maxBRGInstance;

		// Token: 0x04000C69 RID: 3177
		public int maxMeshCount;

		// Token: 0x04000C6A RID: 3178
		public int maxMaterialCount;

		// Token: 0x04000C6B RID: 3179
		public int maxDrawCommandBatch;
	}
}
