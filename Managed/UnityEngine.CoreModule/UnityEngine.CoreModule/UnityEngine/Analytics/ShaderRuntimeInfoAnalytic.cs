using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000476 RID: 1142
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class ShaderRuntimeInfoAnalytic : AnalyticsEventBase
	{
		// Token: 0x06002CF8 RID: 11512 RVA: 0x00055160 File Offset: 0x00053360
		private ShaderRuntimeInfoAnalytic()
			: base("shaderRuntimeInfo", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0005520C File Offset: 0x0005340C
		[RequiredByNativeCode]
		public static ShaderRuntimeInfoAnalytic CreateShaderRuntimeInfoAnalytic()
		{
			return new ShaderRuntimeInfoAnalytic();
		}

		// Token: 0x04000C6E RID: 3182
		public long VariantsRequested = 0L;

		// Token: 0x04000C6F RID: 3183
		public long VariantsRequestedMissing = 0L;

		// Token: 0x04000C70 RID: 3184
		public long VariantsRequestedUnsupported = 0L;

		// Token: 0x04000C71 RID: 3185
		public long VariantsRequestedCompiled = 0L;

		// Token: 0x04000C72 RID: 3186
		public long VariantsRequestedViaWarmup = 0L;

		// Token: 0x04000C73 RID: 3187
		public long VariantsUnused = 0L;

		// Token: 0x04000C74 RID: 3188
		public int VariantsCompilationTimeTotal = 0;

		// Token: 0x04000C75 RID: 3189
		public int VariantsCompilationTimeMax = 0;

		// Token: 0x04000C76 RID: 3190
		public int VariantsCompilationTimeMedian = 0;

		// Token: 0x04000C77 RID: 3191
		public int VariantsWarmupTimeTotal = 0;

		// Token: 0x04000C78 RID: 3192
		public int VariantsWarmupTimeMax = 0;

		// Token: 0x04000C79 RID: 3193
		public int VariantsWarmupTimeMedian = 0;

		// Token: 0x04000C7A RID: 3194
		public bool UseProgressiveWarmup = false;

		// Token: 0x04000C7B RID: 3195
		public int ShaderChunkSizeMin = 0;

		// Token: 0x04000C7C RID: 3196
		public int ShaderChunkSizeMax = 0;

		// Token: 0x04000C7D RID: 3197
		public int ShaderChunkSizeAvg = 0;

		// Token: 0x04000C7E RID: 3198
		public int ShaderChunkCountMin = 0;

		// Token: 0x04000C7F RID: 3199
		public int ShaderChunkCountMax = 0;

		// Token: 0x04000C80 RID: 3200
		public int ShaderChunkCountAvg = 0;
	}
}
