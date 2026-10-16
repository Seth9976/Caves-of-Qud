using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020005A1 RID: 1441
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public static class ShaderWarmup
	{
		// Token: 0x060037C1 RID: 14273 RVA: 0x0006D700 File Offset: 0x0006B900
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShader")]
		public static void WarmupShader(Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShader_Injected(Object.MarshalledUnityObject.Marshal<Shader>(shader), ref setup);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x0006D71C File Offset: 0x0006B91C
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShaderFromCollection")]
		public static void WarmupShaderFromCollection(ShaderVariantCollection collection, Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShaderFromCollection_Injected(Object.MarshalledUnityObject.Marshal<ShaderVariantCollection>(collection), Object.MarshalledUnityObject.Marshal<Shader>(shader), ref setup);
		}

		// Token: 0x060037C3 RID: 14275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShader_Injected(IntPtr shader, [In] ref ShaderWarmupSetup setup);

		// Token: 0x060037C4 RID: 14276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShaderFromCollection_Injected(IntPtr collection, IntPtr shader, [In] ref ShaderWarmupSetup setup);
	}
}
