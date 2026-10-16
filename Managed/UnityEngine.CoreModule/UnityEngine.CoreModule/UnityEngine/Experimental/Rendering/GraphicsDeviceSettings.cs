using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000595 RID: 1429
	public static class GraphicsDeviceSettings
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600370A RID: 14090
		// (set) Token: 0x0600370B RID: 14091
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern WaitForPresentSyncPoint waitForPresentSyncPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600370C RID: 14092
		// (set) Token: 0x0600370D RID: 14093
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsJobsSyncPoint graphicsJobsSyncPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
