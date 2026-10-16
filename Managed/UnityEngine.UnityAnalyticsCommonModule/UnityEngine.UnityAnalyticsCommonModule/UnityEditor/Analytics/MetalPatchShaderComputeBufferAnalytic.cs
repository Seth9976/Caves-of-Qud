using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000018 RID: 24
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class MetalPatchShaderComputeBufferAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000230F File Offset: 0x0000050F
		public MetalPatchShaderComputeBufferAnalytic()
			: base("MetalPatchShaderComputeBuffersUsage", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002328 File Offset: 0x00000528
		[RequiredByNativeCode]
		internal static MetalPatchShaderComputeBufferAnalytic CreateMetalPatchShaderComputeBufferAnalytic()
		{
			return new MetalPatchShaderComputeBufferAnalytic();
		}
	}
}
