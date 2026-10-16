using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200016E RID: 366
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	public static class ScalableBufferManager
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000D2E RID: 3374
		public static extern float widthScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000D2F RID: 3375
		public static extern float heightScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000D30 RID: 3376
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}
