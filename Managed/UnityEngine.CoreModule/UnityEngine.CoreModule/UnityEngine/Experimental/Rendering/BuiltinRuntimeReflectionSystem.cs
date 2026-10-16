using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200058D RID: 1421
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x060036F1 RID: 14065 RVA: 0x0006C780 File Offset: 0x0006A980
		public bool TickRealtimeProbes()
		{
			return BuiltinRuntimeReflectionSystem.BuiltinUpdate();
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0006C797 File Offset: 0x0006A997
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x00002925 File Offset: 0x00000B25
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x060036F4 RID: 14068
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BuiltinUpdate();

		// Token: 0x060036F5 RID: 14069 RVA: 0x0006C7A4 File Offset: 0x0006A9A4
		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}
	}
}
