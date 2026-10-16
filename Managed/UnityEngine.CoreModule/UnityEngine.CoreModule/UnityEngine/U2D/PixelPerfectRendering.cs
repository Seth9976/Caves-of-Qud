using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x020005B4 RID: 1460
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	public static class PixelPerfectRendering
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060037DD RID: 14301
		// (set) Token: 0x060037DE RID: 14302
		public static extern float pixelSnapSpacing
		{
			[FreeFunction("GetPixelSnapSpacing")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetPixelSnapSpacing")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
