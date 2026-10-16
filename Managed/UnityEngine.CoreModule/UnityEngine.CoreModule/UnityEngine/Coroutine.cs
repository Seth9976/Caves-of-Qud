using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200028C RID: 652
	[NativeHeader("Runtime/Mono/Coroutine.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x06002084 RID: 8324 RVA: 0x0003BEE9 File Offset: 0x0003A0E9
		private Coroutine()
		{
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
		~Coroutine()
		{
			Coroutine.ReleaseCoroutine(this.m_Ptr);
		}

		// Token: 0x06002086 RID: 8326
		[FreeFunction("Coroutine::CleanupCoroutineGC", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseCoroutine(IntPtr ptr);

		// Token: 0x0400099E RID: 2462
		internal IntPtr m_Ptr;

		// Token: 0x0200028D RID: 653
		internal static class BindingsMarshaller
		{
			// Token: 0x06002087 RID: 8327 RVA: 0x0003EC10 File Offset: 0x0003CE10
			public static IntPtr ConvertToNative(Coroutine coroutine)
			{
				return coroutine.m_Ptr;
			}
		}
	}
}
