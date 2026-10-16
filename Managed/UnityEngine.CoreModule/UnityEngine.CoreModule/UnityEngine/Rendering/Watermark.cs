using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F7 RID: 1271
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class Watermark
	{
		// Token: 0x060031AC RID: 12716
		[FreeFunction("IsAnyWatermarkVisible")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsVisible();
	}
}
