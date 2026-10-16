using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000519 RID: 1305
	[RequiredByNativeCode]
	internal static class GPUDrivenCallbacks
	{
		// Token: 0x06003244 RID: 12868 RVA: 0x000611E9 File Offset: 0x0005F3E9
		[RequiredByNativeCode(GenerateProxy = true)]
		public static void InvokeGPUDrivenLODGroupDataNativeCallback(GPUDrivenLODGroupDataNativeCallback callback, in GPUDrivenLODGroupDataNative lodGroupDataNative, GPUDrivenLODGroupDataCallback target)
		{
			callback(in lodGroupDataNative, target);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000611F5 File Offset: 0x0005F3F5
		[RequiredByNativeCode(GenerateProxy = true)]
		public static void InvokeGPUDrivenRendererDataNativeCallback(GPUDrivenRendererDataNativeCallback callback, in GPUDrivenRendererGroupDataNative rendererDataNative, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback target)
		{
			callback(in rendererDataNative, meshes, materials, target);
		}
	}
}
